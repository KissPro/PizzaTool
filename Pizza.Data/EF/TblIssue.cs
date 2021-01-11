using System;
using System.Collections.Generic;

namespace Pizza.Data.EF
{
    public partial class TblIssue
    {
        public TblIssue()
        {
            TblApproval = new HashSet<TblApproval>();
            TblAssign = new HashSet<TblAssign>();
            TblAudit = new HashSet<TblAudit>();
            TblFile = new HashSet<TblFile>();
            TblIt = new HashSet<TblIt>();
            TblOba = new HashSet<TblOba>();
            TblProduct = new HashSet<TblProduct>();
            TblScraptCost = new HashSet<TblScraptCost>();
            TblVerification = new HashSet<TblVerification>();
        }

        public Guid Id { get; set; }
        public string ProcessType { get; set; }
        public string IssueNo { get; set; }
        public string Title { get; set; }
        public int? Rpn { get; set; }
        public string Severity { get; set; }
        public int? RepeateddSymptom { get; set; }
        public string FailureDesc { get; set; }
        public int? FileAttack { get; set; }
        public string NotifiedList { get; set; }
        public string IssueStatus { get; set; }
        public string CurrentStep { get; set; }
        public string StepStatus { get; set; }
        public string ContainmentAction { get; set; }
        public string AnalysisDetail { get; set; }
        public string RecommendedAction { get; set; }
        public string EscapeCause { get; set; }
        public string Capadetail { get; set; }
        public string VerifyNote { get; set; }

        public virtual ICollection<TblApproval> TblApproval { get; set; }
        public virtual ICollection<TblAssign> TblAssign { get; set; }
        public virtual ICollection<TblAudit> TblAudit { get; set; }
        public virtual ICollection<TblFile> TblFile { get; set; }
        public virtual ICollection<TblIt> TblIt { get; set; }
        public virtual ICollection<TblOba> TblOba { get; set; }
        public virtual ICollection<TblProduct> TblProduct { get; set; }
        public virtual ICollection<TblScraptCost> TblScraptCost { get; set; }
        public virtual ICollection<TblVerification> TblVerification { get; set; }
    }
}
