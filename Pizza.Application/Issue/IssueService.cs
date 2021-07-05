using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.VisualBasic;
using Pizza.Data.EF;
using System;
using System.Collections.Generic;
using System.Linq;
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
                check.DetectingTime = oba.DetectingTime;
                check.CreateDate = oba.CreateDate;
                check.UpdatedDate = oba.UpdatedDate;
                check.Supervisor = oba.Supervisor;
                check.Auditor = oba.Auditor;
                check.HowToDetect = oba.HowToDetect;
                check.FailureValidate = oba.FailureValidate;
                check.DefectPart = oba.DefectPart;
                check.DefectName = oba.DefectName;
                check.DefectType = oba.DefectType;
                check.SamplingQty = oba.SamplingQty;
                check.NgphoneOrdinal = oba.NgphoneOrdinal;
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

        public async Task<TblIssue> GetIssueById(Guid issueId)
        {
            return await _context.TblIssue.SingleOrDefaultAsync(x => x.Id == issueId);
        }

        public async Task<List<TblIssue>> GetListIssueByIssueNo(string issueNo)
        {
            // remove last -
            var listIssue = issueNo.Split('-').Reverse().Skip(1).Reverse();
            var newIssueNo = string.Join("-", listIssue);
            return await _context.TblIssue.Where(x => x.IssueNo.Substring(0, newIssueNo.Length) == newIssueNo && x.IssueStatus != "Draft").ToListAsync();
        }
        public async Task<List<TblIssue>> GetListIssueByIssueTitle(string issueTitle)
        {
            // remove last -
            var listIssueTitle = issueTitle.Split('-').Reverse().Skip(2).Reverse();
            var newIssueTitle = string.Join("-", listIssueTitle);
            return await _context.TblIssue.Where(x => x.Title.Substring(0, newIssueTitle.Length) == newIssueTitle && x.IssueStatus != "Draft").ToListAsync();
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
                check.IssueNo = issue.IssueNo;
                check.Title = issue.Title;
                check.CarNo = issue.CarNo;
                check.Severity = issue.Severity;
                check.RepeatedSymptom = issue.RepeatedSymptom;
                check.RepeatedCause = issue.RepeatedCause;
                check.FailureDesc = issue.FailureDesc;
                check.FileAttack = issue.FileAttack;
                check.NotifiedList = issue.NotifiedList;
                check.IssueStatus = issue.IssueStatus;
                check.CurrentStep = issue.CurrentStep;
                check.StepStatus = issue.StepStatus;
                check.ContainmentAction = issue.ContainmentAction;
                check.AnalysisDetail = issue.AnalysisDetail;
                check.RecommendedAction = issue.RecommendedAction;
                check.EscapeCause = issue.EscapeCause;
                check.Capadetail = issue.Capadetail;
                check.VerifyNote = issue.VerifyNote;
                check.ProcessType = issue.ProcessType;
                check.SampleReceivingTime = issue.SampleReceivingTime;
                check.CreatedDate = issue.CreatedDate;
                check.CreatedBy = issue.CreatedBy;
                check.CreateByName = issue.CreateByName;
            }
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> CreateVerificationTable(TblVerification verify)
        {
            var check = await _context.TblVerification.FirstOrDefaultAsync(x => x.Id == verify.Id);
            if (check == null)
            {
                _context.TblVerification.Add(verify);
            }
            else
            {
                check.Ponno = verify.Ponno;
                check.Size = verify.Size;
                check.Ngqty = verify.Ngqty;
                check.Ngrate = verify.Ngrate;
                check.Judgment = verify.Judgment;
                check.Date = verify.Date;
            }
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<TblIssue>> GetListIssue(DateTime from, DateTime to)
        {
            var listIssue = await _context.TblIssue.Where(x => x.CreatedDate >= from && x.CreatedDate < to).ToListAsync();
            return listIssue != null && listIssue.Count > 0 ? listIssue : null;
        }

        public async Task<List<TblProcess>> GetListProcess()
        {
            var listProcess = await _context.TblProcess.ToListAsync();
            return listProcess != null && listProcess.Count > 0 ? listProcess : null;
        }

        public async Task<bool> RemoveIssueById(Guid issueId)
        {
            var check = _context.TblIssue.Find(issueId);
            _context.TblIssue.Remove(check);
            await _context.SaveChangesAsync();
            return true;
        }


        public async Task<List<TblFile>> GetListFileByIssueId(Guid issueId)
        {
            var listFile = await _context.TblFile.Where(x => x.IssueId == issueId).OrderBy(x => x.UploadedDate).ToListAsync();
            return listFile;
        }

        public async Task<bool> RemoveFileByIssueId(Guid issueId)
        {
            var check = _context.TblFile.Where(x => x.IssueId == issueId);
            _context.TblFile.RemoveRange(check);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoveFileById(Guid id)
        {
            var check = _context.TblFile.Find(id);
            _context.TblFile.Remove(check);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoveVerificationById(Guid id)
        {
            var check = _context.TblVerification.Find(id);
            _context.TblVerification.Remove(check);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> RemoveVerificationByIssueId(Guid IssueId)
        {
            var checkList = _context.TblVerification.Where(x => x.IssueId == IssueId);
            _context.TblVerification.RemoveRange(checkList);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<TblOba> GetOBAByIssueId(Guid issueId)
        {
            var oba = await _context.TblOba.FirstOrDefaultAsync(x => x.IssueId == issueId);
            return oba;
        }

        public async Task<TblProduct> GetProductByPSN(string psn)
        {
            var product = await _context.TblProduct.FirstOrDefaultAsync(x => x.Psn == psn);
            return product;
        }

        public async Task<TblProduct> GetProductByIssueId(Guid issueId)
        {
            var product = await _context.TblProduct.FirstOrDefaultAsync(x => x.IssueId == issueId);
            return product;
        }

        public async Task<TblProcess> GetProcessByName(string processName)
        {
            var process = await _context.TblProcess.FirstOrDefaultAsync(x => x.ProcessName == processName);
            return process;
        }

        public async Task<List<TblVerification>> GetListVerificationByIssueId(Guid issueId)
        {
            var listVerify = await _context.TblVerification.Where(x => x.IssueId == issueId).ToListAsync();
            return listVerify;
        }

        public async Task<bool> RemoveOBAById(Guid id)
        {
            var check = _context.TblOba.Find(id);
            _context.TblOba.Remove(check);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoveOBAByIssueId(Guid IssueId)
        {
            var checkList = _context.TblOba.Where(x => x.IssueId == IssueId);
            _context.TblOba.RemoveRange(checkList);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoveProductById(Guid id)
        {
            var check = _context.TblProduct.Find(id);
            _context.TblProduct.Remove(check);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoveProductByIssueId(Guid IssueId)
        {
            var checkList = _context.TblProduct.Where(x => x.IssueId == IssueId);
            _context.TblProduct.RemoveRange(checkList);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
