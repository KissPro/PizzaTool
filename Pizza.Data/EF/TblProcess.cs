using System;
using System.Collections.Generic;

namespace Pizza.Data.EF
{
    public partial class TblProcess
    {
        public Guid Id { get; set; }
        public string ProcessName { get; set; }
        public string RefTable { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string ProcessRemark { get; set; }
    }
}
