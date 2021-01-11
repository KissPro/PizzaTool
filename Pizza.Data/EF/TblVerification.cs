using System;
using System.Collections.Generic;

namespace Pizza.Data.EF
{
    public partial class TblVerification
    {
        public Guid Id { get; set; }
        public Guid IssueId { get; set; }
        public string Ponno { get; set; }
        public long? Size { get; set; }
        public long? Ngqty { get; set; }
        public string Ngrate { get; set; }
        public string Judgment { get; set; }
        public DateTime? Date { get; set; }

        public virtual TblIssue Issue { get; set; }
    }
}
