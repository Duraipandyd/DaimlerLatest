using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Daimler.Controllers;
using Daimler.Models;
using Daimler.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static Daimler.Controllers.CHAISCDetailController;

namespace Daimler.Pages
{
    public class UploadIscListModel : PageModel
    {
        private IHostingEnvironment _environment;

        [Obsolete]
        public UploadIscListModel(IHostingEnvironment environment)
        {
            _environment = environment;
        }
        [BindProperty]
        public IFormFile Upload { get; set; }
       
        
        [Obsolete]
        public async Task OnPostAsync(string id)
        {
            int iscId =Convert.ToInt32( Request.Form["iscId"]);
            var boeNo = Request.Form["boeNo"].ToString();
            if (Upload != null)
            {
                if (HttpContext.Session.GetString("filelist") == null)
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
                    var iSCService = new ISCService();

                    if (fileextension.ToUpper() == ".PDF")
                    {
                        var attachmentId = CreateAttachment(file, fileextension, _context, iscId.ToString());

                        if (attachmentId != 0)
                        {
                            var _dutyPaymentRequestDetailController = new DutyPaymentRequestDetailController(_context);
                            var dutyPaymentdetails = _context.DutyPaymentRequestDetail.Find(iscId);
                            dutyPaymentdetails.IscPdfAttachmentId = attachmentId;
                            _context.DutyPaymentRequestDetail.Update(dutyPaymentdetails);
                            _context.SaveChanges();
                        }
                    }
                    else
                    {
                        var iSCServiceController = new CHAISCDetailController(_context);

                        var result = iSCService.ReadDatafromExcelWorkSheet4ISC(file, boeNo,Convert.ToInt32(id));

                        if (result != null)
                        {
                            var actionResult = iSCServiceController.CreateBulk(result);
                            var attachmentId = CreateAttachment(file, fileextension, _context, iscId.ToString());

                            var _dutyPaymentRequestDetailController = new DutyPaymentRequestDetailController(_context);
                            var dutyPaymentdetails = _context.DutyPaymentRequestDetail.Find(iscId);
                            dutyPaymentdetails.IscExcelAttachmentId = attachmentId;
                            _context.DutyPaymentRequestDetail.Update(dutyPaymentdetails);
                            _context.SaveChanges();
                        }
                        else
                        { //show error meesage 
                        }

                    }


                }
                else
                {
                    var dd = HttpContext.Session.GetString("filelist").Split(";");

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
                        var iSCService = new ISCService();
                        var iSCServiceController = new CHAISCDetailController(_context);
                        
                        if (fileextension.ToUpper() == ".PDF")
                        {                            
                            var attachmentId = CreateAttachment(file, fileextension, _context, iscId.ToString());

                            var _dutyPaymentRequestDetailController = new DutyPaymentRequestDetailController(_context);
                            var dutyPaymentdetails = _context.DutyPaymentRequestDetail.Find(iscId);
                            dutyPaymentdetails.IscPdfAttachmentId = attachmentId;
                            _context.DutyPaymentRequestDetail.Update(dutyPaymentdetails);
                            _context.SaveChanges();
                        }
                        else
                        {
                            var result = iSCService.ReadDatafromExcelWorkSheet4ISC(file, boeNo,Convert.ToInt32(id));

                            if (result != null)
                            {
                                var actionResult = iSCServiceController.CreateBulk(result);

                                var attachmentId = CreateAttachment(file, fileextension, _context, iscId.ToString());

                                var _dutyPaymentRequestDetailController = new DutyPaymentRequestDetailController(_context);
                                var dutyPaymentdetails = _context.DutyPaymentRequestDetail.Find(iscId);
                                dutyPaymentdetails.IscExcelAttachmentId = attachmentId;
                                _context.DutyPaymentRequestDetail.Update(dutyPaymentdetails);
                                _context.SaveChanges();
                            }
                            else
                            { //show error meesage 
                            }
                        }



                    }
                   
                }
            }
            OnGet(id);
        }

        private int CreateAttachment(string file, string fileextension, DaimlerContext _context, string boeNo)
        {
            var attachmentFileList = new AttachmentFileListController(_context);
            var attachment = new AttachmentFileList();
            attachment.Binary = null;
            attachment.Type = "ISC";
            attachment.FileLocation = _environment.WebRootPath + @"assets\uploads";
            var fileName = file.Split(@"\");
            attachment.FileName = fileName[(fileName.Length - 1)];
            attachment.Extension = fileextension;
            attachment.SourceId = boeNo;
            _context.AttachmentFileLists.Add(attachment);
            _context.SaveChanges();
            return attachment.Id;
        }

        public BOERecordsList4ISC boerecordslist { get; set; }
        public List<BOERecordsDetails4ISC> boeRecordsDetails4ISC { get; set; }

        public void OnGet(string id)
        {
            DaimlerContext _context = new DaimlerContext();
             
            var chaiscdetail = new CHAISCDetailController(_context);
            boerecordslist = chaiscdetail.GetISCRecords(getUplioadId(id));
            boeRecordsDetails4ISC = boerecordslist.boeRecordsDetails4ISC;
        }

        private int getUplioadId(string id)
        {
            try
            {
                var isdId = Convert.ToInt32(id);
                HttpContext.Session.SetString("IscID", id);
                return isdId;
            }
            catch (Exception ex)
            {
                return  CommonFunction.NullToIntZero(HttpContext.Session.GetString("IscID"));

            }
        
        }

        public void OnGetDetele(string id, string iscid, string type)
        {
            DaimlerContext _context = new DaimlerContext();
            var attachment = new AttachmentFileListController(_context);
            var isccontroller = new DutyPaymentRequestDetailController(_context);
            var cHAISCDetailController = new CHAISCDetailController(_context);
            var dutyPaymentRequestDetail = _context.DutyPaymentRequestDetail.FirstOrDefault(x => x.Id == Convert.ToInt32(iscid));

            if (type == ".xlsx")
            {
                dutyPaymentRequestDetail.IscExcelAttachmentId = null;
                var BOENumber = _context.DutyPaymentRequestDetail.Where(x => x.Id == Convert.ToInt32(iscid)).FirstOrDefault().Boeno;
                var chaiscdetail = _context.Chaiscdetails.Where(x => x.Boeid == dutyPaymentRequestDetail.HeaderId && x.Beno== BOENumber);

                foreach (var item in chaiscdetail)
                {
                     cHAISCDetailController.DeleteConfirmed(item.Id);
                }
                
            }

            if (type == ".pdf")
            {
                dutyPaymentRequestDetail.IscPdfAttachmentId = null;
            }

             isccontroller.Edit(dutyPaymentRequestDetail.Id, dutyPaymentRequestDetail);

             _context = new DaimlerContext();

            var attachmentFileList =  _context.AttachmentFileLists.FirstOrDefault(x => x.SourceId == iscid && x.Extension == type && x.Type == "ISC");

             attachment.DeleteConfirmed(attachmentFileList.Id);
            OnGet(id);
        }
    }
}