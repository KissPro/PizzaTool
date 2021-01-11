using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pizza.Application.Issue;
using Pizza.Data.EF;

namespace PizzaTool.Controller
{
    [Route("api/issue")]
    [ApiController]
    public class IssueController : ControllerBase
    {
        private IIssueService _issueService;
        public IssueController(IIssueService issueService)
        {
            _issueService = issueService;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetListIssue()
        {
            var listIssue = await _issueService.GetListIssue();
            return Ok(listIssue);
        }
    }
}