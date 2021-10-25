using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Daimler.Models;
using Microsoft.AspNetCore.Http;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.HSSF.UserModel;
using System.IO;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Cors;
using Newtonsoft.Json;
using Daimler.Services;
using System.Data;

namespace Daimler.Controllers
{
    public class DutyPaymentRequestHeaderController : Controller
    {
        private readonly DaimlerContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public DutyPaymentRequestHeaderController(DaimlerContext context)
        {
            _context = context;
        }

        public IActionResult ShowGrid()
        {
            return View();
        }

        public IActionResult LoadData()
        {
            try
            {
                var draw = HttpContext.Request.Form["draw"].FirstOrDefault();
                // Skiping number of Rows count
                var start = Request.Form["start"].FirstOrDefault();
                // Paging Length 10,20
                var length = Request.Form["length"].FirstOrDefault();
                // Sort Column Name
                var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
                // Sort Column Direction ( asc ,desc)
                var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
                // Search Value from (Search box)
                var searchValue = Request.Form["search[value]"].FirstOrDefault();

                //Paging Size (10,20,50,100)
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int recordsTotal = 0;

                // Getting all Customer data
                var customerData = (from tempcustomer in _context.DutyPaymentRequestHeaders
                                    select tempcustomer);

                ////Sorting
                //if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                //{
                //    customerData = customerData.OrderBy(sortColumn + " " + sortColumnDirection);
                //}
                ////Search
                //if (!string.IsNullOrEmpty(searchValue))
                //{
                //    customerData = customerData.Where(m => m.Name == searchValue || m.Phoneno == searchValue || m.City == searchValue);
                //}

                //total number of rows count 
                recordsTotal = customerData.Count();
                //Paging 
                var data = customerData.Skip(skip).Take(pageSize).ToList();
                //Returning Json Data
                return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });

            }
            catch (Exception)
            {
                throw;
            }

        }

        [HttpGet]
        public IActionResult Edit(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    return RedirectToAction("ShowGrid", "DemoGrid");
                }

                return View("Edit");
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public IActionResult Delete(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    return RedirectToAction("ShowGrid", "DemoGrid");
                }

                int result = 0;

                if (result > 0)
                {
                    return Json(data: true);
                }
                else
                {
                    return Json(data: false);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public class paymentlist
        {
            public int Id { get; set; }
            public string Dprno { get; set; }
            public string FileName { get; set; }
            public string Download { get; set; }
            public DateTime? UploadedDate { get; set; }
            public string UploadedBy { get; set; }
            public string DocumentReference { get; set; }
            public string Status { get; set; }
            public int BOERecord { get; set; }
        }
        [HttpGet]
        public IList<paymentlist> GetDutyPaymentRequestHeader()
        {
            var dutyPaymentRequestHeaders = (from DutyPaymentRequestHeader in _context.DutyPaymentRequestHeaders
                                             join Login in _context.Logins on DutyPaymentRequestHeader.UploadedBy equals Login.Id
                                             //join DutyPaymentRequestDetail in _context.DutyPaymentRequestDetail 
                                             //on DutyPaymentRequestHeader.Dprno equals DutyPaymentRequestDetail.RefNo
                                             let totalCount =
                      (from DutyPaymentRequestDetail in _context.DutyPaymentRequestDetail
                       where DutyPaymentRequestDetail.HeaderId == DutyPaymentRequestHeader.Id
                       select DutyPaymentRequestDetail
                      ).Count() 
                                             select new paymentlist
                                             {
                                                 Id = DutyPaymentRequestHeader.Id,
                                                 Dprno = DutyPaymentRequestHeader.Dprno,
                                                 FileName = DutyPaymentRequestHeader.FileName,
                                                 UploadedDate = DutyPaymentRequestHeader.UploadedDate,
                                                 BOERecord = totalCount,
                                                 UploadedBy = Login.Username,
                                                 DocumentReference = DutyPaymentRequestHeader.DocumentReference,
                                                 Status = DutyPaymentRequestHeader.Status,
                                                 Download = "assets/uploads/" + DutyPaymentRequestHeader.FileName
                                             }).ToList();


            return dutyPaymentRequestHeaders;
            //return await _context.DutyPaymentRequestHeader.ToListAsync();
        }

        public class ISClist
        {
            public int Id { get; set; }
            public string Dprno { get; set; }
            public DateTime? DRPDate { get; set; }
            public decimal? DutyDPR { get; set; }
            public decimal? DutyISC { get; set; }
            public int BOERecords { get; set; }
            public int ISCRecords { get; set; }
            public string Status { get; set; }
            public decimal? BOEDuty { get; set; }

        }

        public class ISCDashboard
        {
            public int Dutypaymentrequesttotal { get; set; }
            public int boeRecords { get; set; }
            public int iscRecords { get; set; }
            public int iscPending { get; set; }
            public List<ISClist> ISClist { get; set; }
        }

        public class IDtDashboard
        {
            public int boeRecords { get; set; }
            public int iscRecords { get; set; }
            public int idtRecords { get; set; }
            public int idtPending { get; set; }
            public List<IDTlist> Idtlist { get; set; }
        }

        [HttpGet]
        public ISCDashboard GetISCRecords()
        {
            CommonFunction commonfunction = new CommonFunction();
            DataTable dsExceLData = new DataTable("dsExceLData");
            ISCDashboard ISCDashboarddto = new ISCDashboard();
            List<ISClist> ISClist = new List<ISClist>();
            

            var SQL = @" SELECT * FROM 

                     (SELECT DutyPaymentRequestHeader.ID,DutyPaymentRequestHeader.DPRNo,DutyPaymentRequestHeader.UploadedDate,

                     (SELECT SUM(ISNULL(DutyPR.BOEDuty,0)) FROM DutyPaymentRequestDetail as DutyPR
	                 WHERE DutyPR.HeaderID=DutyPaymentRequestHeader.ID) AS BOEDuty,
                                	
                     ISNULL((SELECT (SUM(ISNULL(CHAISDetails.BasicDuty,0))+SUM(ISNULL(CHAISDetails.SocialWelfareSurchargeAmount_Customs,0))
	                + SUM(ISNULL(CHAISDetails.IGSTAmount,0))) From CHAISCDetail as CHAISDetails 
	                WHERE CHAISDetails.BOEID=DutyPaymentRequestHeader.ID),0) AS ISCDuty,
                 
                    (SELECT Count(DPdetail.ID) FROM DutyPaymentRequestDetail AS DPdetail WHERE DPdetail.HeaderID=DutyPaymentRequestHeader.ID) AS BOErocrds,

                    (SELECT Count(ISCdetail.ID) FROM DutyPaymentRequestDetail as ISCdetail WHERE ISNULL(ISC_Excel_AttachmentID,0)!=0 AND 
                    ISCdetail.HeaderID=DutyPaymentRequestHeader.ID) AS ISCrocrds,

                    (SELECT Count(Dutydetail.ID) FROM DutyPaymentRequestDetail as Dutydetail WHERE Dutydetail.HeaderID=DutyPaymentRequestHeader.ID 
                 	AND ISNULL(Dutydetail.ISC_Excel_AttachmentID,0)=0) AS ISCPending
                    FROM DutyPaymentRequestDetail 
                    
                 	JOIN DutyPaymentRequestHeader ON DutyPaymentRequestHeader.ID=DutyPaymentRequestDetail.HeaderID
                    LEFT JOIN CHAISCDetail ON CHAISCDetail.BOEID=DutyPaymentRequestDetail.HeaderID 
                    GROUP BY DutyPaymentRequestHeader.ID,DutyPaymentRequestHeader.DPRNo,DutyPaymentRequestHeader.UploadedDate) A
                                
                 	WHERE ABS(BOEDuty - ISCDuty) > 1";

            var ISCDashboardrecords = commonfunction.ExecuteSelectSQL(SQL);
            if (ISCDashboardrecords == null) return new ISCDashboard();
            int ISCPending = 0;
            for (int i = 0; i < ISCDashboardrecords.Rows.Count; i++)
            {
                ISClist ISClistdto = new ISClist();
                ISClistdto.Id = CommonFunction.NullToIntZero(ISCDashboardrecords.Rows[i]["ID"]);
                ISClistdto.Dprno = CommonFunction.NullToEmpty(ISCDashboardrecords.Rows[i]["DPRNo"]);
                ISClistdto.DRPDate = (DateTime)ISCDashboardrecords.Rows[i]["UploadedDate"];
                ISClistdto.DutyDPR = CommonFunction.NullToDecimalZero(ISCDashboardrecords.Rows[i]["BOEDuty"]);
                ISClistdto.DutyISC = CommonFunction.NullToDecimalZero(ISCDashboardrecords.Rows[i]["ISCDuty"]);
                ISClistdto.BOERecords = CommonFunction.NullToIntZero(ISCDashboardrecords.Rows[i]["BOErocrds"]);
                ISClistdto.ISCRecords = CommonFunction.NullToIntZero(ISCDashboardrecords.Rows[i]["ISCrocrds"]);
                ISCPending += CommonFunction.NullToIntZero(ISCDashboardrecords.Rows[i]["ISCPending"]);
                ISClistdto.Status = "Pending";

                ISClist.Add(ISClistdto);
            }
            ISCDashboarddto.Dutypaymentrequesttotal = ISClist.Select(x =>x.Id).Distinct().Count();
            ISCDashboarddto.boeRecords = ISClist.Sum(x => x.BOERecords);
            ISCDashboarddto.iscRecords = ISClist.Sum(x => x.ISCRecords);
            ISCDashboarddto.iscPending = ISCPending;
            ISCDashboarddto.ISClist = ISClist;
            return ISCDashboarddto;
            

        }

        public class IDTlist
        {
            public int Id { get; set; }
            public string Dprno { get; set; }
            public DateTime? DRPDate { get; set; }
            public decimal? DutyDPR { get; set; }
            public decimal? DutyISC { get; set; }
            public decimal? DutyIDT { get; set; }
            public int BOERecords { get; set; }
            public int ISCRecords { get; set; }
            public int IDTRecords { get; set; }
            public string Status { get; set; }
        }


        [HttpGet]
        public IDtDashboard GetIDTRecords()
        {
            CommonFunction commonfunction = new CommonFunction();
            DataTable dsExceLData = new DataTable("dsExceLData");
            IDtDashboard idtDashboarddto = new IDtDashboard();
            List<IDTlist> Idtlist = new List<IDTlist>();
            IDTlist Idtlistdto = new IDTlist();

            //var SQL = @"select * from (Select DutyPaymentRequestHeader.ID,DutyPaymentRequestHeader.DPRNo,DutyPaymentRequestHeader.UploadedDate,
            //            (Select SUM(ISNULL(DutyPaymentRequestDetail.BOEDuty,0)) from DutyPaymentRequestDetail where DutyPaymentRequestDetail.HeaderID=DutyPaymentRequestHeader.ID) AS BOEDuty, 
            //            (Select SUM(ISNULL(CHAISCDetail.BasicDuty,0))+ SUM(ISNULL(CHAISCDetail.SocialWelfareSurchargeAmount_Customs,0)) + SUM(ISNULL(CHAISCDetail.IGSTAmount,0)) from CHAISCDetail 
            //            where CHAISCDetail.BOEID=DutyPaymentRequestHeader.ID) AS ISCDuty, 
            //            (Select SUM(ISNULL(CHADTDetail.BasicDuty,0))+ SUM(ISNULL(CHADTDetail.SocialWelfareSurchargeAmount_Customs,0)) + SUM(ISNULL(CHADTDetail.IGSTAmount,0)) from CHADTDetail where CHADTDetail.BOEID=DutyPaymentRequestHeader.ID) AS IDTDuty, 
            //            (Select Count(DPdetail.ID) from DutyPaymentRequestDetail as DPdetail where DPdetail.HeaderID=DutyPaymentRequestHeader.ID) AS BOErocrds, 
            //            (Select Count(ISCdetail.ID) from CHAISCDetail as ISCdetail where ISCdetail.BOEID=DutyPaymentRequestHeader.ID) AS ISCrocrds, 
            //            (Select Count(IDTdetail.ID) from CHADTDetail as IDTdetail where IDTdetail.BOEID=DutyPaymentRequestHeader.ID) AS IDTrocrds  
            //            from DutyPaymentRequestDetail  
            //            Join DutyPaymentRequestHeader on DutyPaymentRequestHeader.ID=DutyPaymentRequestDetail.HeaderID 
            //            join CHAISCDetail On CHAISCDetail.BOEID=DutyPaymentRequestDetail.HeaderID  
            //            left join CHADTDetail On CHADTDetail.BOEID=DutyPaymentRequestDetail.HeaderID  
            //            group by DutyPaymentRequestHeader.ID,DutyPaymentRequestHeader.DPRNo,DutyPaymentRequestHeader.UploadedDate 
            //            having (sum(CHAISCDetail.BasicDuty)+sum(CHAISCDetail.SocialWelfareSurchargeAmount_Customs)+ sum(CHAISCDetail.IGSTAmount))-sum(DutyPaymentRequestDetail.BOEDuty)>=0 ) A
            //            where BOEDuty-IDTDuty!=0";


            var SQL = @"SELECT * FROM (SELECT DutyPaymentRequestHeader.ID,DutyPaymentRequestHeader.DPRNo,DutyPaymentRequestHeader.UploadedDate,
                        (SELECT SUM(ISNULL(DutyPaymentRequestDetail.BOEDuty,0)) FROM DutyPaymentRequestDetail where DutyPaymentRequestDetail.HeaderID=DutyPaymentRequestHeader.ID) AS BOEDuty, 
                      
                         ISNULL((SELECT SUM(ISNULL(CHAISCDetail.BasicDuty,0))+ SUM(ISNULL(CHAISCDetail.SocialWelfareSurchargeAmount_Customs,0)) + SUM(ISNULL(CHAISCDetail.IGSTAmount,0)) FROM CHAISCDetail 
                          where CHAISCDetail.BOEID=DutyPaymentRequestHeader.ID),0) AS ISCDuty, 

                          ISNULL((SELECT SUM(ISNULL(CHADTDetail.BasicDuty,0))+ SUM(ISNULL(CHADTDetail.SocialWelfareSurchargeAmount_Customs,0)) + SUM(ISNULL(CHADTDetail.IGSTAmount,0))
                          FROM CHADTDetail where CHADTDetail.BOEID=DutyPaymentRequestHeader.ID),0) AS IDTDuty, 


                          (SELECT Count(DPdetail.ID) FROM DutyPaymentRequestDetail as DPdetail where DPdetail.HeaderID=DutyPaymentRequestHeader.ID) AS BOErocrds, 

                          (SELECT Count(ISCdetail.ID) FROM DutyPaymentRequestDetail as ISCdetail 
                          WHERE ISNULL(ISC_Excel_AttachmentID,0)!=0 AND ISCdetail.HeaderID=DutyPaymentRequestHeader.ID) AS ISCrocrds, 

                          (SELECT Count(IDTdetail.ID) FROM DutyPaymentRequestDetail as IDTdetail 
						  WHERE ISNULL(IDT_Excel_AttachmentID,0)!=0 AND IDTdetail.HeaderID=DutyPaymentRequestHeader.ID) AS IDTrocrds  

                          FROM DutyPaymentRequestDetail  
                          JOIN DutyPaymentRequestHeader on DutyPaymentRequestHeader.ID=DutyPaymentRequestDetail.HeaderID 
                          JOIN CHAISCDetail On CHAISCDetail.BOEID=DutyPaymentRequestDetail.HeaderID  
                          LEFT JOIN CHADTDetail On CHADTDetail.BOEID=DutyPaymentRequestDetail.HeaderID  

                          GROUP BY DutyPaymentRequestHeader.ID,DutyPaymentRequestHeader.DPRNo,DutyPaymentRequestHeader.UploadedDate
                        ) A

                          WHERE ABS(ISNULL(BOEDuty,0)-ISNULL(IDTDuty,0)) > 1 AND ABS(ISNULL(BOEDuty,0) - ISNULL(ISCDuty,0)) < 1 ";

            var IDtDashboardrecords = commonfunction.ExecuteSelectSQL(SQL);
            if (IDtDashboardrecords == null) return new IDtDashboard();
            for (int i = 0; i < IDtDashboardrecords.Rows.Count; i++)
            {
                Idtlistdto.Id = (int)IDtDashboardrecords.Rows[i]["ID"];
                Idtlistdto.Dprno = (string)IDtDashboardrecords.Rows[i]["DPRNo"];
                Idtlistdto.DRPDate = (DateTime)IDtDashboardrecords.Rows[i]["UploadedDate"];
                Idtlistdto.DutyDPR = (decimal)IDtDashboardrecords.Rows[i]["BOEDuty"];
                Idtlistdto.DutyISC = (decimal)IDtDashboardrecords.Rows[i]["ISCDuty"];
                Idtlistdto.DutyIDT = (decimal)IDtDashboardrecords.Rows[i]["IDTDuty"];
                Idtlistdto.BOERecords = (int)IDtDashboardrecords.Rows[i]["BOErocrds"];
                Idtlistdto.ISCRecords = (int)IDtDashboardrecords.Rows[i]["ISCrocrds"];
                Idtlistdto.IDTRecords = (int)IDtDashboardrecords.Rows[i]["IDTrocrds"];
                Idtlistdto.Status = "Pending";

                Idtlist.Add(Idtlistdto);
            }
            idtDashboarddto.boeRecords = Idtlist.FirstOrDefault().BOERecords;
            idtDashboarddto.iscRecords = Idtlist.FirstOrDefault().ISCRecords;
            idtDashboarddto.idtRecords = Idtlist.FirstOrDefault().IDTRecords;
            idtDashboarddto.idtPending = idtDashboarddto.iscRecords - idtDashboarddto.idtRecords;
            idtDashboarddto.Idtlist = Idtlist;

            return idtDashboarddto;

        }
        // GET: DutyPaymentRequestHeader/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dutyPaymentRequestHeader = await _context.DutyPaymentRequestHeaders
                .Include(d => d.UploadedBy)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dutyPaymentRequestHeader == null)
            {
                return NotFound();
            }

            return View(dutyPaymentRequestHeader);
        }



        [HttpPost]
        public IActionResult Upload(IFormFile file)
        {
            // Extract file name from whatever was posted by browser
            var fileName = System.IO.Path.GetFileName(file.FileName);

            // If file with same name exists delete it
            if (System.IO.File.Exists(fileName))
            {
                System.IO.File.Delete(fileName);
            }

            // Create new local file and copy contents of uploaded file
            using (var localFile = System.IO.File.OpenWrite(fileName))
            using (var uploadedFile = file.OpenReadStream())
            {
                uploadedFile.CopyTo(localFile);
            }

            ViewBag.Message = "File successfully uploaded";

            return View();
        }

        // POST: DutyPaymentRequestHeader/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DutyPaymentRequestHeader dutyPaymentRequestHeader)
        {

            if (ModelState.IsValid)
            {
                _context.Add(dutyPaymentRequestHeader);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Id"] = new SelectList(_context.Logins, "Id", "Id", dutyPaymentRequestHeader.Id);
            return View(dutyPaymentRequestHeader);
        }

        // GET: DutyPaymentRequestHeader/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dutyPaymentRequestHeader = await _context.DutyPaymentRequestHeaders.FindAsync(id);
            if (dutyPaymentRequestHeader == null)
            {
                return NotFound();
            }
            ViewData["Id"] = new SelectList(_context.Logins, "Id", "Id", dutyPaymentRequestHeader.Id);
            return View(dutyPaymentRequestHeader);
        }

        // POST: DutyPaymentRequestHeader/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Dprno,FileName,UploadedDate,UploadedBy,DocumentReference,Status")] DutyPaymentRequestHeader dutyPaymentRequestHeader)
        {
            if (id != dutyPaymentRequestHeader.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dutyPaymentRequestHeader);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DutyPaymentRequestHeaderExists(dutyPaymentRequestHeader.Id))
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
            ViewData["Id"] = new SelectList(_context.Logins, "Id", "Id", dutyPaymentRequestHeader.Id);
            return View(dutyPaymentRequestHeader);
        }

        // GET: DutyPaymentRequestHeader/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dutyPaymentRequestHeader = await _context.DutyPaymentRequestHeaders
                .Include(d => d.UploadedBy)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dutyPaymentRequestHeader == null)
            {
                return NotFound();
            }

            return View(dutyPaymentRequestHeader);
        }

        // POST: DutyPaymentRequestHeader/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dutyPaymentRequestHeader = await _context.DutyPaymentRequestHeaders.FindAsync(id);
            var dutyPaymentRequestDetails = _context.DutyPaymentRequestDetail.Where(x => x.HeaderId == id).ToList();
            //dutyPaymentRequestHeader.DutyPaymentRequestDetails = dutyPaymentRequestDetails;
            foreach (var dutyPaymentRequestDetail in dutyPaymentRequestDetails)
            {
                _context.DutyPaymentRequestDetail.Remove(dutyPaymentRequestDetail);
            }
            _context.DutyPaymentRequestHeaders.Remove(dutyPaymentRequestHeader);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DutyPaymentRequestHeaderExists(int id)
        {
            return _context.DutyPaymentRequestHeaders.Any(e => e.Id == id);
        }
    }
}
