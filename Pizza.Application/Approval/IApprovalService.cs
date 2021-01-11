using Pizza.Data.EF;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pizza.Application.Approval
{
    public interface IApprovalService
    {
        Task<bool> GetApproval();
        Task<bool> InsertUpdateApproval(TblApproval approval);
    }
}
