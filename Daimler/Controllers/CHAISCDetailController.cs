using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Daimler.Models;
using Daimler.Services;

namespace Daimler.Controllers
{
    public class CHAISCDetailController : Controller
    {
        private readonly DaimlerContext _context;

        public CHAISCDetailController(DaimlerContext context)
        {
            _context = context;
        }

        // GET: CHAISCDetail
        public async Task<IActionResult> Index()
        {
            return View(await _context.Chaiscdetails.ToListAsync());
        }

        // GET: CHAISCDetail/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var CHAISCDetail = await _context.Chaiscdetails
                .FirstOrDefaultAsync(m => m.Id == id);
            if (CHAISCDetail == null)
            {
                return NotFound();
            }

            return View(CHAISCDetail);
        }

        // GET: CHAISCDetail/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CHAISCDetail/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Chaiscdetail CHAISCDetail)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(CHAISCDetail);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                return View(CHAISCDetail);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }


        [HttpPost]
        public IActionResult CreateBulk(List<Chaiscdetail> CHAISCDetail)
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

        // GET: CHAISCDetail/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var CHAISCDetail = await _context.Chaiscdetails.FindAsync(id);
            if (CHAISCDetail == null)
            {
                return NotFound();
            }
            return View(CHAISCDetail);
        }

        // POST: CHAISCDetail/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Chaiscdetail CHAISCDetail)
        {
            if (id != CHAISCDetail.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(CHAISCDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CHAISCDetailExists(CHAISCDetail.Id))
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
            return View(CHAISCDetail);
        }

        // GET: CHAISCDetail/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var CHAISCDetail = await _context.Chaiscdetails
                .FirstOrDefaultAsync(m => m.Id == id);
            if (CHAISCDetail == null)
            {
                return NotFound();
            }

            return View(CHAISCDetail);
        }

        // POST: CHAISCDetail/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var CHAISCDetail = await _context.Chaiscdetails.FindAsync(id);
            _context.Chaiscdetails.Remove(CHAISCDetail);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CHAISCDetailExists(int id)
        {
            return _context.Chaiscdetails.Any(e => e.Id == id);
        }

        public class BOERecordsList4ISC
        {
            public int Id { get; set; }
            public string Dprno { get; set; }
            public DateTime? DRPDate { get; set; }
            public decimal? DutyDPR { get; set; }
            public decimal? DutyISC { get; set; }
            public int BOERecords { get; set; }
            public int ISCRecords { get; set; }
           
            public List<BOERecordsDetails4ISC> boeRecordsDetails4ISC { get; set; }

        }

        public class BOERecordsDetails4ISC
        {
            public int Id { get; set; }
            public string BoeNo { get; set; }
            public decimal? BoeDuty { get; set; }
            public decimal? DutyValue { get; set; }
            public decimal? Fine { get; set; }
            public decimal? Penalty { get; set; }
            public decimal? Interest { get; set; }
            public int? CISISC { get; set; }
            public int? BOE { get; set; }
            public string RefNo { get; set; }
            public string InvoiceNo { get; set; }
            public string PortCode { get; set; }
            public string ISCRecordsAttachmentPath { get; set; }
            public string BOERecordsAttachmentPath { get; set; }

            public decimal? SocialWelfareSurchargeAmountCustoms { get; set; }
            public decimal? IGSTAmt { get; set; }

        }

        public class AttachmentDetails
        {
            public string FileName { get; set; }
            public string FileLocation { get; set; }
        }

        [HttpGet]
        public BOERecordsList4ISC GetISCRecords(int id)
        {

            var recordsList4ISC = new BOERecordsList4ISC();
            var BOERecordsDetails4ISC = new List<BOERecordsDetails4ISC>();
            var boeRecordCount = 0;
            var iscRecordCount = 0;

            recordsList4ISC = (from DutyPaymentRequestHeader in _context.DutyPaymentRequestHeaders
                               where DutyPaymentRequestHeader.Id == id

                               select new BOERecordsList4ISC
                               {
                                   Id = DutyPaymentRequestHeader.Id,
                                   Dprno = DutyPaymentRequestHeader.Dprno,
                                   DRPDate = DutyPaymentRequestHeader.UploadedDate
                               }
                                 ).Distinct().FirstOrDefault();

            var RecordDetailsList = (from DutyPaymentRequestDetail in _context.DutyPaymentRequestDetail
                                     where DutyPaymentRequestDetail.HeaderId == id

                                     select new BOERecordsDetails4ISC
                                     {
                                         Id =CommonFunction.NullToIntZero(DutyPaymentRequestDetail.Id),
                                         BoeNo = CommonFunction.NullToEmpty(DutyPaymentRequestDetail.Boeno),
                                         BoeDuty = CommonFunction.NullToDecimalZero(DutyPaymentRequestDetail.Boeduty),
                                         DutyValue = CommonFunction.NullToDecimalZero(DutyPaymentRequestDetail.DutyValue),
                                         Fine = CommonFunction.NullToDecimalZero(DutyPaymentRequestDetail.Fine),
                                         Interest = CommonFunction.NullToDecimalZero(DutyPaymentRequestDetail.Interest),
                                         InvoiceNo = CommonFunction.NullToEmpty(DutyPaymentRequestDetail.InvoiceNo),
                                         Penalty = CommonFunction.NullToDecimalZero(DutyPaymentRequestDetail.Penalty),
                                         PortCode = CommonFunction.NullToEmpty(DutyPaymentRequestDetail.PortCode),
                                         RefNo = CommonFunction.NullToEmpty(DutyPaymentRequestDetail.RefNo),
                                         BOE = CommonFunction.NullToIntZero(DutyPaymentRequestDetail.IscPdfAttachmentId),
                                         CISISC = CommonFunction.NullToIntZero(DutyPaymentRequestDetail.IscExcelAttachmentId)
                                         
                                     }
                                 ).Distinct().ToList();

            foreach (var details4ISC in RecordDetailsList)
            {
                
                if (CommonFunction.NullToIntZero(details4ISC.BOE) != 0)
                {
                    var attachment = (from AttachmentFileList in _context.AttachmentFileLists
                                      where AttachmentFileList.Id == details4ISC.BOE

                                      select new AttachmentDetails
                                      {
                                          FileLocation = AttachmentFileList.FileLocation,
                                          FileName = AttachmentFileList.FileName
                                      }).FirstOrDefault();
                    details4ISC.BOERecordsAttachmentPath = "/assets/uploads/" + attachment.FileName;
                    boeRecordCount += 1;

                }

                if (CommonFunction.NullToIntZero(details4ISC.CISISC) != 0)
                {
                    var attachment = (from AttachmentFileList in _context.AttachmentFileLists
                                      where AttachmentFileList.Id == details4ISC.CISISC

                                      select new AttachmentDetails
                                      {
                                          FileLocation = AttachmentFileList.FileLocation,
                                          FileName = AttachmentFileList.FileName
                                      }).FirstOrDefault();
                    details4ISC.ISCRecordsAttachmentPath = "/assets/uploads/" + attachment.FileName;
                    iscRecordCount += 1;
                }
            }

            var ISCDuty = (from Chaiscdetails in _context.Chaiscdetails
                                     where Chaiscdetails.Boeid == id

                                     select new BOERecordsDetails4ISC
                                     {
                                          DutyValue = Chaiscdetails.BasicDuty,
                                         SocialWelfareSurchargeAmountCustoms = CommonFunction.NullToDecimalZero(Chaiscdetails.SocialWelfareSurchargeAmountCustoms),
                                         IGSTAmt = CommonFunction.NullToDecimalZero(Chaiscdetails.Igstamount),
                                     }
                                 ).ToList();

            recordsList4ISC.boeRecordsDetails4ISC = RecordDetailsList;
            recordsList4ISC.BOERecords = RecordDetailsList.Count();
            recordsList4ISC.ISCRecords = iscRecordCount;
            recordsList4ISC.DutyDPR = RecordDetailsList.Sum(a => a.BoeDuty);
            recordsList4ISC.DutyISC = CommonFunction.NullToDecimalZero(ISCDuty.Sum(a => a.DutyValue) + ISCDuty.Sum(a => a.SocialWelfareSurchargeAmountCustoms)+ ISCDuty.Sum(a => a.IGSTAmt));

            return recordsList4ISC;

        }
       
    }
}
