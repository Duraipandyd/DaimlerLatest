using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Daimler.Models;
using Daimler.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Office.Interop.Excel;

namespace Daimler.Pages
{
    public class ReportsModel : PageModel
    {
        private IHostingEnvironment _environment;

        [Obsolete]
        public ReportsModel(IHostingEnvironment environment)
        {
            _environment = environment;
        }
        public FileResult OnGetDownloadFile(string file, string filepath)
        {

            //Read the File data into Byte Array.
            byte[] bytes = System.IO.File.ReadAllBytes(filepath);

            //Send the File to Download.
            return File(bytes, "application/octet-stream", file);
        }
        public void OnGet()
        {
            
        }

        public string DownlodFile { get; set; }
        public string DownloadFilePath { get; set; }
        public void OnGetReport(string dprnumber, string chaisc, string chaidt, string boenumber, string boedate)

        {
            string FileName = "";
            CommonFunction commonfunction = new CommonFunction();
            DataSet dsExceLData = new DataSet("dsExceLData");



            var SQL = @"Select DutyPaymentRequestDetail.*
                        from DutyPaymentRequestDetail  
                        Join DutyPaymentRequestHeader on DutyPaymentRequestHeader.ID=DutyPaymentRequestDetail.HeaderID 
                        join CHAISCDetail On CHAISCDetail.BOEID=DutyPaymentRequestDetail.HeaderID  
                        left join CHADTDetail On CHADTDetail.BOEID=DutyPaymentRequestDetail.HeaderID  
                        left join AttachmentFileList On AttachmentFileList.ID=DutyPaymentRequestDetail.ISC_Excel_AttachmentID
                        left join AttachmentFileList A On A.ID=DutyPaymentRequestDetail.ISC_PDF_AttachmentID
                        where 1=1";
            if (dprnumber != "" && dprnumber != null)
            {
                SQL += " AND DutyPaymentRequestHeader.DPRNo='" + dprnumber + "'";
            }
            if (chaisc != "" && chaisc != null)
            {
                SQL += " AND DutyPaymentRequestDetail.DutyValue=" + chaisc;
            }
            if (chaidt != "" && chaidt != null)
            {
                SQL += " AND DutyPaymentRequestDetail.BOEDuty=" + chaidt;
            }
            if (boenumber != "" && boenumber != null)
            {
                SQL += " AND DutyPaymentRequestDetail.BOENo='" + boenumber + "'";
            }
            if (boedate != "" && boedate != null)
            {
                SQL += " AND CHAISCDetail.BEDate='" + Convert.ToDateTime(boedate).ToString("yyyy-MM-dd") + "'";
            }


            var DutyPaymentReques = ExecuteSelectSQL(SQL);
            if (DutyPaymentReques != null)
            {
                dsExceLData.Tables.Add(DutyPaymentReques);
                var CHAISCDetail = new System.Data.DataTable();
                var CHAIDTDetail = new System.Data.DataTable();
                if (DutyPaymentReques.Rows.Count > 0)
                {
                    SQL = "Select * from CHAISCDetail WHERE BOEID =" + DutyPaymentReques.Rows[0]["HeaderID"];
                    var ISCDetails = ExecuteSelectSQL(SQL);
                    if (ISCDetails != null)
                    {
                        dsExceLData.Tables.Add(ISCDetails);

                    }

                    SQL = "Select * from CHADTDetail WHERE BOEID =" + DutyPaymentReques.Rows[0]["HeaderID"];
                    var IDtDetails = ExecuteSelectSQL(SQL);
                    if (IDtDetails != null)
                    {
                        dsExceLData.Tables.Add(IDtDetails);
                    }
                }
            }

           

            FileName =  dprnumber + (Regex.Replace(DateTime.Now.ToString(), @"[^0-9a-zA-Z]+", "").Replace(":", "_").Replace(@"\", "_").Replace(" ", "")) + ".xlsx";
            DownlodFile = FileName;
             FileName = Path.Combine(_environment.WebRootPath, @"assets\download\", FileName);
            DownloadFilePath = @"assets\download\" + DownlodFile;

            Application ExcelApp = new Application();

            Workbook ExcelWorkBook = null;

            Worksheet ExcelWorkSheet = null;

            ExcelApp.Visible = false;
            ExcelWorkBook = ExcelApp.Workbooks.Add(XlWBATemplate.xlWBATWorksheet);

            List<string> SheetNames = new List<string>();

            SheetNames.Add("DutyPaymentRequest");
            SheetNames.Add("ISC");
            SheetNames.Add("IDT");

            try

            {

                for (int i = 1; i < dsExceLData.Tables.Count; i++)

                    ExcelWorkBook.Worksheets.Add(); //Adding New sheet in Excel Workbook



                for (int i = 0; i < dsExceLData.Tables.Count; i++)

                {

                    int r = 1; // Initialize Excel Row Start Position  = 1



                    ExcelWorkSheet = (Worksheet)ExcelWorkBook.Worksheets[i + 1];



                    //Writing Columns Name in Excel Sheet



                    for (int col = 1; col < dsExceLData.Tables[i].Columns.Count; col++)

                        ExcelWorkSheet.Cells[r, col] = CommonFunction.NullToEmpty(dsExceLData.Tables[i].Columns[col - 1].ColumnName);

                    r++;



                    //Writing Rows into Excel Sheet

                    for (int row = 0; row < dsExceLData.Tables[i].Rows.Count; row++) //r stands for ExcelRow and col for ExcelColumn

                    {

                        // Excel row and column start positions for writing Row=1 and Col=1

                        for (int col = 1; col < dsExceLData.Tables[i].Columns.Count; col++)

                            ExcelWorkSheet.Cells[r, col] = CommonFunction.NullToEmpty( dsExceLData.Tables[i].Rows[row][col - 1].ToString());

                        r++;

                    }

                    ExcelWorkSheet.Name = SheetNames[i];//Renaming the ExcelSheets



                }



                ExcelWorkBook.SaveAs(FileName);

                ExcelWorkBook.Close();

                ExcelApp.Quit();

                //Marshal.ReleaseComObject(ExcelWorkSheet);

                //Marshal.ReleaseComObject(ExcelWorkBook);

                //Marshal.ReleaseComObject(ExcelApp);

                ////return File(FileName, "application/vnd.openxmlformatsofficedocument.spreadsheetml.sheet", orginalFilename);
                ////Read the File data into Byte Array.

                ////FilePath = FileName;
                //byte[] bytes = System.IO.File.ReadAllBytes(FilePath);

                //////return File(bytes, "application/force-download", orignalFile);

                //////Send the File to Download.
                //return  File(bytes, "application/octet-stream", orignalFile);
               
            }

            catch (Exception exHandle)

            {
                
            }


        }
        public ActionResult OnGetDownload(string path,string filename)
        {
            return File(path, "application/octet-stream", filename);
        }
        private string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
        }

        private Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                {".txt", "text/plain"},
                {".pdf", "application/pdf"},
                {".doc", "application/vnd.ms-word"},
                {".docx", "application/vnd.ms-word"},
                {".xls", "application/vnd.ms-excel"},
                {".xlsx", "application/vnd.openxmlformatsofficedocument.spreadsheetml.sheet"},  
                {".png", "image/png"},
                {".jpg", "image/jpeg"},
                {".jpeg", "image/jpeg"},
                {".gif", "image/gif"},
                {".csv", "text/csv"}
            };
        }
        public System.Data.DataTable ExecuteSelectSQL(String SQLString)
        {

            try
            {

                System.Data.DataTable dtblDataTable = new System.Data.DataTable();

                using (DaimlerContext context = new DaimlerContext())
                {
                    DbConnection conn = context.Database.GetDbConnection();
                    DbCommand cmd = conn.CreateCommand();

                    cmd.CommandText = SQLString;

                    conn.Open();

                    try
                    {
                        SqlDataAdapter dataAdapter = new SqlDataAdapter();
                        dataAdapter.SelectCommand = (SqlCommand)cmd;
                        dataAdapter.Fill(dtblDataTable);
                    }
                    catch (Exception ex)
                    {

                    }

                    conn.Close();

                    if (dtblDataTable.Rows.Count == 0) return null;

                }

                return dtblDataTable;

            }
            catch (Exception ex)
            {

                return null;
            }

        }
    }
}
