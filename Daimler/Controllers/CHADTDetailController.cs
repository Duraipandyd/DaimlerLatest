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
using static Daimler.Controllers.DutyPaymentRequestHeaderController;

namespace Daimler.Controllers
{
    public class CHADTDetailController : Controller
    {
        private readonly DaimlerContext _context;

        public CHADTDetailController(DaimlerContext context)
        {
            _context = context;
        }

        // GET: CHADTDetails
        public async Task<IActionResult> Index()
        {
            return View(await _context.Chadtdetails.ToListAsync());
        }

        // GET: CHADTDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var CHADTDetail = await _context.Chadtdetails
                .FirstOrDefaultAsync(m => m.Id == id);
            if (CHADTDetail == null)
            {
                return NotFound();
            }

            return View(CHADTDetail);
        }

        // GET: CHADTDetails/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CHADTDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Chadtdetail CHADTDetail)
        {
            if (ModelState.IsValid)
            {
                _context.Add(CHADTDetail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(CHADTDetail);
        }

        // GET: CHADTDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var CHADTDetail = await _context.Chadtdetails.FindAsync(id);
            if (CHADTDetail == null)
            {
                return NotFound();
            }
            return View(CHADTDetail);
        }

        // POST: CHADTDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Chadtdetail CHADTDetail )
        {
            if (id != CHADTDetail.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(CHADTDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CHADTDetailExists(CHADTDetail.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(CHADTDetail);
        }

        // GET: CHADTDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var CHADTDetail = await _context.Chadtdetails
                .FirstOrDefaultAsync(m => m.Id == id);
            if (CHADTDetail == null)
            {
                return NotFound();
            }

            return View(CHADTDetail);
        }

        // POST: CHADTDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var CHADTDetail = await _context.Chadtdetails.FindAsync(id);
            _context.Chadtdetails.Remove(CHADTDetail);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CHADTDetailExists(int id)
        {
            return _context.Chadtdetails.Any(e => e.Id == id);
        }

        [HttpPost]
        public IActionResult CreateBulk(List<Chadtdetail> CHAISCDetail)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.AddRange(CHAISCDetail);
                    _context.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
                return View(CHAISCDetail);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public class BOERecordsList4IDT
        {
            public int Id { get; set; }
            public string Dprno { get; set; }
            public string DRPDate { get; set; }
            public decimal? DutyDPR { get; set; }
            public decimal? DutyISC { get; set; }
            public decimal? DutyIDT { get; set; }
            public int BOERecords { get; set; }
            public int ISCRecords { get; set; }
            public int IDTRecords { get; set; }
            public List<BOERecordsDetails4IDT> boeRecordsDetails4IDT { get; set; }


        }

        public class BOERecordsDetails4IDT
        {
            public int Id { get; set; }
            public string BoeNo { get; set; }
            public decimal? BoeDuty { get; set; }
            public decimal? DutyValue { get; set; }
            public decimal? Fine { get; set; }
            public decimal? Penalty { get; set; }
            public decimal? Interest { get; set; }
            public int? CHAIDT { get; set; }
            public string RefNo { get; set; }
            public string InvoiceNo { get; set; }
            public string PortCode { get; set; }
            public string ISCRecordsAttachmentPath { get; set; }
            public string BOERecordsAttachmentPath { get; set; }
            public string IDTRecordsAttachmentPath { get; set; }
            public int? ISCRecordsAttachmentPathID { get; set; }
            public int? BOERecordsAttachmentPathID { get; set; }
            public string IDTRecordsAttachmentPathID { get; set; }
            public string Dprno { get; set; }
            public string DRPDate { get; set; }
            public decimal? DutyDPR { get; set; }
            public decimal? DutyISC { get; set; }
            public decimal? DutyIDT { get; set; }
            public int BOERecords { get; set; }
            public int ISCRecords { get; set; }
            public int IDTRecords { get; set; }
            public List<AttachmentDetails> IDTExcelAttachmentDetails { get; set; }
        }

        public class AttachmentDetails
        {
            public string FileName { get; set; }
            public string FileLocation { get; set; }
        }

        [HttpGet]
        public BOERecordsList4IDT GetIDTRecords(int id)
        {


            CommonFunction commonfunction = new CommonFunction();
            DataTable dsExceLData = new DataTable("dsExceLData");
            BOERecordsList4IDT idtDashboarddto = new BOERecordsList4IDT();
            List<BOERecordsDetails4IDT> Idtlist = new List<BOERecordsDetails4IDT>();


            var SQL = @"SELECT DISTINCT DutyPaymentRequestDetail.ID,DutyPaymentRequestDetail.BOENo,DutyPaymentRequestHeader.DPRNo,
                        DutyPaymentRequestHeader.UploadedDate, (ISNULL(DutyPaymentRequestDetail.BOEDuty,0)) AS BOEDuty,
                        
                          ISNULL((SELECT SUM(ISNULL(CHAISCDetail.BasicDuty,0))+ SUM(ISNULL(CHAISCDetail.SocialWelfareSurchargeAmount_Customs,0)) 
                          + SUM(ISNULL(CHAISCDetail.IGSTAmount,0)) FROM CHAISCDetail 
                          where CHAISCDetail.BOEID=DutyPaymentRequestHeader.ID  AND CHAISCDetail.BENo=DutyPaymentRequestDetail.BOENo ),0) AS ISCDuty,
                        
                        ISNULL((SELECT SUM(ISNULL(CHADTDetail.BasicDuty,0))+ SUM(ISNULL(CHADTDetail.SocialWelfareSurchargeAmount_Customs,0)) 
                        + SUM(ISNULL(CHADTDetail.IGSTAmount,0)) FROM CHADTDetail where CHADTDetail.BOEID=DutyPaymentRequestDetail.HeaderID
                        AND CHADTDetail.BENO_IDT=DutyPaymentRequestDetail.BOENo),0) AS IDTDuty,
                        
                           (ISNULL(DutyPaymentRequestDetail.Fine,0)) as Fine, (ISNULL(DutyPaymentRequestDetail.Penalty,0)) AS Penalty,
						   (ISNULL(DutyPaymentRequestDetail.Interest,0)) AS Interest,
                          (SELECT COUNT(DPdetail.ID) FROM DutyPaymentRequestDetail as DPdetail where DPdetail.HeaderID=DutyPaymentRequestHeader.ID) AS BOErocrds, 
                        
                          (SELECT COUNT(ISCdetail.ID) FROM DutyPaymentRequestDetail as ISCdetail WHERE ISNULL(ISC_Excel_AttachmentID,0)!=0 AND 
                                            ISCdetail.HeaderID=DutyPaymentRequestHeader.ID) AS ISCrocrds,
                        
                          (SELECT COUNT(IDTdetail.ID) FROM DutyPaymentRequestDetail as IDTdetail where ISNULL(IDT_Excel_AttachmentID,0)!=0 AND
						  IDTdetail.HeaderID=DutyPaymentRequestHeader.ID) AS IDTrocrds,

                          ISNULL(AttachmentFileList.FileName,'') As ISCExcelAttachmentID,ISNULL(A.FileName,'') As ISCPDFAttachmentID
                          FROM DutyPaymentRequestDetail  
                        
                          JOIN DutyPaymentRequestHeader on DutyPaymentRequestHeader.ID=DutyPaymentRequestDetail.HeaderID 
                          JOIN CHAISCDetail On CHAISCDetail.BOEID=DutyPaymentRequestDetail.HeaderID  
                          LEFT JOIN CHADTDetail On CHADTDetail.BOEID=DutyPaymentRequestDetail.HeaderID  
                          LEFT JOIN AttachmentFileList On AttachmentFileList.ID=DutyPaymentRequestDetail.ISC_Excel_AttachmentID
                          LEFT JOIN AttachmentFileList A On A.ID=DutyPaymentRequestDetail.ISC_PDF_AttachmentID

                          WHERE DutyPaymentRequestHeader.ID=" + id;
                    

            var IDtDashboardrecords = commonfunction.ExecuteSelectSQL(SQL);
            if (IDtDashboardrecords == null) return new BOERecordsList4IDT();
            for (int i = 0; i < IDtDashboardrecords.Rows.Count; i++)
            {
                BOERecordsDetails4IDT Idtlistdto = new BOERecordsDetails4IDT();
                Idtlistdto.Id = CommonFunction.NullToIntZero(IDtDashboardrecords.Rows[i]["ID"]);
                Idtlistdto.BoeNo = CommonFunction.NullToEmpty(IDtDashboardrecords.Rows[i]["BoeNo"]);
                Idtlistdto.BoeDuty = CommonFunction.NullToDecimalZero(IDtDashboardrecords.Rows[i]["BoeDuty"]);
                Idtlistdto.DutyValue = CommonFunction.NullToDecimalZero(IDtDashboardrecords.Rows[i]["IDTDuty"]);
                Idtlistdto.Fine = CommonFunction.NullToDecimalZero(IDtDashboardrecords.Rows[i]["Fine"]);
                Idtlistdto.Penalty = CommonFunction.NullToDecimalZero(IDtDashboardrecords.Rows[i]["Penalty"]);
                Idtlistdto.Interest = CommonFunction.NullToDecimalZero(IDtDashboardrecords.Rows[i]["Interest"]);
                Idtlistdto.DutyISC = CommonFunction.NullToDecimalZero(IDtDashboardrecords.Rows[i]["ISCDuty"]);
                Idtlistdto.BOERecords = CommonFunction.NullToIntZero(IDtDashboardrecords.Rows[i]["BOErocrds"]);
                Idtlistdto.ISCRecords = CommonFunction.NullToIntZero(IDtDashboardrecords.Rows[i]["ISCrocrds"]);
                Idtlistdto.IDTRecords = CommonFunction.NullToIntZero(IDtDashboardrecords.Rows[i]["IDTrocrds"]);
                Idtlistdto.Dprno = CommonFunction.NullToEmpty(IDtDashboardrecords.Rows[i]["DPRNo"]);
                Idtlistdto.DRPDate = CommonFunction.NullToEmpty(IDtDashboardrecords.Rows[i]["UploadedDate"]);
                Idtlistdto.DutyDPR = CommonFunction.NullToDecimalZero(IDtDashboardrecords.Rows[i]["BOEDuty"]);
                if (CommonFunction.NullToEmpty(IDtDashboardrecords.Rows[i]["ISCExcelAttachmentID"]) != "")
                {
                    Idtlistdto.ISCRecordsAttachmentPath = "/assets/uploads/" + CommonFunction.NullToEmpty(IDtDashboardrecords.Rows[i]["ISCExcelAttachmentID"]);
                }
                else
                {
                    Idtlistdto.ISCRecordsAttachmentPath = "";
                }
                if (CommonFunction.NullToEmpty(IDtDashboardrecords.Rows[i]["ISCPDFAttachmentID"]) != "")
                {
                    Idtlistdto.BOERecordsAttachmentPath = "/assets/uploads/" + CommonFunction.NullToEmpty(IDtDashboardrecords.Rows[i]["ISCPDFAttachmentID"]);
                }
                else
                {
                    Idtlistdto.BOERecordsAttachmentPath = "0";
                }
                Idtlistdto.IDTExcelAttachmentDetails = GetAttachmentDetails(Idtlistdto.Id);

                Idtlist.Add(Idtlistdto);
            }

            idtDashboarddto.BOERecords = Idtlist.FirstOrDefault().BOERecords;
            idtDashboarddto.ISCRecords = Idtlist.FirstOrDefault().ISCRecords;
            idtDashboarddto.IDTRecords = Idtlist.FirstOrDefault().IDTRecords;
            idtDashboarddto.DutyISC = Idtlist.Sum(x => x.DutyISC);
            idtDashboarddto.DutyDPR = Idtlist.Sum(x => x.DutyDPR);
            idtDashboarddto.DutyIDT = Idtlist.Sum(x => x.DutyValue);
            idtDashboarddto.DRPDate = CommonFunction.NullToEmpty(IDtDashboardrecords.Rows[0]["UploadedDate"]);
            idtDashboarddto.Dprno = CommonFunction.NullToEmpty(IDtDashboardrecords.Rows[0]["DPRNo"]);

            idtDashboarddto.boeRecordsDetails4IDT = Idtlist;

            return idtDashboarddto;
            //var recordsList4ISC = new BOERecordsList4IDT();
            //var BOERecordsDetails4ISC = new List<BOERecordsDetails4IDT>();
            //var boeRecordCount = 0;
            //var iscRecordCount = 0;
            //var idtRecordCount = 0;

            //recordsList4ISC = (from DutyPaymentRequestHeader in _context.DutyPaymentRequestHeaders
            //                   where DutyPaymentRequestHeader.Id == id

            //                   select new BOERecordsList4IDT
            //                   {
            //                       Id = DutyPaymentRequestHeader.Id,
            //                       Dprno = DutyPaymentRequestHeader.Dprno,
            //                       DRPDate = DutyPaymentRequestHeader.UploadedDate
            //                   }
            //                 ).Distinct().FirstOrDefault();

            //var RecordDetailsList = (from DutyPaymentRequestDetail in _context.DutyPaymentRequestDetail
            //                         where DutyPaymentRequestDetail.HeaderId == id

            //                         select new BOERecordsDetails4IDT
            //                         {
            //                             Id = DutyPaymentRequestDetail.Id,
            //                             BoeNo = DutyPaymentRequestDetail.Boeno,
            //                             BoeDuty = DutyPaymentRequestDetail.Boeduty,
            //                             DutyValue = DutyPaymentRequestDetail.DutyValue,
            //                             Fine = DutyPaymentRequestDetail.Fine,
            //                             Interest = DutyPaymentRequestDetail.Interest,
            //                             InvoiceNo = DutyPaymentRequestDetail.InvoiceNo,
            //                             Penalty = DutyPaymentRequestDetail.Penalty,
            //                             PortCode = DutyPaymentRequestDetail.PortCode,
            //                             RefNo = DutyPaymentRequestDetail.RefNo,
            //                             BOERecordsAttachmentPathID = DutyPaymentRequestDetail.IscPdfAttachmentId,
            //                             IDTRecordsAttachmentPathID = DutyPaymentRequestDetail.IdtExcelAttachmentId,
            //                             ISCRecordsAttachmentPathID = DutyPaymentRequestDetail.IscExcelAttachmentId

            //                         }
            //                     ).ToList();

            //foreach (var details4ISC in RecordDetailsList)
            //{
            //    if (CommonFunction.NullToIntZero(details4ISC.BOERecordsAttachmentPathID) != 0)
            //    {
            //        var attachment = (from AttachmentFileList in _context.AttachmentFileLists
            //                          where AttachmentFileList.Id == details4ISC.BOERecordsAttachmentPathID

            //                          select new AttachmentDetails
            //                          {
            //                              FileLocation = AttachmentFileList.FileLocation,
            //                              FileName = AttachmentFileList.FileName
            //                          }).FirstOrDefault();
            //        details4ISC.BOERecordsAttachmentPath = "/assets/uploads/" + attachment.FileName;
            //        boeRecordCount += 1;
            //    }

            //    if (CommonFunction.NullToIntZero(details4ISC.ISCRecordsAttachmentPathID) != 0)
            //    {
            //        var attachment = (from AttachmentFileList in _context.AttachmentFileLists
            //                          where AttachmentFileList.Id == details4ISC.ISCRecordsAttachmentPathID

            //                          select new AttachmentDetails
            //                          {
            //                              FileLocation = AttachmentFileList.FileLocation,
            //                              FileName = AttachmentFileList.FileName
            //                          }).FirstOrDefault();
            //        details4ISC.ISCRecordsAttachmentPath = "/assets/uploads/" + attachment.FileName;
            //        iscRecordCount += 1;
            //    }

            //    if (CommonFunction.NullToIntZero(details4ISC.IDTRecordsAttachmentPathID) != 0)
            //    {
            //        var attachId = CommonFunction.NullToIntZero(details4ISC.IDTRecordsAttachmentPathID);
            //        var attachment = (from AttachmentFileList in _context.AttachmentFileLists
            //                          where AttachmentFileList.Id == attachId

            //                          select new AttachmentDetails
            //                          {
            //                              FileLocation = AttachmentFileList.FileLocation,
            //                              FileName = AttachmentFileList.FileName
            //                          }).FirstOrDefault();
            //        details4ISC.IDTRecordsAttachmentPath = "/assets/uploads/" + attachment.FileName;
            //        idtRecordCount += 1;
            //    }
            //}

            //var IscDetailsList = (from CHAISCDetail in _context.Chaiscdetails
            //                      join DutyPaymentRequestDetail in _context.DutyPaymentRequestDetail
            //             on CHAISCDetail.Beno equals DutyPaymentRequestDetail.Boeno
            //                      where DutyPaymentRequestDetail.HeaderId == id

            //                      select new BOERecordsDetails4IDT
            //                      {

            //                          DutyValue = CHAISCDetail.TotalDuty
            //                      }
            //                     ).ToList();

            //var IDtDetailsList = (from CHAISCDetail in _context.Chadtdetails
            //                      join DutyPaymentRequestDetail in _context.DutyPaymentRequestDetail
            //             on CHAISCDetail.Beno equals DutyPaymentRequestDetail.Boeno
            //                      where DutyPaymentRequestDetail.HeaderId == id

            //                      select new BOERecordsDetails4IDT
            //                      {

            //                          DutyValue = CHAISCDetail.TotalDuty
            //                      }
            //                     ).ToList();

            //recordsList4ISC.BOERecords = boeRecordCount;
            //recordsList4ISC.ISCRecords = iscRecordCount;
            //recordsList4ISC.DutyDPR = RecordDetailsList.Sum(a => a.DutyValue);
            //recordsList4ISC.DutyISC = IDtDetailsList.Sum(a => a.DutyValue);
            //recordsList4ISC.boeRecordsDetails4IDT = RecordDetailsList;
            //return recordsList4ISC;

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
                            where Type='IDT' and AttachmentFileList.SourceID="+ detailID;
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
    }
}
