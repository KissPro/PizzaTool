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
        Task<List<TblIssue>> GetListIssue(DateTime from, DateTime to);
        Task<TblIssue> GetIssueById(Guid issueId);
        Task<List<TblIssue>> GetListIssueByIssueNo(string issueNo);
        Task<List<TblIssue>> GetListIssueByIssueTitle(string issueTitle);
        Task<bool> RemoveIssueById(Guid issueId);
        Task<List<TblProcess>> GetListProcess();
        Task<TblProcess> GetProcessByName(string processName);
        // Create Issue
        Task<bool> CreateUpdateIssue(TblIssue issue);

        // OBA
        Task<bool> CreateOBATable(TblOba issue);
        Task<TblOba> GetOBAByIssueId(Guid issueId);
        Task<bool> RemoveOBAById(Guid id);
        Task<bool> RemoveOBAByIssueId(Guid IssueId);

        // Product
        Task<bool> CreateProductTable(TblProduct product);
        Task<TblProduct> GetProductByIssueId(Guid issueId);
        Task<bool> RemoveProductById(Guid id);
        Task<bool> RemoveProductByIssueId(Guid IssueId);

        // Verification
        Task<bool> CreateVerificationTable(TblVerification verifi);
        Task<List<TblVerification>> GetListVerificationByIssueId(Guid issueId);
        Task<bool> RemoveVerificationById(Guid id);
        Task<bool> RemoveVerificationByIssueId(Guid IssueId);

        // File
        Task<bool> CreateFile(TblFile file);
        Task<List<TblFile>> GetListFileByIssueId(Guid issueId);
        Task<bool> RemoveFileByIssueId(Guid issueId);
        Task<bool> RemoveFileById(Guid id);
    }
}
