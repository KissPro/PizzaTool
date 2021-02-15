using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pizza.Application.Config;

namespace Pizza.BackendAPI.Controllers
{
    [Route("api/config")]
    [ApiController]
    public class ConfigController : ControllerBase
    {
        private readonly IConfigService _configService;
        public ConfigController(IConfigService configService)
        {
            _configService = configService;
        }

        [HttpGet("list-dropdown/{name}")]
        public async Task<IActionResult> GetConfigService([FromRoute]string name)
        {
            var res = await _configService.GetDropListByName(name);
            return Ok(res);
        }
    }
}