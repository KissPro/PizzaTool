using System;
using System.Collections.Generic;

namespace Pizza.Data.EF
{
    public partial class TblScraptCost
    {
        public Guid Id { get; set; }
        public Guid IssueId { get; set; }
        public string PartName { get; set; }
        public long? ScapQty { get; set; }
        public decimal? ScapCost { get; set; }

        public virtual TblIssue Issue { get; set; }
    }
}
