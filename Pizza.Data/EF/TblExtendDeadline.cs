using System;
using System.Collections.Generic;

namespace Pizza.Data.EF
{
    public partial class TblExtendDeadline
    {
        public Guid Id { get; set; }
        public Guid AssignNo { get; set; }
        public DateTime CurrentDeadLine { get; set; }
        public DateTime RequestDeadLine { get; set; }
        public string Reason { get; set; }
        public string ApprovalContent { get; set; }
        public string Status { get; set; }
        public DateTime RequestDate { get; set; }
        public DateTime? ApprovalDate { get; set; }
    }
}

