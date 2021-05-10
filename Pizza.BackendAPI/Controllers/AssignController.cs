using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Flurl.Http;
using Hangfire;
using Hangfire.Server;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using Newtonsoft.Json;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
using Pizza.Application.Assign;
using Pizza.Application.Mail;
using Pizza.Data.EF;
using Pizza.ViewModel.Common;

namespace Pizza.BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssignController : ControllerBase
    {
        private IAssignService _assignService;

        public AssignController(IAssignService assignService)
        {
            _assignService = assignService;
        }

        [HttpPost("insert-update-assign")]
        public async Task<IActionResult> InsertUpdateAssign([FromBody]TblAssign assign)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _assignService.InsertUpdateAssign(assign);
            return Ok(result);
        }

        [HttpPost("check-deadline/{token}")]
        public async Task<IActionResult> CheckUpdateDeadline([FromBody]TblAssign assign, [FromRoute]string token)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            // Draft -> not create mail schedule
            if (assign.Status.ToUpper() != "DRAFT" && assign.Status.ToUpper() != "SUBMIT-DRAFT")
            {
                // Not yet Done -> create mail schedule
                if (assign.Status.ToUpper() != "DONE")
                {
                    var listJobId = _assignService.SetScheduleDeadLine(assign, token);
                    if (listJobId.Count > 0)
                    {
                        var update = await _assignService.GetAssignById(assign.Id);

                        if (update != null)
                        {
                            // Remove old job
                            if (!String.IsNullOrEmpty(update.ScheduleDeadLine))
                            {
                                foreach (var item in update.ScheduleDeadLine.Split(';'))
                                {
                                    BackgroundJob.Delete(item);
                                }
                            }
                            // Insert list jobId -> database(schedule level)
                            update.ScheduleDeadLine = string.Join(';', listJobId);
                            await _assignService.InsertUpdateAssign(update);
                        }
                    }
                }
                else
                {
                    // Done -> remove all schedule
                    var check = await _assignService.GetAssignById(assign.Id);
                    if (!String.IsNullOrEmpty(check.ScheduleDeadLine))
                    {
                        foreach (var item in check.ScheduleDeadLine.Split(';'))
                        {
                            BackgroundJob.Delete(item);
                        }
                    }
                }
            }
            return Ok(true);
        }



        [HttpPost("insert-update-deadline")]
        public async Task<IActionResult> InsertUpdateDeadline([FromBody]TblExtendDeadline deadline)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _assignService.InsertUpdateDeadline(deadline);
            return Ok(result);
        }

        [HttpGet("get-deadline/{assignId}")]
        public async Task<IActionResult> GetDeadLineByAssign([FromRoute]Guid assignId)
        {
            var result = await _assignService.GetListDeadLineByAssignId(assignId);
            return Ok(result);
        }

        [HttpGet("get-list-assign/{issueId}")]
        public async Task<IActionResult> GetListAssign([FromRoute]Guid issueId)
        {
            var result = await _assignService.GetListAssign(issueId);
            return Ok(result);
        }

        [HttpGet("get-assign-id/{assignId}")]
        public async Task<IActionResult> GetAssignById([FromRoute]Guid assignId)
        {
            var result = await _assignService.GetAssignById(assignId);
            return Ok(result);
        }

        [HttpDelete("remove-assign/{assignId}")]
        public async Task<IActionResult> RemoveAssignById([FromRoute]Guid assignId)
        {
            var result = await _assignService.RemoveAssign(assignId);
            if (result)
                result = await _assignService.RemoveDeadLineByAssignId(assignId);
            return Ok(result);
        }

    }
}