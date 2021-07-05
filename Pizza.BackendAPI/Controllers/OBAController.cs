using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Pizza.ViewModel.Common;

namespace Pizza.BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OBAController : ControllerBase
    {
        public IConfiguration _configuration;

        public OBAController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // GET: OBA
        [HttpGet("lists")]
        public IActionResult GetListOBA(string queryJson)
        {
            var request = JsonConvert.DeserializeObject<OBARequest>(queryJson);
            var obaList = ListFailItem3(request.Date, request.Factory, request.Npi);
            return Ok(obaList);
        }
        public List<OBAVM> ListFailItem3(DateTime date, string factory, bool? npi)
        {
            List<OBAFailViewModel> listITEM = new List<OBAFailViewModel>();
            List<OBAVM> listOBA = new List<OBAVM>();
            using (var conn = new SqlConnection(_configuration["ConnectionStrings:FMSRDbContext"]))
            {
                conn.Open();
                using (var command = new SqlCommand("[dbo].[p_OBA_Fail_3]", conn) { CommandType = CommandType.StoredProcedure })
                {
                    command.Parameters.Add(new SqlParameter("@date", date));
                    command.Parameters.Add(new SqlParameter("@factory", factory));
                    command.Parameters.Add(new SqlParameter("@npi", npi));
                    // execute the command
                    using (SqlDataReader rdr = command.ExecuteReader())
                    {
                        listITEM = DataReaderMapToList<OBAFailViewModel>(rdr);
                    }
                }
                foreach (var item in listITEM)
                {
                    using (var command = new SqlCommand("[dbo].[p_OBA_Fail_Items_3]", conn) { CommandType = CommandType.StoredProcedure })
                    {
                        command.Parameters.Add(new SqlParameter("@pid", item.PID));
                        command.Parameters.Add(new SqlParameter("@line", item.LINE));
                        command.Parameters.Add(new SqlParameter("@factory", factory));
                        command.Parameters.Add(new SqlParameter("@npi", npi));
                        using (SqlDataReader rdr = command.ExecuteReader())
                        {
                            var list = DataReaderMapToList<OBAVM>(rdr);
                            listOBA.AddRange(list);
                        }
                    }
                }
            }
            return listOBA;
        }
        public static List<T> DataReaderMapToList<T>(IDataReader dr)
        {
            List<T> list = new List<T>();
            T obj = default(T);
            while (dr.Read())
            {
                obj = Activator.CreateInstance<T>();
                foreach (PropertyInfo prop in obj.GetType().GetProperties())
                {
                    if (!object.Equals(dr[prop.Name], DBNull.Value))
                    {
                        prop.SetValue(obj, dr[prop.Name], null);
                    }
                }
                list.Add(obj);
            }
            return list;
        }
    }
}
