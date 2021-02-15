using System;
using System.Collections.Generic;

namespace Pizza.Data.EF
{
    public partial class TblProduct
    {
        public Guid Id { get; set; }
        public Guid IssueId { get; set; }
        public string Imei { get; set; }
        public string Customer { get; set; }
        public string Product { get; set; }
        public string Psn { get; set; }
        public string Ponno { get; set; }
        public int Ponsize { get; set; }
        public string Spcode { get; set; }
        public string Line { get; set; }
        public string Pattern { get; set; }
        public string Shift { get; set; }

        public virtual TblIssue Issue { get; set; }
    }
}
