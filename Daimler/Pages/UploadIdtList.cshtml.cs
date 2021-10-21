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
using static Daimler.Controllers.CHADTDetailController;
using static Daimler.Controllers.CHAISCDetailController;

namespace Daimler.Pages
{
    public class UploadIdtListModel : PageModel
    {
        private IHostingEnvironment _environment;

        [Obsolete]
        public UploadIdtListModel(IHostingEnvironment environment)
        {
            _environment = environment;
        }

        [BindProperty]
        public IFormFile Upload { get; set; }

        [Obsolete]
        public async Task OnPostAsync(string id)
        {
            int idtId = Convert.ToInt32(Request.Form["idtId"]);
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

                    if (fileextension.ToUpper() == ".PDF")
                    {
                        var attachmentId = CreateAttachment(file, fileextension, _context, idtId.ToString());

                        if (attachmentId != 0)
                        {
                            var _dutyPaymentRequestDetailController = new DutyPaymentRequestDetailController(_context);
                            var dutyPaymentdetails = _context.DutyPaymentRequestDetail.Find(idtId);
                            dutyPaymentdetails.IdtPdfAttachmentId = attachmentId;
                            _context.DutyPaymentRequestDetail.Update(dutyPaymentdetails);
                            _context.SaveChanges();
                        }
                    }
                    else
                    {
                        
                        var iSCService = new IDTService();
                        var idtController = new CHADTDetailController(_context);
                        var result = iSCService.ReadDatafromExcelWorkSheet4IDT(file, boeNo, Convert.ToInt32(id));

                        if (result != null)
                        {
                            var actionResult = idtController.CreateBulk(result);
                            var attachmentId = CreateAttachment(file, fileextension, _context, idtId.ToString());

                            var _dutyPaymentRequestDetailController = new DutyPaymentRequestDetailController(_context);
                            var dutyPaymentdetails = _context.DutyPaymentRequestDetail.Find(idtId);
                            dutyPaymentdetails.IdtExcelAttachmentId = attachmentId.ToString();
                            _context.DutyPaymentRequestDetail.Update(dutyPaymentdetails);
                            _context.SaveChanges();
                            updateAttachmentid(result, attachmentId);
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

                        if (fileextension.ToUpper() == ".PDF")
                        {
                            var attachmentId = CreateAttachment(file, fileextension, _context, idtId.ToString());

                            if (attachmentId != 0)
                            {
                                var _dutyPaymentRequestDetailController = new DutyPaymentRequestDetailController(_context);
                                var dutyPaymentdetails = _context.DutyPaymentRequestDetail.Find(idtId);
                                dutyPaymentdetails.IdtPdfAttachmentId = attachmentId;
                                _context.DutyPaymentRequestDetail.Update(dutyPaymentdetails);
                                _context.SaveChanges();
                            }
                        }
                        else {
                            var iSCService = new IDTService();
                            var idtController = new CHADTDetailController(_context);
                            var result = iSCService.ReadDatafromExcelWorkSheet4IDT(file, boeNo,Convert.ToInt32(id));

                            if (result != null)
                            {
                                var actionResult = idtController.CreateBulk(result);
                                var attachmentId = CreateAttachment(file, fileextension, _context, idtId.ToString());

                                var _dutyPaymentRequestDetailController = new DutyPaymentRequestDetailController(_context);
                                var dutyPaymentdetails = _context.DutyPaymentRequestDetail.Find(idtId);
                                dutyPaymentdetails.IdtExcelAttachmentId = attachmentId.ToString();
                                _context.DutyPaymentRequestDetail.Update(dutyPaymentdetails);
                                _context.SaveChanges();
                                updateAttachmentid(result, attachmentId);
                            }
                        }
                        

                    }
                    
                }
            }
            OnGet(id);
        }

        private bool updateAttachmentid(List<Chadtdetail> Chadtdetails,int attachmentID)
        {
            try
            {
                DaimlerContext _context = new DaimlerContext();
                foreach (var Chadtdetail in Chadtdetails)
                {
                    var ChadtdetailRecord = _context.Chadtdetails.Find(Chadtdetail.Id);
                    ChadtdetailRecord.AttachmentID = attachmentID;
                    _context.Chadtdetails.Update(ChadtdetailRecord);
                    _context.SaveChanges();

                }
                
                return true;

            }
            catch (Exception)
            {

                throw;
            }
        }
        private int CreateAttachment(string file, string fileextension, DaimlerContext _context, string boeNo)
        {
            var attachmentFileList = new AttachmentFileListController(_context);
            var attachment = new AttachmentFileList();
            attachment.Binary = null;
            attachment.Type = "IDT";
            attachment.FileLocation = _environment.WebRootPath + @"assets\uploads";
            var fileName = file.Split(@"\");
            attachment.FileName = fileName[(fileName.Length - 1)];
            attachment.Extension = fileextension;
            attachment.SourceId = boeNo;
            _context.AttachmentFileLists.Add(attachment);
            _context.SaveChanges();
            return attachment.Id;
        }

        public BOERecordsList4IDT boerecordslist { get; set; }
        public List<BOERecordsDetails4IDT> boeRecordsDetails4DT { get; set; }

        public void OnGet(string id)
        {
            DaimlerContext _context = new DaimlerContext();

            var chaiscdetail = new CHADTDetailController(_context);
            boerecordslist = chaiscdetail.GetIDTRecords(getUplioadId(id));
            boeRecordsDetails4DT = boerecordslist.boeRecordsDetails4IDT;
        }

        private int getUplioadId(string id)
        {
            try
            {
                var isdId = Convert.ToInt32(id);
                HttpContext.Session.SetString("IdtID", id);
                return isdId;
            }
            catch (Exception ex)
            {
                return CommonFunction.NullToIntZero(HttpContext.Session.GetString("IdtID"));

            }

        }

        public void OnGetDelete(string id, string iscid, string attachmentID)
        {
            DaimlerContext _context = new DaimlerContext();
            var attachment = new AttachmentFileListController(_context);
            var isccontroller = new DutyPaymentRequestDetailController(_context);
            var cHAIDTDetailController = new CHADTDetailController(_context);

           
            if (attachmentID != null)
            {
                var chaidtdetail = _context.Chadtdetails.Where(x => x.AttachmentID == Convert.ToInt32(attachmentID));
                foreach (var item in chaidtdetail)
                {
                    cHAIDTDetailController.DeleteConfirmed(item.Id);
                }

            }

            var dutyPaymentRequestDetail = _context.DutyPaymentRequestDetail.FirstOrDefault(x => x.Id == Convert.ToInt32(iscid));
            if (attachmentID != null)
            {
                dutyPaymentRequestDetail.IdtExcelAttachmentId = null;
            }
            isccontroller.Edit(dutyPaymentRequestDetail.Id, dutyPaymentRequestDetail);

            _context = new DaimlerContext();

            var attachmentFileList = _context.AttachmentFileLists.FirstOrDefault(x => x.Id == Convert.ToInt32(attachmentID));
            attachment.DeleteConfirmed(attachmentFileList.Id);

            OnGet(id);
        }
    }
}