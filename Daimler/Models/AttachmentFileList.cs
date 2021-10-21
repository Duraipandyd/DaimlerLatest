using System;
using System.Collections.Generic;

#nullable disable

namespace Daimler.Models
{
    public partial class AttachmentFileList
    {
        public AttachmentFileList()
        {
            DutyPaymentRequestDetailIdtPdfAttachments = new HashSet<DutyPaymentRequestDetail>();
            DutyPaymentRequestDetailIscExcelAttachments = new HashSet<DutyPaymentRequestDetail>();
            DutyPaymentRequestDetailIscPdfAttachments = new HashSet<DutyPaymentRequestDetail>();
        }

        public int Id { get; set; }
        public string Type { get; set; }
        public string SourceId { get; set; }
        public string FileName { get; set; }
        public byte[] Binary { get; set; }
        public string Extension { get; set; }
        public string FileLocation { get; set; }
        public int? UserId { get; set; }

        public virtual ICollection<DutyPaymentRequestDetail> DutyPaymentRequestDetailIdtPdfAttachments { get; set; }
        public virtual ICollection<DutyPaymentRequestDetail> DutyPaymentRequestDetailIscExcelAttachments { get; set; }
        public virtual ICollection<DutyPaymentRequestDetail> DutyPaymentRequestDetailIscPdfAttachments { get; set; }
    }
}
