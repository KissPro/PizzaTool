using System;
using System.Collections.Generic;

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

        public virtual TblIssue Issue { get; set; }
    }
}
