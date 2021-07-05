using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Pizza.ViewModel.Common
{
    public class OBAVM
    {
        public long ID { get; set; }

        [StringLength(50)]
        public string PID { get; set; }

        [StringLength(50)]
        public string POINTS_ID { get; set; }

        [StringLength(50)]
        public string STATUS { get; set; }

        public string FAILURE_MODE_CODE { get; set; }

        public string FAILURE_MODE_DESC { get; set; }

        public string FAILURE_COMPONENT_CODE { get; set; }

        public string FAILURE_COMPONENT_DESC { get; set; }

        public string FAILURE_CLASSIFICATION_CODE { get; set; }

        public string FAILURE_CLASSIFICATION_DESC { get; set; }

        public string FAIL_DESC { get; set; }

        public string FAIL_DESC2 { get; set; }

        public string FLAG { get; set; }

        [StringLength(50)]
        public string FAMILY { get; set; }

        public string FLAG9 { get; set; }

        public DateTime? CREATED_DATE { get; set; }

        public DateTime? UPDATED_DATE { get; set; }

        [StringLength(50)]
        public string SUPERVISOR { get; set; }

        [StringLength(50)]
        public string UNLOCKER { get; set; }

        [StringLength(50)]
        public string AUDITOR { get; set; }
        [StringLength(50)]
        public string LINE { get; set; }
        public string FileName { get; set; }
        public string Url { get; set; }
    }


    public class OBAFailViewModel
    {
        public string LINE { get; set; }
        public string PID { get; set; }
        public int? tt_fail { get; set; }
        public int? tt_unlock { get; set; }
    }

    public class OBARequest
    {
        public string Factory { get; set; }
        public DateTime Date { get; set; }
        public bool? Npi { get; set; }
    }
}
