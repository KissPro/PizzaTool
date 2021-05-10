using Microsoft.EntityFrameworkCore;
using Pizza.Data.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizza.Application.Approval
{
    public class ApprovalService : IApprovalService
    {
        private readonly PizzaContext _context;

        public ApprovalService(PizzaContext context)
        {
            _context = context;
        }
        public async Task<TblApproval> GetApprovalById(Guid id)
        {
            var check = await _context.TblApproval.FindAsync(id);
            return check;
        }

        public async Task<List<TblApproval>> GetListApprovalByIssueId(Guid issueId)
        {
            var checkList = await _context.TblApproval.Where(x => x.IssueNo == issueId).ToListAsync();
            return checkList;
        }

        public async Task<bool> InsertUpdateApproval(TblApproval approval)
        {
            var check = _context.TblApproval.FirstOrDefault(x => x.IssueNo == approval.IssueNo && x.ApproverId == approval.ApproverId);
            if (check == null)
            {
                _context.TblApproval.Add(approval);
            }
            else
            {
                check.ApproverId = approval.ApproverId;
                check.ApproverRemark = approval.ApproverRemark;
                check.Action = approval.Action;
                check.Team = approval.Team;
                check.UpdatedBy = approval.UpdatedBy;
                check.UpdatedDate = approval.UpdatedDate;
            }
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoveApprovalById(Guid id)
        {
            var check = _context.TblApproval.Find(id);
            _context.TblApproval.Remove(check);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoveApprovalByIssueId(Guid IssueId)
        {
            var checkList = _context.TblApproval.Where(x => x.IssueNo == IssueId);
            _context.TblApproval.RemoveRange(checkList);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
