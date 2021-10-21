using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Daimler.Models;
using Daimler.Services;
using System.Data;

namespace Daimler.Controllers
{
    public class ArchiveRecordsController : Controller
    {
        private readonly DaimlerContext _context;

        public ArchiveRecordsController(DaimlerContext context)
        {
            _context = context;
        }

        [HttpGet]
        public List<ArchiveRecordDto> GetArchiveRecords(string DprNo, string DPRDate, string boeNo, string boeDate)
        {

            CommonFunction commonfunction = new CommonFunction();
            DataTable dsExceLData = new DataTable("dsExceLData");
            
            List<ArchiveRecordDto> ArchiveRecordDtos = new List<ArchiveRecordDto>();


            var SQL = @"Select Distinct DutyPaymentRequestDetail.ID,DutyPaymentRequestDetail.BOENo, (ISNULL(DutyPaymentRequestDetail.BOEDuty,0)) AS BOEDuty, 
						(Select SUM(ISNULL(CHADTDetail.BasicDuty,0))+ SUM(ISNULL(CHADTDetail.SocialWelfareSurchargeAmount_Customs,0)) + SUM(ISNULL(CHADTDetail.IGSTAmount,0)) from CHADTDetail where CHADTDetail.BOEID=DutyPaymentRequestDetail.HeaderID) AS IDTDuty,
                         (ISNULL(DutyPaymentRequestDetail.Fine,0)) as Fine, (ISNULL(DutyPaymentRequestDetail.Penalty,0)) AS Penalty, (ISNULL(DutyPaymentRequestDetail.Interest,0)) AS Interest,
                        ISNULL(AttachmentFileList.FileName,'') As ISCExcelAttachmentID,ISNULL(A.FileName,'') As ISCPDFAttachmentID,
						ISNULL(DutyPaymentRequestDetail.InvoiceNo,'') AS InvoiceNo,ISNULL(DutyPaymentRequestDetail.RefNo,'') AS RefNo,
						ISNULL(DutyPaymentRequestDetail.PortCode,'') AS PortCode
                        from DutyPaymentRequestDetail  
                        Join DutyPaymentRequestHeader on DutyPaymentRequestHeader.ID=DutyPaymentRequestDetail.HeaderID 
                        join CHAISCDetail On CHAISCDetail.BOEID=DutyPaymentRequestDetail.HeaderID  
                        left join CHADTDetail On CHADTDetail.BOEID=DutyPaymentRequestDetail.HeaderID  
                        left join AttachmentFileList On AttachmentFileList.ID=DutyPaymentRequestDetail.ISC_Excel_AttachmentID
                        left join AttachmentFileList A On A.ID=DutyPaymentRequestDetail.ISC_PDF_AttachmentID
                        where 1=1";
            if(DprNo!="" && DprNo != null)
            {
                SQL += " AND DutyPaymentRequestHeader.DPRNo='"+ DprNo + "'";
            }
            if (DPRDate != "" && DPRDate != null)
            {
                SQL += " AND DutyPaymentRequestHeader.UploadedDate='" +Convert.ToDateTime( DPRDate).ToString("yyyy-MM-dd") + "'";
            }
            if (boeNo != "" && boeNo != null)
            {
                SQL += " AND DutyPaymentRequestDetail.BOENo='" + boeNo + "'";
            }
            if (boeDate != "" && boeDate != null)
            {
                SQL += " AND CHAISCDetail.BEDate='" + Convert.ToDateTime(boeDate).ToString("yyyy-MM-dd") + "'";
            }

            var dtarchiveRecord = commonfunction.ExecuteSelectSQL(SQL);
            if (dtarchiveRecord == null) return  ArchiveRecordDtos;
            if (dtarchiveRecord.Rows.Count == 0) return ArchiveRecordDtos;
            for (int i = 0; i < dtarchiveRecord.Rows.Count; i++)
            {
                ArchiveRecordDto ArchiveRecordDto = new ArchiveRecordDto();
                ArchiveRecordDto.Id = CommonFunction.NullToIntZero(dtarchiveRecord.Rows[i]["ID"]);
                ArchiveRecordDto.BoeNo = CommonFunction.NullToEmpty(dtarchiveRecord.Rows[i]["BOENo"]);
                ArchiveRecordDto.BoeDuty = CommonFunction.NullToDecimalZero(dtarchiveRecord.Rows[i]["BoeDuty"]);
                ArchiveRecordDto.DutyValue = CommonFunction.NullToDecimalZero(dtarchiveRecord.Rows[i]["IDTDuty"]);
                ArchiveRecordDto.Fine = CommonFunction.NullToDecimalZero(dtarchiveRecord.Rows[i]["Fine"]);
                ArchiveRecordDto.Penalty = CommonFunction.NullToDecimalZero(dtarchiveRecord.Rows[i]["Penalty"]);
                ArchiveRecordDto.Interset = CommonFunction.NullToDecimalZero(dtarchiveRecord.Rows[i]["Interest"]);
                ArchiveRecordDto.InvoiceNo = CommonFunction.NullToEmpty(dtarchiveRecord.Rows[i]["InvoiceNo"]);
                ArchiveRecordDto.RefNo = CommonFunction.NullToEmpty(dtarchiveRecord.Rows[i]["RefNo"]);
                ArchiveRecordDto.PortCode = CommonFunction.NullToEmpty(dtarchiveRecord.Rows[i]["PortCode"]);
                if (CommonFunction.NullToEmpty(dtarchiveRecord.Rows[i]["ISCExcelAttachmentID"]) != "")
                {
                    ArchiveRecordDto.CHAISCAttachmentPath = "/assets/uploads/" + CommonFunction.NullToEmpty(dtarchiveRecord.Rows[i]["ISCExcelAttachmentID"]);
                }
                else
                {
                    ArchiveRecordDto.CHAISCAttachmentPath = "";
                }
                if (CommonFunction.NullToEmpty(dtarchiveRecord.Rows[i]["ISCPDFAttachmentID"]) != "")
                {
                    ArchiveRecordDto.BOEAttachmentPath = "/assets/uploads/" + CommonFunction.NullToEmpty(dtarchiveRecord.Rows[i]["ISCPDFAttachmentID"]);
                }
                else
                {
                    ArchiveRecordDto.BOEAttachmentPath = "0";
                }
                ArchiveRecordDto.CHAITDAttachmentPathList = GetAttachmentDetails(ArchiveRecordDto.Id);

                ArchiveRecordDtos.Add(ArchiveRecordDto);
            }


            return ArchiveRecordDtos;

        }
        public List<AttachmentDetails> GetAttachmentDetails(int detailID)
        {
            CommonFunction commonfunction = new CommonFunction();
            DataTable dsExceLData = new DataTable("dsExceLData");
            List<AttachmentDetails> attachmentDetaillist = new List<AttachmentDetails>();
            try
            {
                var SQL = @"Select FileName from AttachmentFileList
                            Join DutyPaymentRequestDetail ON DutyPaymentRequestDetail.ID =AttachmentFileList.SourceID
                            where Type='IDT' and AttachmentFileList.SourceID=" + detailID;
                var attachmentDetails = commonfunction.ExecuteSelectSQL(SQL);

                if (attachmentDetails == null) return attachmentDetaillist;
                if (attachmentDetails.Rows.Count == 0) return attachmentDetaillist;
                foreach (DataRow attachmentitem in attachmentDetails.Rows)
                {
                    var attachmentdetail = new AttachmentDetails();
                    attachmentdetail.FileName = "/assets/uploads/" + CommonFunction.NullToEmpty(attachmentitem["FileName"]);
                    attachmentDetaillist.Add(attachmentdetail);
                }

                return attachmentDetaillist;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public class AttachmentDetails
        {
            public string FileName { get; set; }
            public string FileLocation { get; set; }
        }
        public class ArchiveRecordDto
        {
            public int Id { get; set; }
            public string DprNo { get; set; }
            public DateTime? DprDate { get; set; }
            public string BoeNo { get; set; }
            public DateTime? BoeDate { get; set; }
            public decimal? BoeDuty { get; set; }
            public decimal? DutyValue { get; set; }
            public decimal? Fine { get; set; }
            public decimal? Penalty { get; set; }
            public decimal? Interset { get; set; }
            public int? CHAISC { get; set; }
            public int? BOE { get; set; }
            public string CHAITD { get; set; }
            public string BOEAttachmentPath { get; set; }
            public string CHAISCAttachmentPath { get; set; }
            public List<AttachmentDetails> CHAITDAttachmentPathList { get; set; }
            public string InvoiceNo { get; set; }
            public string RefNo { get; set; }
            public string PortCode { get; set; }


        }
    }
}
