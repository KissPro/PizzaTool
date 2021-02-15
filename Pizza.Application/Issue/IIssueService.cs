using Pizza.Data.EF;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pizza.Application.Issue
{
    public interface IIssueService
    {
        // Get information
        Task<List<TblIssue>> GetListIssue();
        Task<List<TblProcess>> GetListProcess();
        // Create Issue
        Task<bool> CreateUpdateIssue(TblIssue issue);

        Task<bool> CreateOBATable(TblOba issue);

        Task<bool> CreateProductTable(TblProduct product);

        Task<bool> CreateVerificationTable(TblVerification verifi);

        Task<bool> CreateFile(TblFile file);

    }
}
