using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pizza.Application.Approval;
using Pizza.Data.EF;

namespace Pizza.BackendAPI.Controllers
{
    [Route("api/approval")]
    [ApiController]
    public class ApprovalController : ControllerBase
    {
        private IApprovalService _approvalService;
        public ApprovalController(IApprovalService approvalService)
        {
            _approvalService = approvalService;
        }

        [HttpPost]
        public async Task<IActionResult> InsertOrUpdate([FromBody]TblApproval approval)
        {
            var result = await _approvalService.InsertUpdateApproval(approval);
            return Ok(result);
        }

        [HttpGet("get-issueId/{issueId}")]
        public async Task<IActionResult> GetListApprovalByIssueId([FromRoute]Guid issueId)
        {
            var listApproval = await _approvalService.GetListApprovalByIssueId(issueId);
            return Ok(listApproval);
        }

        [HttpGet("get-id/{id}")]
        public async Task<IActionResult> GetApprovalById([FromRoute]Guid id)
        {
            var approval = await _approvalService.GetApprovalById(id);
            return Ok(approval);
        }


        [HttpDelete("remove-id/{id}")]
        public async Task<IActionResult> RemoveApprovalById([FromRoute]Guid id)
        {
            var result = await _approvalService.RemoveApprovalById(id);
            return Ok(result);
        }

        [HttpDelete("remove-issueId/{issueId}")]
        public async Task<IActionResult> RemoveApprovalByIssueId([FromRoute]Guid issueId)
        {
            var result = await _approvalService.RemoveApprovalByIssueId(issueId);
            return Ok(result);
        }
    }
}