using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pizza.Application.Config;
using Pizza.Data.EF;

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

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var res = await _configService.GetAll();
            return Ok(res);
        }

        [HttpGet("list-dropdown/{name}")]
        public async Task<IActionResult> GetConfigService([FromRoute]string name)
        {
            var res = await _configService.GetDropListByName(name);
            return Ok(res);
        }

        [HttpPost]
        public async Task<IActionResult> InsertOrUpdateConfig([FromBody]TblDropList config)
        {
            var res = await _configService.AddConfig(config);
            return Ok(res);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveConfig([FromRoute]Guid id)
        {
            var res = await _configService.RemoveConfig(id);
            return Ok(res);
        }
    }
}