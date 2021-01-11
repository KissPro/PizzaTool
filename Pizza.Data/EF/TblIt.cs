using System;
using System.Collections.Generic;

namespace Pizza.Data.EF
{
    public partial class TblIt
    {
        public Guid Id { get; set; }
        public Guid IssueId { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string CustomerService { get; set; }

        public virtual TblIssue Issue { get; set; }
    }
}
