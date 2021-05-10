using Pizza.Data.EF;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pizza.Application.Approval
{
    public interface IApprovalService
    {
        Task<TblApproval> GetApprovalById(Guid id);
        Task<List<TblApproval>> GetListApprovalByIssueId(Guid issueId);
        Task<bool> InsertUpdateApproval(TblApproval approval);
        Task<bool> RemoveApprovalById(Guid id);
        Task<bool> RemoveApprovalByIssueId(Guid IssueId);
    }
}
