using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Daimler.Models;

namespace Daimler.Controllers
{
    public class DutyPaymentRequestDetailController : Controller
    {
        DaimlerContext _context;

        public DutyPaymentRequestDetailController(DaimlerContext context)
        {
            _context = context;
        }

        // GET: DutyPaymentRequestDetail
        public async Task<IActionResult> Index()
        {
            var daimlerContext = _context.DutyPaymentRequestDetail.Include(d => d.Header).Include(d => d.IscExcelAttachment).Include(d => d.IscPdfAttachment).Include(d => d.IscPdfAttachment).Include(d => d.IdtExcelAttachmentId).Include(d => d.IdtExcelAttachmentId).Include(d => d.IdtPdfAttachment).Include(d => d.IscExcelAttachment).Include(d => d.IscPdfAttachment);
            return View(await daimlerContext.ToListAsync());
        }

        // GET: DutyPaymentRequestDetail/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dutyPaymentRequestDetail = await _context.DutyPaymentRequestDetail
                .Include(d => d.Header)
                .Include(d => d.IscExcelAttachmentId)
                .Include(d => d.IscPdfAttachmentId)
                .Include(d => d.IdtPdfAttachmentId)
                .Include(d => d.IdtExcelAttachmentId)  
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dutyPaymentRequestDetail == null)
            {
                return NotFound();
            }

            return View(dutyPaymentRequestDetail);
        }

        // GET: DutyPaymentRequestDetail/Create
        public IActionResult Create()
        {
            ViewData["HeaderId"] = new SelectList(_context.DutyPaymentRequestHeaders, "Id", "Id");
            ViewData["IscExcelAttachmentId"] = new SelectList(_context.AttachmentFileLists, "Id", "Id");
            ViewData["IscPdfAttachmentId"] = new SelectList(_context.AttachmentFileLists, "Id", "Id");
            ViewData["IdtPdfAttachmentId"] = new SelectList(_context.AttachmentFileLists, "Id", "Id");
            ViewData["IdtExcelAttachmentId"] = new SelectList(_context.AttachmentFileLists, "Id", "Id"); 
            return View();
        }

        // POST: DutyPaymentRequestDetail/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,HeaderId,LineId,DutyValue,RefNo,InvoiceNo,Boeno,PortCode,Boeduty,Fine,Penalty,Interest,IscExcelAttachmentId,IscPdfAttachmentId,IdtPdfAttachmentId,IscExcelAttachmentId,ISC_PDF_AttachmentID,IDT_PDF_AttachmentID,IDT_Excel_AttachmentID,IdtExcelAttachmentId5")] DutyPaymentRequestDetail dutyPaymentRequestDetail)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dutyPaymentRequestDetail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["HeaderId"] = new SelectList(_context.DutyPaymentRequestHeaders, "Id", "Id");
            ViewData["IscExcelAttachmentId"] = new SelectList(_context.AttachmentFileLists, "Id", "Id");
            ViewData["IscPdfAttachmentId"] = new SelectList(_context.AttachmentFileLists, "Id", "Id");
            ViewData["IdtPdfAttachmentId"] = new SelectList(_context.AttachmentFileLists, "Id", "Id");
            ViewData["IdtExcelAttachmentId"] = new SelectList(_context.AttachmentFileLists, "Id", "Id");
            return View(dutyPaymentRequestDetail);
        }

        // GET: DutyPaymentRequestDetail/Edit/5
        public async Task<ActionResult<DutyPaymentRequestDetail>> Edit(int? id)
        {
            if (id == null)
            {
                return null;
            }

            var dutyPaymentRequestDetail = await _context.DutyPaymentRequestDetail.FindAsync(id);
            if (dutyPaymentRequestDetail == null)
            {
                return null;
            }
            ViewData["HeaderId"] = new SelectList(_context.DutyPaymentRequestHeaders, "Id", "Id");
            ViewData["IscExcelAttachmentId"] = new SelectList(_context.AttachmentFileLists, "Id", "Id");
            ViewData["IscPdfAttachmentId"] = new SelectList(_context.AttachmentFileLists, "Id", "Id");
            ViewData["IdtPdfAttachmentId"] = new SelectList(_context.AttachmentFileLists, "Id", "Id");
            ViewData["IdtExcelAttachmentId"] = new SelectList(_context.AttachmentFileLists, "Id", "Id");
            return dutyPaymentRequestDetail;
        }

        // POST: DutyPaymentRequestDetail/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, DutyPaymentRequestDetail dutyPaymentRequestDetail)
        {
            if (id != dutyPaymentRequestDetail.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dutyPaymentRequestDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DutyPaymentRequestDetailExists(dutyPaymentRequestDetail.Id))
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
            ViewData["HeaderId"] = new SelectList(_context.DutyPaymentRequestHeaders, "Id", "Id");
            ViewData["IscExcelAttachmentId"] = new SelectList(_context.AttachmentFileLists, "Id", "Id");
            ViewData["IscPdfAttachmentId"] = new SelectList(_context.AttachmentFileLists, "Id", "Id");
            ViewData["IdtPdfAttachmentId"] = new SelectList(_context.AttachmentFileLists, "Id", "Id");
            ViewData["IdtExcelAttachmentId"] = new SelectList(_context.AttachmentFileLists, "Id", "Id");
            return View(dutyPaymentRequestDetail);
        }

        // GET: DutyPaymentRequestDetail/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dutyPaymentRequestDetail = await _context.DutyPaymentRequestDetail
                .Include(d => d.Header)
                .Include(d => d.IscExcelAttachmentId)
                .Include(d => d.IscPdfAttachmentId)
                .Include(d => d.IdtPdfAttachmentId)
                .Include(d => d.IdtExcelAttachmentId)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dutyPaymentRequestDetail == null)
            {
                return NotFound();
            }

            return View(dutyPaymentRequestDetail);
        }

        // POST: DutyPaymentRequestDetail/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dutyPaymentRequestDetail = await _context.DutyPaymentRequestDetail.FindAsync(id);
            _context.DutyPaymentRequestDetail.Remove(dutyPaymentRequestDetail);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DutyPaymentRequestDetailExists(int id)
        {
            return _context.DutyPaymentRequestDetail.Any(e => e.Id == id);
        }
    }
}
