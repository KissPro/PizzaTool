using System;
using System.Collections.Generic;

namespace Pizza.Data.EF
{
    public partial class TblApproval
    {
        public Guid Id { get; set; }
        public Guid IssueNo { get; set; }
        public string ApproverId { get; set; }
        public string Team { get; set; }
        public string Action { get; set; }
        public string ApproverRemark { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public virtual TblIssue IssueNoNavigation { get; set; }
    }
}
