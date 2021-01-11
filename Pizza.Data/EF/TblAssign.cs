using System;
using System.Collections.Generic;

namespace Pizza.Data.EF
{
    public partial class TblAssign
    {
        public Guid Id { get; set; }
        public Guid IssueNo { get; set; }
        public string CurrentStep { get; set; }
        public string Team { get; set; }
        public string OwnerId { get; set; }
        public string RequestContent { get; set; }
        public string ActionResult { get; set; }
        public string ActionContent { get; set; }
        public DateTime AssignedDate { get; set; }
        public DateTime DeadLine { get; set; }
        public int? DeadLevel { get; set; }
        public string Status { get; set; }
        public string Remark { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public virtual TblIssue IssueNoNavigation { get; set; }
    }
}
