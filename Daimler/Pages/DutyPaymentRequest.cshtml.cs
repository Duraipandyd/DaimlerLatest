using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Daimler.Controllers;
//using Daimler.Controllers;
using Daimler.Models;
using Daimler.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using static Daimler.Controllers.DutyPaymentRequestHeaderController;

namespace Daimler.Pages
{
    public class DutyPaymentRequest : PageModel
    {
        public DutyPaymentRequestHeader dutyPaymentRequestHeader { get; set; }
        [Obsolete]
        private IHostingEnvironment _environment;

        [Obsolete]
        public DutyPaymentRequest(IHostingEnvironment environment)
        {
            _environment = environment;
        }
        [BindProperty]
        public IFormFile Upload { get; set; }
         
        [Obsolete]
        public async Task OnPostAsync()
        {
            int loginId =CommonFunction.NullToIntZero(HttpContext.Session.GetString("LoginID"));

            if (Upload != null)
            {
                if(HttpContext.Session.GetString("filelist") == null)
                {
                    
                      var file = Path.Combine(_environment.WebRootPath, @"assets\uploads", Upload.FileName);
                    HttpContext.Session.SetString("filelist", file);
                    var fileextension = Path.GetExtension(file);
                    file = file.Replace(fileextension, "");
                    file = file.Remove(file.Length - 1, 1);
                    file = file + "_" + (Regex.Replace(DateTime.Now.ToString(), @"[^0-9a-zA-Z]+", "").Replace(":", "_").Replace(@"\", "_").Replace(" ", "")) + fileextension;
                    var filelist = Request.Headers["filelist"];
                    using (var fileStream = new FileStream(file, FileMode.Create))
                    {
                        await Upload.CopyToAsync(fileStream);
                    }
                    DaimlerContext _context = new DaimlerContext();
                    var dutyPaymentRequest = new DutyPaymentRequestHeaderService(_context);
                    var dutyPayment = new DutyPaymentRequestHeaderController(_context);
                    var result = dutyPaymentRequest.ReadDatafromExcelWorkSheet(file, loginId);

                    var ss = dutyPayment.Create(result);
                    
                    
                    
                }
                else  
                {
                    var dd= HttpContext.Session.GetString("filelist").Split(";");
                    
                    var file = Path.Combine(_environment.WebRootPath, @"assets\uploads", Upload.FileName);
                    var sessionvalue = HttpContext.Session.GetString("filelist");
                    HttpContext.Session.SetString("filelist", sessionvalue + ";" + file);
                    
                    if (!dd.Contains(file))
                    {
                        var fileextension = Path.GetExtension(file);
                        file = file.Replace(fileextension, "");
                        file = file.Remove(file.Length - 1, 1);
                        file = file + "_" + (Regex.Replace(DateTime.Now.ToString(), @"[^0-9a-zA-Z]+", "").Replace(":", "_").Replace(@"\", "_").Replace(" ", "")) + fileextension;
                        using (var fileStream = new FileStream(file, FileMode.Create))
                        {
                            await Upload.CopyToAsync(fileStream);
                        }
                        DaimlerContext _context = new DaimlerContext();
                        var dutyPaymentRequest = new DutyPaymentRequestHeaderService(_context);
                        var dutyPayment = new DutyPaymentRequestHeaderController(_context);
                        var result = dutyPaymentRequest.ReadDatafromExcelWorkSheet(file, loginId);

                        var ss = dutyPayment.Create(result);
                        
                        
                        
                    }
                    
                }
            }
            OnGet();
        }

        public IActionResult dutyPaymentRequestHeaderlist { get; set; }
        public IList<paymentlist> dutyPaymentRequestHeaders { get; set; }
        public void OnGet()
        {
            DaimlerContext _context = new DaimlerContext();

            var dutyPayment = new DutyPaymentRequestHeaderController(_context);
            dutyPaymentRequestHeaders = dutyPayment.GetDutyPaymentRequestHeader();

            //dutyPaymentRequestHeaders = (IEnumerable<DutyPaymentRequestHeader>)dutyPaymentRequestHeaderlist;
        }

        public void OnGetDetele(string id)
        {
            DaimlerContext _context = new DaimlerContext();

            var dutyPayment = new DutyPaymentRequestHeaderController(_context);
            dutyPayment.DeleteConfirmed(Convert.ToInt32(id));
            OnGet();
            //dutyPaymentRequestHeaders = (IEnumerable<DutyPaymentRequestHeader>)dutyPaymentRequestHeaderlist;
        }
    }
}
