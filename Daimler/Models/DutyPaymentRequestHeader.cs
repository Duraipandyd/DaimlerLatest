using System;
using System.Collections.Generic;

#nullable disable

namespace Daimler.Models
{
    public partial class DutyPaymentRequestHeader
    {
        public DutyPaymentRequestHeader()
        {
            DutyPaymentRequestDetails = new HashSet<DutyPaymentRequestDetail>();
        }

        public int Id { get; set; }
        public string Dprno { get; set; }
        public string FileName { get; set; }
        public DateTime? UploadedDate { get; set; }
        public int? UploadedBy { get; set; }
        public string DocumentReference { get; set; }
        public string Status { get; set; }

        public virtual ICollection<DutyPaymentRequestDetail> DutyPaymentRequestDetails { get; set; }
    }
}
