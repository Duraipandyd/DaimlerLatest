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
    public class AttachmentFileListController : Controller
    {
        private readonly DaimlerContext _context;

        public AttachmentFileListController(DaimlerContext context)
        {
            _context = context;
        }

        // GET: AttachmentFileList
        public async Task<IActionResult> Index()
        {
            return View(await _context.AttachmentFileLists.ToListAsync());
        }

        // GET: AttachmentFileList/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attachmentFileList = await _context.AttachmentFileLists
                .FirstOrDefaultAsync(m => m.Id == id);
            if (attachmentFileList == null)
            {
                return NotFound();
            }

            return View(attachmentFileList);
        }

        // GET: AttachmentFileList/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AttachmentFileList/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AttachmentFileList attachmentFileList)
        {
            if (ModelState.IsValid)
            {
                _context.Add(attachmentFileList);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(attachmentFileList);
        }

        // GET: AttachmentFileList/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attachmentFileList = await _context.AttachmentFileLists.FindAsync(id);
            if (attachmentFileList == null)
            {
                return NotFound();
            }
            return View(attachmentFileList);
        }

        // POST: AttachmentFileList/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Type,SourceId,FileName,Binary,Extension,FileLocation,UserId")] AttachmentFileList attachmentFileList)
        {
            if (id != attachmentFileList.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(attachmentFileList);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AttachmentFileListExists(attachmentFileList.Id))
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
            return View(attachmentFileList);
        }

        // GET: AttachmentFileList/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attachmentFileList = await _context.AttachmentFileLists
                .FirstOrDefaultAsync(m => m.Id == id);
            if (attachmentFileList == null)
            {
                return NotFound();
            }

            return View(attachmentFileList);
        }

        // POST: AttachmentFileList/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var attachmentFileList = await _context.AttachmentFileLists.FindAsync(id);
            _context.AttachmentFileLists.Remove(attachmentFileList);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AttachmentFileListExists(int id)
        {
            return _context.AttachmentFileLists.Any(e => e.Id == id);
        }
    }
}
