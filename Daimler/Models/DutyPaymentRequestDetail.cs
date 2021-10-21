using System;
using System.Collections.Generic;

#nullable disable

namespace Daimler.Models
{
    public partial class DutyPaymentRequestDetail
    {
        public int Id { get; set; }
        public int HeaderId { get; set; }
        public int LineId { get; set; }
        public decimal? DutyValue { get; set; }
        public string RefNo { get; set; }
        public string InvoiceNo { get; set; }
        public string Boeno { get; set; }
        public string PortCode { get; set; }
        public decimal? Boeduty { get; set; }
        public decimal? Fine { get; set; }
        public decimal? Penalty { get; set; }
        public decimal? Interest { get; set; }
        public int? IscExcelAttachmentId { get; set; }
        public int? IscPdfAttachmentId { get; set; }
        public int? IdtPdfAttachmentId { get; set; }
        public string IdtExcelAttachmentId { get; set; }

        public virtual DutyPaymentRequestHeader Header { get; set; }
        public virtual AttachmentFileList IdtPdfAttachment { get; set; }
        public virtual AttachmentFileList IscExcelAttachment { get; set; }
        public virtual AttachmentFileList IscPdfAttachment { get; set; }
    }
}
