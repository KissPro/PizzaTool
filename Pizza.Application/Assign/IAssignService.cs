using Pizza.Data.EF;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pizza.Application.Assign
{
    public interface IAssignService
    {
        Task<bool> GetAssign();
        Task<bool> InsertUpdateAssign(TblAssign assign);
    }
}
