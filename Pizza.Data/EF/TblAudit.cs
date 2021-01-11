using System;
using System.Collections.Generic;

namespace Pizza.Data.EF
{
    public partial class TblAudit
    {
        public Guid Id { get; set; }
        public Guid IssueId { get; set; }
        public string AuditType { get; set; }
        public string RelatedCapa { get; set; }
        public string Auditor { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }

        public virtual TblIssue Issue { get; set; }
    }
}
