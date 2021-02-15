using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Pizza.Data.EF;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace Pizza.Application.Assign
{
    public class AssignService : IAssignService
    {
        private readonly PizzaContext _context;

        public AssignService(PizzaContext context)
        {
            _context = context;
        }
        public async Task<TblAssign> GetAssign(Guid assignId)
        {
            var assign = await _context.TblAssign.SingleOrDefaultAsync(x => x.Id == assignId);
            return assign;
        }


        public async Task<bool> InsertUpdateAssign(TblAssign assign)
        {
            var checkAssign = _context.TblAssign.FirstOrDefault(x => x.IssueNo == assign.IssueNo && x.CurrentStep == assign.CurrentStep && x.OwnerId == assign.OwnerId);
            if (checkAssign == null)
            {
                _context.TblAssign.Add(assign);
            }
            else
            {
                checkAssign.RequestContent = assign.RequestContent;
                checkAssign.ActionContent = assign.ActionContent;
                checkAssign.AssignedDate = assign.AssignedDate;
                checkAssign.DeadLine = assign.DeadLine;
                checkAssign.DeadLevel = assign.DeadLevel;
                checkAssign.Team = assign.Team;
                checkAssign.Status = assign.Status;
                checkAssign.Remark = assign.Remark;
                checkAssign.UpdatedBy = assign.UpdatedBy;
                checkAssign.UpdatedDate = assign.UpdatedDate;
            }
            await _context.SaveChangesAsync();
            return true;
        }


        public Task<List<TblExtendDeadline>> GetListDeadLineByAssignId(Guid assignId)
        {
            throw new NotImplementedException();
        }
        public async Task<bool> InsertUpdateDeadline(TblExtendDeadline assign)
        {
            var checkDeadline = _context.TblExtendDeadLine.FirstOrDefault(x => x.Id == assign.Id);
            if (checkDeadline == null)
            {
                _context.TblExtendDeadLine.Add(assign);
            }
            else
            {
                checkDeadline.Reason = assign.Reason;
                checkDeadline.CurrentDeadLine = assign.CurrentDeadLine;
                checkDeadline.RequestDeadLine = assign.RequestDeadLine;
                checkDeadline.ApprovalContent = assign.ApprovalContent;
                checkDeadline.Status = assign.Status;
                checkDeadline.RequestDate = assign.RequestDate;
                checkDeadline.ApprovalDate = assign.ApprovalDate;
            }
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
