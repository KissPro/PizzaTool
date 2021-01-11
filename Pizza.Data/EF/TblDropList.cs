using System;
using System.Collections.Generic;

namespace Pizza.Data.EF
{
    public partial class TblDropList
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string DropListRemark { get; set; }
    }
}
