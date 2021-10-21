using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Daimler.Controllers;
using Daimler.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static Daimler.Controllers.DutyPaymentRequestHeaderController;

namespace Daimler.Pages
{
    public class UploadiscModel : PageModel
    {
        public IList<ISClist> isclist { get; set; }
        public ISCDashboard iSCDashboard { get; set; }
        public void OnGet()
        {
            DaimlerContext _context = new DaimlerContext();

            var dutyPayment = new DutyPaymentRequestHeaderController(_context);
            iSCDashboard = dutyPayment.GetISCRecords();
            isclist = iSCDashboard.ISClist;
        }
    }
}
