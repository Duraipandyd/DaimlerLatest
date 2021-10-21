using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.IO;
using ExcelDataReader;
using Daimler.Models;
using Microsoft.AspNetCore.Http;

namespace Daimler.Services
{
    public class DutyPaymentRequestHeaderService
    {
        private readonly DaimlerContext _context;

        public DutyPaymentRequestHeaderService(DaimlerContext context)
        {
            _context = context;
        }
        public DutyPaymentRequestHeader ReadDatafromExcelWorkSheet(string fileName,int loginId)
        {
            try
            {


                //Variable Declaration
                var dutyPaymentRequestHeaderDto = new DutyPaymentRequestHeader();
                var lstdutyPaymentRequestLinesDto = new List<DutyPaymentRequestDetail>();

                DataSet dsexcelRecords = new DataSet();
                IExcelDataReader reader = null;
                Stream FileStream = null;
                FileInfo fileInfo = new FileInfo(fileName);

                //Excel Data reader must be added below encoded line
                System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

                //Read the data and stroed to file stream memory
                FileStream = System.IO.File.Open(fileName, FileMode.Open, FileAccess.Read);

                //Check the file extension read the data 
                if (fileInfo.Extension.ToUpper() == ".XLS")
                {
                    reader = ExcelReaderFactory.CreateBinaryReader(FileStream);
                }

                else if (fileInfo.Extension.ToUpper() == ".XLSX")
                {
                    reader = ExcelReaderFactory.CreateOpenXmlReader(FileStream);
                }

                else
                {
                    //TODO
                    //message = "The file format is not supported.";
                }

                //Converted to dataset
                dsexcelRecords = reader.AsDataSet();
                reader.Close();
                var splitedFileName = fileName.Split(@"\");

                var orinnalFileName = splitedFileName[splitedFileName.Length - 1];

                
                if (dsexcelRecords != null && dsexcelRecords.Tables.Count > 0)
                {
                    foreach (DataTable excelTable in dsexcelRecords.Tables)
                    {
                       
                        dutyPaymentRequestHeaderDto.DocumentReference = "";
                        dutyPaymentRequestHeaderDto.FileName = orinnalFileName;
                        //dutyPaymentRequestHeaderDto.UploadedBy = 100;
                        dutyPaymentRequestHeaderDto.UploadedBy =loginId;
                        dutyPaymentRequestHeaderDto.UploadedDate = DateTime.Now;
                       
                        

                        if (!excelTable.Columns.Contains("Column11"))
                        {
                            //TODO 
                            //Update the status to Rejected
                            dutyPaymentRequestHeaderDto.Status = "Rejected";
                            break;
                        }
                        
                        var rowcount = 0;
                   
                        foreach (DataRow dataRow in excelTable.Rows)
                        {
                            rowcount += 1;
                            if (rowcount == 1)
                            {
                                continue;
                            }

                            if (rowcount == 2)
                            {
                                var DPRNo = NullToEmpty(dataRow["Column11"]);

                                if (DPRNo == "")
                                {
                                    //TODO 
                                    //Update the status to Rejected
                                    dutyPaymentRequestHeaderDto.Status = "Rejected";
                                    break;
                                }
                                else
                                {
                                    DPRNo = DPRNo.Split(":")[1].Trim();
                                    dutyPaymentRequestHeaderDto.Dprno = DPRNo;
                                }
                            }

                            if (rowcount <= 17)
                            {
                                continue;
                            }


                            if (rowcount == 18)
                            {
                                if (NullToEmpty(dataRow["Column1"]) != "S.no")
                                {
                                    //TODO 
                                    //Update the status to Rejected   
                                    dutyPaymentRequestHeaderDto.Status = "Rejected";
                                    break;
                                }

                                if (NullToEmpty(dataRow["Column2"]) != "Bill No")
                                {
                                    //TODO 
                                    //Update the status to Rejected    
                                    dutyPaymentRequestHeaderDto.Status = "Rejected";
                                    break;
                                }

                                if (NullToEmpty(dataRow["Column3"]) != "Duty Value")
                                {
                                    //TODO 
                                    //Update the status to Rejected  
                                    dutyPaymentRequestHeaderDto.Status = "Rejected";
                                    break;
                                }

                                if (NullToEmpty(dataRow["Column4"]) != "Ref No.")
                                {
                                    //TODO 
                                    //Update the status to Rejected  
                                    dutyPaymentRequestHeaderDto.Status = "Rejected";
                                    break;
                                }

                                if (NullToEmpty(dataRow["Column5"]) != "Invoice No.")
                                {
                                    //TODO 
                                    //Update the status to Rejected  
                                    dutyPaymentRequestHeaderDto.Status = "Rejected";
                                    break;
                                }

                                if (NullToEmpty(dataRow["Column6"]) != "BOE No.")
                                {
                                    //TODO 
                                    //Update the status to Rejected    
                                    dutyPaymentRequestHeaderDto.Status = "Rejected";
                                    break;
                                }

                                if (NullToEmpty(dataRow["Column7"]) != "Port Code")
                                {
                                    //TODO 
                                    //Update the status to Rejected  
                                    dutyPaymentRequestHeaderDto.Status = "Rejected";
                                    break;
                                }

                                if (NullToEmpty(dataRow["Column8"]) != "BOE DUTY")
                                {
                                    //TODO 
                                    //Update the status to Rejected  
                                    dutyPaymentRequestHeaderDto.Status = "Rejected";
                                    break;

                                }

                                if (NullToEmpty(dataRow["Column9"]) != "Fine")
                                {
                                    //TODO 
                                    //Update the status to Rejected  
                                    dutyPaymentRequestHeaderDto.Status = "Rejected";
                                    break;

                                }

                                if (NullToEmpty(dataRow["Column10"]) != "Penalty")
                                {
                                    //TODO 
                                    //Update the status to Rejected  
                                    dutyPaymentRequestHeaderDto.Status = "Rejected";
                                    break;

                                }

                                if (NullToEmpty(dataRow["Column11"]) != "Interest")
                                {
                                    //TODO 
                                    //Update the status to Rejected   
                                    dutyPaymentRequestHeaderDto.Status = "Rejected";
                                    break;

                                }

                                //TODO 
                                //Insert the Duty payment request data to the table
                            }

                            if (rowcount > 18 && NullToIntZero(dataRow["Column1"]) == 0)
                            {
                                continue;
                            }

                            if (rowcount >=19)
                            {
                                var dutyPaymentRequestLinesDto = new DutyPaymentRequestDetail();
                                dutyPaymentRequestLinesDto.LineId = NullToIntZero(dataRow["Column1"]);
                                dutyPaymentRequestLinesDto.DutyValue = NullToDecimalZero(dataRow["Column3"]);
                                dutyPaymentRequestLinesDto.RefNo = NullToEmpty(dataRow["Column4"]);
                                dutyPaymentRequestLinesDto.InvoiceNo = NullToEmpty(dataRow["Column5"]);
                                dutyPaymentRequestLinesDto.Boeno = NullToEmpty(dataRow["Column6"]);
                                dutyPaymentRequestLinesDto.PortCode = NullToEmpty(dataRow["Column7"]);
                                dutyPaymentRequestLinesDto.Boeduty = NullToDecimalZero(dataRow["Column8"]);

                                if (NullToEmpty(dataRow["Column9"]) != "-")
                                {
                                    dutyPaymentRequestLinesDto.Fine = NullToDecimalZero(dataRow["Column9"]);
                                }

                                if (NullToEmpty(dataRow["Column10"]) != "-")
                                {
                                    dutyPaymentRequestLinesDto.Penalty = NullToDecimalZero(dataRow["Column10"]);
                                }

                                if (NullToEmpty(dataRow["Column11"]) != "-")
                                {
                                    dutyPaymentRequestLinesDto.Interest = NullToDecimalZero(dataRow["Column11"]);
                                }

                                lstdutyPaymentRequestLinesDto.Add(dutyPaymentRequestLinesDto);
                            }

                            


                        }

                    }
                }

                dutyPaymentRequestHeaderDto.Status = "Accepted";
                dutyPaymentRequestHeaderDto.DutyPaymentRequestDetails = lstdutyPaymentRequestLinesDto;

                //System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

                //using (var stream = System.IO.File.Open(fileName, FileMode.Open, FileAccess.Read))
                //{
                //    using (var reader = ExcelReaderFactory.CreateReader(stream))
                //    {

                //        var dsResponseXMLFile = new DataSet();
                //        //dsResponseXMLFile.CreateDataReader(stream);
                //        while (reader.Read()) //Each row of the file
                //        {



                //        }
                //    }
                //}
                return dutyPaymentRequestHeaderDto;
            }

            catch (Exception)
            {

                return null;
            }
        }


        public static object NullToZero(object value)
        {
            try
            {
                return Convert.ToDecimal(value);
            }
            catch (Exception)
            {
                return 0;
            }

        }

        public static Double NothingToDoubleZero(Object value)
        {
            if (!(value is DBNull))
            {
                return Convert.ToDouble(value);
            }
            else
            {
                return 0.0;
            }
        }

        public static decimal NullToDecimalZero(object value)
        {
            try
            {
                if (!(value is DBNull))
                {
                    return Convert.ToDecimal(value);
                }
                else
                {
                    return 0;
                }

            }
            catch (Exception)
            {
                return 0;
            }
        }

        public static int NullToIntZero(object value)
        {
            try
            {
                if (!(value is DBNull))
                {
                    return Convert.ToInt32(value);
                }
                else
                {
                    return 0;
                }

            }
            catch (Exception)
            {
                return 0;
            }
        }

        public static string NullToEmpty(object value)
        {
            try
            {
                if (value == null)
                {
                    return "";
                }

                return value.ToString();
            }
            catch (Exception)
            {
                return "";
            }
        }

        public static double NullToDoubleZero(object prmobject)
        {
            try
            {
                if (DBNull.Value.Equals(prmobject))
                {
                    return 0.0;
                }
                else
                {
                    return Convert.ToDouble(prmobject);
                }


            }
            catch (Exception)
            {
                return 0.0;
            }

        }

    }

    public class DutyPaymentRequestHeaderDto
    {
        public int ID { get; set; }
        public string DPRNo { get; set; }
        public string FileName { get; set; }
        public DateTime UploadedDate { get; set; }
        public int UploadedBy { get; set; }
        public string DocumentReference { get; set; }
        public string Status { get; set; }
        public List<DutyPaymentRequestLinesDto> DutyPaymentRequestLines { get; set; }
    }

    public class DutyPaymentRequestLinesDto
    {
        public int ID { get; set; }
        public int HeaderID { get; set; }
        public int LineID { get; set; }

        public decimal? DutyValue { get; set; }
        public string RefNo { get; set; }
        public string InvoiceNo { get; set; }
        public string BOENo { get; set; }
        public string PortCode { get; set; }

        public decimal? BOEDuty { get; set; }
        public decimal? Fine { get; set; }
        public decimal? Penalty { get; set; }
        public decimal? Interest { get; set; }

        public int? ISC_Excel_AttachmentID { get; set; }
        public int? ISC_PDF_AttachmentID { get; set; }
        public int? IDT_PDF_AttachmentID { get; set; }
        public int? IDT_Excel_AttachmentID { get; set; }
    }
}
