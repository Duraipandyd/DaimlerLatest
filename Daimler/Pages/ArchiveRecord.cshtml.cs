using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Daimler.Controllers;
using Daimler.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static Daimler.Controllers.ArchiveRecordsController;

namespace Daimler.Pages
{
    public class ArchiveRecordModel : PageModel
    {
        public List<ArchiveRecordDto>  archiveRecordDtos { get; set; }

        public void OnGet()
        {
            DaimlerContext _context = new DaimlerContext();

            var archiveRecords = new ArchiveRecordsController(_context);
            var archiveDetails = archiveRecords.GetArchiveRecords("", "", "", "");
            archiveRecordDtos = archiveDetails;
        }
            public void OnGetArchive(string dprnumber, string dprdate, string boenumber, string boedate)
        {
            DaimlerContext _context = new DaimlerContext();

            var archiveRecords = new ArchiveRecordsController(_context);
            var archiveDetails = archiveRecords.GetArchiveRecords(dprnumber, dprdate, boenumber, boedate);
            archiveRecordDtos = archiveDetails;
            
        }
    }
}
