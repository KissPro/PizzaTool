using Flurl.Http;
using Hangfire;
using Hangfire.Server;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using Pizza.Application.Mail;
using Pizza.Data.EF;
using Pizza.Utilities.Helper;
using Pizza.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace Pizza.Application.Assign
{
    public class AssignService : IAssignService
    {
        private readonly PizzaContext _context;
        private readonly MailService _mailService;

        public AssignService(PizzaContext context)
        {
            _context = context;
            _mailService = new MailService();
        }
        public async Task<TblAssign> GetAssignById(Guid assignId)
        {
            var assign = await _context.TblAssign.SingleOrDefaultAsync(x => x.Id == assignId);
            return assign;
        }


        public async Task<bool> InsertUpdateAssign(TblAssign assign)
        {
            var checkAssign = _context.TblAssign.FirstOrDefault(x => x.Id == assign.Id);
            if (checkAssign == null)
            {
                _context.TblAssign.Add(assign);
            }
            else
            {
                checkAssign.RequestContent = assign.RequestContent;
                checkAssign.ActionContent = assign.ActionContent;
                checkAssign.AssignedDate = assign.AssignedDate;
                checkAssign.ActionDate = assign.ActionDate;
                checkAssign.DeadLine = assign.DeadLine;
                checkAssign.Team = assign.Team;
                checkAssign.Name = assign.Name;
                checkAssign.Email = assign.Email;
                checkAssign.Status = assign.Status;
                checkAssign.Remark = assign.Remark;
                checkAssign.UpdatedBy = assign.UpdatedBy;
                checkAssign.UpdatedDate = assign.UpdatedDate;
            }
            await _context.SaveChangesAsync();
            return true;
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

        public async Task<List<TblExtendDeadline>> GetListDeadLineByAssignId(Guid assignId)
        {
            return await _context.TblExtendDeadLine.Where(x => x.AssignNo == assignId).OrderBy(x => x.RequestDate).ToListAsync();
        }
        public async Task<List<TblAssign>> GetListAssign(Guid issueId)
        {
            return await _context.TblAssign.Where(x => x.IssueNo == issueId).OrderBy(x => x.AssignedDate).ToListAsync();
        }

        public async Task<bool> RemoveAssign(Guid assignId)
        {
            var check = await _context.TblAssign.FindAsync(assignId);
            //Remove all deadline schedule
            if (check != null && !String.IsNullOrEmpty(check.ScheduleDeadLine))
            {
                foreach (var item in check.ScheduleDeadLine.Split(';'))
                {
                    BackgroundJob.Delete(item);
                }
            }

            _context.TblAssign.Remove(check);
            _context.SaveChanges();
            return true;
        }

        public async Task<bool> RemoveAssignByIssueId(Guid issueId)
        {
            var checkList = _context.TblAssign.Where(x => x.IssueNo == issueId);
            // Remove all deadline schedule
            foreach (var item in checkList)
            {
                if (item != null && !String.IsNullOrEmpty(item.ScheduleDeadLine))
                {
                    foreach (var re in item.ScheduleDeadLine.Split(';'))
                    {
                        BackgroundJob.Delete(re);
                    }
                }
            }

            _context.TblAssign.RemoveRange(checkList);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoveDeadLineByAssignId(Guid assignId)
        {
            var list = _context.TblExtendDeadLine.Where(x => x.AssignNo == assignId);
            _context.TblExtendDeadLine.RemoveRange(list);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task CheckAndSendMail(Guid assignId, string token, string step, int level)
        {
            var check = await _context.TblAssign.FindAsync(assignId);
            if (check == null)
            {
                return;
            }
            var checkIssue = await _context.TblIssue.FindAsync(check.IssueNo);
            // Check if not done yet and deadlevel != 1
            if (check.Status.ToUpper() != "DONE" && check.Status.ToUpper() != "REASSIGNED")
            {
                // Owner information
                FlurlClient flurlClient = new FlurlClient("http://localhost:5000/api/adweb/");
                var resultOwner = await flurlClient.Request("user-detail-id/" + token + "/" + check.OwnerId).GetJsonAsync();
                var ownerMail = resultOwner.work_email;
                var ownerName = resultOwner.ad_user_displayName;

                // CC
                List<string> cc = new List<string>();
                // SLM information
                if (level >= 1)
                {
                    string slmId = "/" + resultOwner.parent_id[0].ToString();
                    var result = await flurlClient.Request("user-id/" + token + slmId).GetJsonAsync();
                    cc.Add(result.work_email);
                }
                // Head information
                if (level >= 2)
                {
                    string department = "/" + resultOwner.department.ToString();
                    var resultHead = await flurlClient.Request("head-of-department/" + token + "/" + department).GetStringAsync();
                    var result = await flurlClient.Request("user-id/" + token + "/" + resultHead).GetJsonAsync();
                    cc.Add(result.work_email);
                }

                // Head of factory
                //if (level <= 3)
                //{

                //}

                // Send Mail
                var newMail = new MailModel()
                {
                    Sender = "The Pizza System",
                    To = ownerMail,
                    //Cc = string.Join(";", cc),
                    Cc = ownerMail,
                    Bcc = "",
                    Subject = "Deadline Notification" + checkIssue.Title.ToUpper(),
                    Content = string.Format(MailFormat.Email_Noti_Deadline, ownerName, step.ToUpper(), check.IssueNo)
                };
                await _mailService.SendMail(newMail);
            }
            else
            {
                // Done - Remove all deadline
                if (!String.IsNullOrEmpty(check.ScheduleDeadLine))
                {
                    foreach (var item in check.ScheduleDeadLine.Split(';'))
                    {
                        BackgroundJob.Delete(item);
                    }
                }
            }
        }
        public List<string> SetScheduleDeadLine(TblAssign assign, string token)
        {
            List<string> listScheduleID = new List<string>();
            var timeDeadLine = assign.DeadLine.Subtract(DateTime.Now);

            if (assign.CurrentStep == "caca")
            {
                var jobId = BackgroundJob.Schedule(() => CheckAndSendMail(assign.Id, token, assign.CurrentStep, 1), timeDeadLine);
                var jobId24 = BackgroundJob.Schedule(() => CheckAndSendMail(assign.Id, token, assign.CurrentStep, 2), timeDeadLine + TimeSpan.FromHours(24));
                var jobId48 = BackgroundJob.Schedule(() => CheckAndSendMail(assign.Id, token, assign.CurrentStep, 3), timeDeadLine + TimeSpan.FromHours(48));
                listScheduleID.Add(jobId);
                listScheduleID.Add(jobId24);
                listScheduleID.Add(jobId48);
            }
            else if (assign.CurrentStep == "caca1")
            {
                var jobId168 = BackgroundJob.Schedule(() => CheckAndSendMail(assign.Id, token, assign.CurrentStep, 1), timeDeadLine + TimeSpan.FromHours(168));
                var jobId192 = BackgroundJob.Schedule(() => CheckAndSendMail(assign.Id, token, assign.CurrentStep, 2), timeDeadLine + TimeSpan.FromHours(192));
                var jobId216 = BackgroundJob.Schedule(() => CheckAndSendMail(assign.Id, token, assign.CurrentStep, 3), timeDeadLine + TimeSpan.FromHours(216));
                listScheduleID.Add(jobId168);
                listScheduleID.Add(jobId192);
                listScheduleID.Add(jobId216);
            }
            else if (assign.CurrentStep == "capa" || assign.CurrentStep == "close")
            {
                var jobId192 = BackgroundJob.Schedule(() => CheckAndSendMail(assign.Id, token, assign.CurrentStep, 1), timeDeadLine + TimeSpan.FromHours(192));
                var jobId216 = BackgroundJob.Schedule(() => CheckAndSendMail(assign.Id, token, assign.CurrentStep, 2), timeDeadLine + TimeSpan.FromHours(216));
                var jobId240 = BackgroundJob.Schedule(() => CheckAndSendMail(assign.Id, token, assign.CurrentStep, 3), timeDeadLine + TimeSpan.FromHours(240));
                listScheduleID.Add(jobId192);
                listScheduleID.Add(jobId216);
                listScheduleID.Add(jobId240);
            }
            return listScheduleID;
        }
    }
}
