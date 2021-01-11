using System;
using System.Collections.Generic;

namespace Pizza.Data.EF
{
    public partial class TblFile
    {
        public Guid Id { get; set; }
        public Guid IssueId { get; set; }
        public string CurrentStep { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string Remark { get; set; }
        public string UploadedBy { get; set; }
        public DateTime? UploadedDate { get; set; }

        public virtual TblIssue Issue { get; set; }
    }
}
