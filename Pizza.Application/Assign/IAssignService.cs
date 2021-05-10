using Hangfire.Server;
using Pizza.Data.EF;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pizza.Application.Assign
{
    public interface IAssignService
    {
        Task<List<TblAssign>> GetListAssign(Guid issueId);
        Task<TblAssign> GetAssignById(Guid assignId);
        Task<bool> InsertUpdateAssign(TblAssign assign);
        Task<bool> InsertUpdateDeadline(TblExtendDeadline assign);
        Task<List<TblExtendDeadline>> GetListDeadLineByAssignId(Guid assignId);
        Task<bool> RemoveAssign(Guid assignId);
        Task<bool> RemoveAssignByIssueId(Guid issueId);
        Task<bool> RemoveDeadLineByAssignId(Guid assignId);
        List<string> SetScheduleDeadLine(TblAssign assign, string token);

    }
}
