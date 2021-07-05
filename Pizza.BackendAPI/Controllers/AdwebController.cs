﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Flurl.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Pizza.ViewModel.Common;

namespace Pizza.BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdwebController : ControllerBase
    {
        public IConfiguration _configuration;
        private string ADWeb_URI = "http://idmgt.fushan.fihnbb.com";
        private string CLIENT_ID = "iTamCf61OHFQQvSgzWBRtQDijutK9czS";
        private string CLIENT_SECRET = "DFHW86lqRTn8zndsilOEJ5cPM3yuNKBG";
        private string CLIENT_REDIRECT_URL = "https://localhost:44399/Login/Success";
        private static string URI_ADWEB_SEARCH = "/adweb/record/search/v1";

        public AdwebController(IConfiguration configuration)
        {
            _configuration = configuration;
            if (!string.IsNullOrEmpty(_configuration["ADWeb_URI"]))
                ADWeb_URI = _configuration["ADWeb_URI"];

            if (!string.IsNullOrEmpty(_configuration["CLIENT_ID"]))
                CLIENT_ID = _configuration["CLIENT_ID"];

            if (!string.IsNullOrEmpty(_configuration["CLIENT_SECRET"]))
                CLIENT_SECRET = _configuration["CLIENT_SECRET"];

            if (!string.IsNullOrEmpty(_configuration["CLIENT_REDIRECT_URL"]))
                CLIENT_REDIRECT_URL = _configuration["CLIENT_REDIRECT_URL"];
            //FlurlHttp.Configure(c =>
            //{
            //    c.AllowedHttpStatusRange = "100-299,4xx";
            //});
        }

        [HttpGet("token/{_code}")]
        public IActionResult GetAccessToken(string _code)
        {
            var res = (ADWeb_URI + "/adweb/oauth2/access_token/v1")
            .PostUrlEncodedAsync(new
            {
                client_id = CLIENT_ID,
                redirect_uri = CLIENT_REDIRECT_URL,
                client_secret = CLIENT_SECRET,
                code = _code,
                grant_type = "authorization_code"
            }).ReceiveString().Result;
            return Ok(res);
        }

        [HttpGet("user-infor/{token}")]
        public IActionResult GetUserInfor(string token)
        {
            var res = (ADWeb_URI + "/adweb/people/me/v1")
                .WithOAuthBearerToken(token)
                .GetStringAsync().Result;
            return Ok(res);
        }

        #region Get detail user information
        [HttpGet("user-id/{accessToken}/{Id}")]
        public IActionResult GetUserDetailByID(string accessToken, string Id) // Id(in adweb) != Employee Id
        {

            var res = (ADWeb_URI + URI_ADWEB_SEARCH)
               .WithOAuthBearerToken(accessToken)
               .SetQueryParams(new
               {
                   model = "hr.employee",
                   fields = "[\"id\", \"name\",\"ad_user_employeeID\", \"ad_user_displayName\", \"work_email\", \"job_title\", \"ad_user_sAMAccountName\", \"parent_id\", \"department_id\"]",
                   search_datas = "[('id', 'ilike', '" + Id + "')]"
               })
               .GetStringAsync().Result;

            var data = JsonConvert.DeserializeObject<List<CommonModel>>(res);
            if (data.Count == 2 && data[1].data != null && data[1].data.Count > 0)
            {
                dynamic _data = data[1].data[0][0];
                if (_data.job_title == "Head of Factory")
                {
                    var _obj = new EmployeeModel
                    {
                        id = _data.id,
                        name = _data.name,
                        ad_user_displayName = _data.ad_user_displayName,
                        ad_user_employeeID = _data.ad_user_employeeID,
                        ad_user_sAMAccountName = _data.ad_user_sAMAccountName,
                        job_title = _data.job_title,
                        work_email = _data.work_email,
                        department_id = new List<object> { "0", "Fushan Factory" },
                        parent_id = new List<object> { _data.id, _data.ad_user_displayName },
                    };
                    return Ok(_obj);
                }
                var temp = JsonConvert.DeserializeObject<EmployeeModel>(data[1].data[0][0].ToString());
                return Ok(temp);
            }
            return Ok(null);
        }

        [HttpGet("user-detail-id/{accessToken}/{empId}")]
        public IActionResult GetUserDetailByEmployeeID(string accessToken, string empId)
        {

            var res = (ADWeb_URI + URI_ADWEB_SEARCH)
               .WithOAuthBearerToken(accessToken)
               .SetQueryParams(new
               {
                   model = "hr.employee",
                   fields = "[\"id\", \"name\",\"ad_user_employeeID\", \"ad_user_displayName\", \"work_email\", \"job_title\", \"ad_user_sAMAccountName\", \"parent_id\", \"department_id\"]",
                   search_datas = "[('ad_user_employeeID', 'ilike', '" + empId + "')]"
               })
               .GetStringAsync().Result;

            var data = JsonConvert.DeserializeObject<List<CommonModel>>(res);
            if (data.Count == 2 && data[1].data != null && data[1].data.Count > 0)
            {
                dynamic _data = data[1].data[0][0];
                if (_data.job_title == "Head of Factory")
                {
                    var _obj = new EmployeeModel
                    {
                        id = _data.id,
                        name = _data.name,
                        ad_user_displayName = _data.ad_user_displayName,
                        ad_user_employeeID = _data.ad_user_employeeID,
                        ad_user_sAMAccountName = _data.ad_user_sAMAccountName,
                        job_title = _data.job_title,
                        work_email = _data.work_email,
                        department_id = new List<object> { "0", "Fushan Factory" },
                        parent_id = new List<object> { _data.id, _data.ad_user_displayName },
                    };
                    return Ok(_obj);
                }
                var temp = JsonConvert.DeserializeObject<EmployeeModel>(data[1].data[0][0].ToString());
                return Ok(temp);
            }
            return Ok(null);
        }

        [HttpGet("user-detail-email/{accessToken}/{email}")]
        public IActionResult GetUserDetailByEmail(string accessToken, string email)
        {
            var res = (ADWeb_URI + URI_ADWEB_SEARCH)
           .WithOAuthBearerToken(accessToken)
           .SetQueryParams(new
           {
               model = "hr.employee",
               fields = "[\"id\", \"name\",\"ad_user_employeeID\", \"ad_user_displayName\", \"work_email\", \"job_title\", \"ad_user_sAMAccountName\", \"parent_id\", \"department_id\"]",
               search_datas = "[('work_email', 'ilike', '" + email + "')]"
           })
           .GetStringAsync().Result;

            var data = JsonConvert.DeserializeObject<List<CommonModel>>(res);
            if (data.Count == 2 && data[1].data != null && data[1].data.Count > 0)
            {
                dynamic _data = data[1].data[0][0];
                if (_data.job_title == "Head of Factory")
                {
                    var _obj = new EmployeeModel
                    {
                        id = _data.id,
                        name = _data.name,
                        ad_user_displayName = _data.ad_user_displayName,
                        ad_user_employeeID = _data.ad_user_employeeID,
                        ad_user_sAMAccountName = _data.ad_user_sAMAccountName,
                        job_title = _data.job_title,
                        work_email = _data.work_email,
                        department_id = new List<object> { "0", "Fushan Factory" },
                        parent_id = new List<object> { _data.id, _data.ad_user_displayName },
                    };
                    return Ok(_obj);
                }
                return Ok(data[1].data[0][0]);
            }
            return Ok(null);
        }

        [HttpGet("user-detail-name/{accessToken}/{name}")]
        public IActionResult GetUserDetailByName(string accessToken, string name)
        {

            var res = (ADWeb_URI + URI_ADWEB_SEARCH)
               .WithOAuthBearerToken(accessToken)
               .SetQueryParams(new
               {
                   model = "hr.employee",
                   fields = "[\"id\", \"name\",\"ad_user_employeeID\", \"ad_user_displayName\", \"work_email\", \"job_title\", \"ad_user_sAMAccountName\", \"parent_id\", \"department_id\"]",
                   search_datas = "[('ad_user_displayName', 'ilike', '" + name + "')]"
               })
               .GetStringAsync().Result;

            var data = JsonConvert.DeserializeObject<List<CommonModel>>(res);
            if (data.Count == 2 && data[1].data != null && data[1].data.Count > 0)
            {
                dynamic _data = data[1].data[0][0];
                if (_data.job_title == "Head of Factory")
                {
                    var _obj = new EmployeeModel
                    {
                        id = _data.id,
                        name = _data.name,
                        ad_user_displayName = _data.ad_user_displayName,
                        ad_user_employeeID = _data.ad_user_employeeID,
                        ad_user_sAMAccountName = _data.ad_user_sAMAccountName,
                        job_title = _data.job_title,
                        work_email = _data.work_email,
                        department_id = new List<object> { "0", "Fushan Factory" },
                        parent_id = new List<object> { _data.id, _data.ad_user_displayName },
                    };
                    return Ok(_obj);
                }
                return Ok(data[1].data[0][0]);
            }
            return Ok(null);
        }
        #endregion

        [HttpGet("user-role/{accessToken}/{empId}")]
        public IActionResult GetUserRole(string accessToken, string empId)
        {

            List<string> listGroup = new List<string>();
            var res = (ADWeb_URI + URI_ADWEB_SEARCH)
           .WithOAuthBearerToken(accessToken)
           .SetQueryParams(new
           {
               model = "fih.nbb.group",
               fields = "[\"name\",\"id\"]",
               search_datas = "[('employee_ids.ad_user_employeeID', 'ilike', '" + empId + "')]"
           })
           .GetStringAsync().Result;
            var group = JsonConvert.DeserializeObject<dynamic>(res);
            dynamic _data = group[1].data;
            foreach (var item in _data)
            {
                listGroup.Add(item[0].name.ToString());
            }
            return Ok(listGroup);
        }

        [HttpGet("head-of-department/{accessToken}/{dept}")]
        public IActionResult GetHeadOfFuctionID(string accessToken, string dept)
        {
            var res = (ADWeb_URI + URI_ADWEB_SEARCH)
                 .WithOAuthBearerToken(accessToken)
                 .SetQueryParams(new
                 {
                     model = "hr.department",
                     fields = "[\"id\", \"ad_department_code\", \"manager_id\"]",
                     search_datas = "[('id', '!=', 21),( 'name', 'ilike', '" + dept + "'),('manager_id', '!=', False)]"
                 })
                 .GetStringAsync().Result;
            var data = JsonConvert.DeserializeObject<List<CommonModel>>(res);
            if (data.Count == 2 && data[1].data != null)
            {
                dynamic obj = data[1].data[0];
                //return Ok(data[1].data[0]);
                return Ok((int)obj[0].manager_id[0]);
            }
            return null;
        }
    }
}