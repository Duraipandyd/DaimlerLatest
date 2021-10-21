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
    public class UploadidtModel : PageModel
    {
        public IList<IDTlist> idtlist { get; set; }
        public IDtDashboard idtDashboard { get; set; }
        public void OnGet()
        {
            DaimlerContext _context = new DaimlerContext();

            var dutyPayment = new DutyPaymentRequestHeaderController(_context);
            idtDashboard = dutyPayment.GetIDTRecords();
            idtlist = idtDashboard.Idtlist;
        }
    }
}
