using Microsoft.EntityFrameworkCore;
using Pizza.Data.EF;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pizza.Application.Issue
{
    public class IssueService : IIssueService
    {
        private readonly PizzaContext _context;

        public IssueService(PizzaContext context)
        {
            _context = context;
        }

        public Task<bool> CreateFile(TblFile file)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CreateOBATable(TblOba issue)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CreateProductTable(TblProduct product)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CreateUpdateIssue(TblIssue issue)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CreateVerificationTable(TblVerification verifi)
        {
            throw new NotImplementedException();
        }

        public async Task<List<TblIssue>> GetListIssue()
        {
            var listIssue = await _context.TblIssue.ToListAsync();
            return listIssue != null && listIssue.Count > 0 ? listIssue : null;
        }
    }
}
