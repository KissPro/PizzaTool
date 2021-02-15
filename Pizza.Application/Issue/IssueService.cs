using Microsoft.EntityFrameworkCore;
using Pizza.Data.EF;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
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

        public async Task<bool> CreateFile(TblFile file)
        {
            var check = await _context.TblFile.FirstOrDefaultAsync(x => x.Id == file.Id);
            if (check == null)
            {
                _context.TblFile.Add(file);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> CreateOBATable(TblOba oba)
        {
            var check = await _context.TblOba.FirstOrDefaultAsync(x => x.Id == oba.Id);
            if (check == null)
            {
                _context.TblOba.Add(oba);
            }
            else
            {
                check.DetectingTime = check.DetectingTime;
                check.DefectPart = check.DefectPart;
                check.DefectName = check.DefectName;
                check.DefectType = check.DefectType;
                check.SamplingQty = check.SamplingQty;
                check.NgphoneOrdinal = check.NgphoneOrdinal;
            }
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> CreateProductTable(TblProduct product)
        {
            var check = await _context.TblProduct.FirstOrDefaultAsync(x => x.Id == product.Id);
            if (check == null)
            {
                _context.TblProduct.Add(product);
            }
            else
            {
                check.Imei = product.Imei;
                check.Customer = product.Customer;
                check.Psn = product.Psn;
                check.Ponno = product.Ponno;
                check.Ponsize = product.Ponsize;
                check.Spcode = product.Spcode;
                check.Line = product.Line;
                check.Pattern = product.Pattern;
                check.Shift = product.Shift;
            }
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> CreateUpdateIssue(TblIssue issue)
        {
            var check = await _context.TblIssue.FirstOrDefaultAsync(x => x.Id == issue.Id);
            if (check == null)
            {
                _context.TblIssue.Add(issue);
            }
            else
            {
                check.IssueNo = check.IssueNo;
                check.IssueStatus = check.IssueStatus;
            }
            await _context.SaveChangesAsync();
            return true;
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

        public async Task<List<TblProcess>> GetListProcess()
        {
            var listProcess = await _context.TblProcess.ToListAsync();
            return listProcess != null && listProcess.Count > 0 ? listProcess : null;
        }
    }
}
