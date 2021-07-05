using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime;

namespace Pizza.Data.EF
{
    public partial class TblOba
    {
        public Guid Id { get; set; }
        public Guid IssueId { get; set; }
        public DateTime DetectingTime { get; set; }
        public string DefectPart { get; set; }
        public string DefectName { get; set; }
        public string DefectType { get; set; }
        public int? SamplingQty { get; set; }
        public string NgphoneOrdinal { get; set; }

        // Hoangnv - 2021.06.16: add by a.Linh request
        public string Supervisor { get; set; }
        public string Auditor { get; set; }
        public string DetectBy { get; set; }
        public string HowToDetect { get; set; }
        public string FailureValidate { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public virtual TblIssue Issue { get; set; }
    }
}
