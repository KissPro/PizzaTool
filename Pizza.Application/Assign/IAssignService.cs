using Pizza.Data.EF;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pizza.Application.Assign
{
    public interface IAssignService
    {
        Task<TblAssign> GetAssign(Guid assignId);
        Task<bool> InsertUpdateAssign(TblAssign assign);
        Task<bool> InsertUpdateDeadline(TblExtendDeadline assign);
        Task<List<TblExtendDeadline>> GetListDeadLineByAssignId(Guid assignId);
    }
}
