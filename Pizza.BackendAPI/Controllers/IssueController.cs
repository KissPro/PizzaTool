﻿using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters.Xml;
using Microsoft.CodeAnalysis.FlowAnalysis;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using OfficeOpenXml;
using Oracle.ManagedDataAccess.Client;
using Pizza.Application.Issue;
using Pizza.Data.EF;
using Pizza.Utilities.Helper;
using Pizza.ViewModel.Common;
using Serilog;

namespace Pizza.BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IssueController : ControllerBase
    {
        private IIssueService _issueService;
        public IConfiguration _configuration;


        public IssueController(IIssueService issueService, IConfiguration configuration)
        {
            _issueService = issueService;
            _configuration = configuration;
        }

        #region Get Information
        [HttpGet("imei-information/{imei}")]
        public async Task<IActionResult> GetImeiInformation([FromRoute]string imei)
        {
            DataTable dt = new DataTable();
            using (var conn = new OracleConnection(_configuration["ConnectionStrings:IFuse"]))
            {
                conn.Open();
                Log.Information("Open ifuse connection string");
                using (OracleCommand command = conn.CreateCommand())
                {
                    using (OracleTransaction transaction = conn.BeginTransaction(IsolationLevel.ReadCommitted))
                    {
                        Log.Information("Start transaction");
                        command.Transaction = transaction;
                        command.CommandText = @"
                                                select p.Sn_Value as IMEI,p.PID as PSN, p.bu as CUSTOMER, s.* from sfc_wip_sn_ex p
                                                inner join
                                                (select a.PID,
                                                       a.FAMILY_NAME as Product,
                                                       a.WORK_ORDER as PONNo,
                                                       b.TARGET_QTY as PONSize,
                                                       a.Model_Name as SPCode,
                                                       a.LINE as Line,
                                                       a.In_Station_Time as Shift
                                                   from  sfc_wip_tracking a inner join SFC_WO_INFO b on a.WORK_ORDER = b.wo_no
                                                   where a.PID = (select PID from sfc_wip_sn_ex where sn_value = '" + imei.Trim() + @"')
                                                   ) s
                                                on p.PID = s.PID
                                                and p.sn_name = 'IMEI'
                        ";
                        command.ExecuteReader();
                        OracleDataAdapter da = new OracleDataAdapter(command);
                        da.Fill(dt);
                        await transaction.CommitAsync();
                    }
                }
            };
            return Ok(dt);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllListIssue() 
        {
            var listIssue = await _issueService.GetListIssue();
            return Ok(listIssue);
        }
    
        [HttpPost("list-issue")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> GetListIssue([FromBody]DTParameterModel dtParameters)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var searchBy = dtParameters.Search?.Value;

            // if we have an empty search then just order the results by Id ascending
            var orderCriteria = "Delivery";
            var orderAscendingDirection = true;

            if (dtParameters.Order != null)
            {
                // in this example we just default sort on the 1st column
                orderCriteria = dtParameters.Columns.ElementAt(dtParameters.Order.FirstOrDefault().Column).Data;
                orderAscendingDirection = dtParameters.Order.FirstOrDefault().Dir.ToString().ToLower() == "asc";
            }

            IQueryable<TblIssue> list = (await _issueService.GetListIssue()).AsQueryable();
            var totalResultsCount = list.Count();

            // filter
            if (!string.IsNullOrEmpty(searchBy))
            {
                foreach (var item in searchBy.Split(';').ToList())
                {

                    // check each line
                    var listCheck = list.Where(x =>
                                          x.IssueNo.ToString().ToLower().Contains(item.ToLower())
                                       || x.Title.ToString().ToLower().Contains(item.ToLower())
                                       || x.IssueStatus.ToLower().Contains(item.ToLower())
                                       || x.FailureDesc.ToLower().Contains(item.ToLower())
                                       || x.ProcessType.ToLower().Contains(item.ToLower())
                                       //|| ((!String.IsNullOrEmpty(x.Address)) && x.Address.ToLower().Contains(item.ToLower()))
                                       //|| ((!String.IsNullOrEmpty(x.HarmonizationCode)) && x.HarmonizationCode.ToLower().Contains(item.ToLower()))
                                       //|| x.Plant.ToLower().Contains(item.ToLower())
                                       //|| x.Status.ToString().ToLower().Contains(item.ToLower())
                                       );
                    if (listCheck.FirstOrDefault() == null)
                        continue;
                    else
                        list = listCheck;
                }
            }

            list = orderAscendingDirection ? list.OrderByDynamic(orderCriteria, DtOrderDir.Asc) : list.OrderByDynamic(orderCriteria, DtOrderDir.Desc);

            // now just get the count of items (without the skip and take) - eg how many could be returned with filtering
            var filteredResultsCount = list.Count();
            // add list to session for download
            HttpContext.Session.SetString("ListIssue", JsonConvert.SerializeObject(list));

            var jsonData = new
            {
                draw = dtParameters.Draw,
                recordsFiltered = filteredResultsCount,
                recordsTotal = totalResultsCount,
                data = list
                    .Skip(dtParameters.Start)
                    .Take(dtParameters.Length)
                    .ToList(),
                start = dtParameters.Start
            };
            return Ok(jsonData);
        }

        [HttpGet("download")]
        public async Task<IActionResult> Download()
        {

            var listDN = JsonConvert.DeserializeObject<List<TblIssue>>(HttpContext.Session.GetString("ListIssue"));
            if (listDN == null) return BadRequest();
            using (var excelPackage = new ExcelPackage())
            {
                var workbook = excelPackage.Workbook;
                var workSheet = workbook.Worksheets.Add("Sheet1");
                // Header
                workSheet.Cells[1, 1].Value = "#";
                workSheet.Cells[1, 2].Value = "IssueNo";
                workSheet.Cells[1, 3].Value = "Status";
                //workSheet.Cells[1, 4].Value = "Level";
                //workSheet.Cells[1, 5].Value = "Item";
                //workSheet.Cells[1, 6].Value = "Ten Hang";
                //workSheet.Cells[1, 7].Value = "Ma HS";
                //workSheet.Cells[1, 8].Value = "Quantity";
                //workSheet.Cells[1, 9].Value = "Don Gia";
                //workSheet.Cells[1, 10].Value = "Country";
                //workSheet.Cells[1, 11].Value = "So TK";
                //workSheet.Cells[1, 12].Value = "Ngay DK";
                //workSheet.Cells[1, 13].Value = "Alt Group";
                //workSheet.Cells[1, 14].Value = "Sort String";
                // Data
                for (int i = 0; i < listDN.Count; i++)
                {
                    var item = listDN[i];
                    workSheet.Cells[i + 2, 1].Value = i + 1;
                    workSheet.Cells[i + 2, 2].Value = item.IssueNo;
                    workSheet.Cells[i + 2, 3].Value = item.IssueStatus;
                    //workSheet.Cells[i + 2, 4].Value = item.Level;
                    //workSheet.Cells[i + 2, 5].Value = item.Item;
                    //workSheet.Cells[i + 2, 6].Value = item.TenHang;
                    //workSheet.Cells[i + 2, 7].Value = item.MaHS;
                    //workSheet.Cells[i + 2, 8].Value = item.Quantity;
                    //workSheet.Cells[i + 2, 9].Value = item.DonGiaHd;
                    //workSheet.Cells[i + 2, 10].Value = item.Country;
                    //workSheet.Cells[i + 2, 11].Value = item.SoTk;
                    //workSheet.Cells[i + 2, 12].Value = item.NgayDk;
                    //workSheet.Cells[i + 2, 13].Value = item.AltGroup;
                    //workSheet.Cells[i + 2, 14].Value = item.SortString;
                }
                // Border
                workSheet.Cells[1, 1, 1, 3].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                workSheet.Cells[1, 1, 1, 3].Style.Font.Bold = true;

                var memory = await Task.Run(() => new MemoryStream(excelPackage.GetAsByteArray()));
                memory.Position = 0;
                return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"); // excel type .xlsx
            }
        }

        [HttpGet("list-process")]
        public async Task<IActionResult> GetListProcess()
        {
            var listProcess = await _issueService.GetListProcess();
            return Ok(listProcess);
        }

        #endregion
        #region Create Information
        [HttpPost("create-issue")]
        public async Task<IActionResult> CreateIssue([FromBody]TblIssue issue)
        {
            var result = await _issueService.CreateUpdateIssue(issue);
            return Ok(result);
        }

        [HttpPost("create-oba")]
        public async Task<IActionResult> CreateOBA([FromBody]TblOba oba)
        {
            var result = await _issueService.CreateOBATable(oba);
            return Ok(result);
        }
        [HttpPost("create-product")]
        public async Task<IActionResult> CreateProduct([FromBody]TblProduct product)
        {
            var result = await _issueService.CreateProductTable(product);
            return Ok(result);
        }
        [HttpPost("create-file")]
        public async Task<IActionResult> CreateFile([FromBody]TblFile file)
        {
            var result = await _issueService.CreateFile(file);
            return Ok(result);
        }
        #endregion
    }

}