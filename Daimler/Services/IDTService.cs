using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.IO;
using ExcelDataReader;
using Daimler.Models;
using Daimler.Controllers;

namespace Daimler.Services
{
    public class IDTService
    {
        
        public List<Chadtdetail> ReadDatafromExcelWorkSheet4IDT(string fileName,string BOENo, int headerId)
        {
            try
            {


                //Variable Declaration
                var lstIDTDetails = new List<Chadtdetail>();

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
                var count = 0;
                if (dsexcelRecords != null && dsexcelRecords.Tables.Count > 0)
                {
                    foreach (DataTable excelTable in dsexcelRecords.Tables)
                    {
                        foreach (DataRow dataRow in excelTable.Rows)
                        {

                            if (count == excelTable.Rows.Count)
                            {
                                break;
                            }
                            count += 1;

                            if (count == 1)
                            {
                                continue;
                            }
                                                                                   
                            var IDTDetails = new Chadtdetail();
                            IDTDetails.Boeid = headerId;
                            IDTDetails.JobNo = CommonFunction.NullToEmpty(dataRow["Column0"]);
                            IDTDetails.Beno = CommonFunction.NullToEmpty(dataRow["Column1"]);

                            //if (BOENo != CommonFunction.NullToEmpty(dataRow["Column66"]))
                            //{
                            //    return null;
                            //}


                            if (CommonFunction.NullToEmpty(dataRow["Column2"]) != "")
                            {
                                IDTDetails.Bedate = DateTime.ParseExact(CommonFunction.NullToEmpty(dataRow["Column2"]).Replace(".", @"/"), @"dd\/MM\/yyyy", null);// Convert.ToDateTime();
                            }

                            IDTDetails.InvoiceNo = CommonFunction.NullToEmpty(dataRow["Column3"]);
                            IDTDetails.InvoiceDate = CommonFunction.NullToEmpty(dataRow["Column4"]);
                            IDTDetails.TotalnvoiceValue = CommonFunction.NullToDecimalZero(dataRow["Column5"]);
                            IDTDetails.TotalInoviceCurrency = CommonFunction.NullToEmpty(dataRow["Column6"]);
                            IDTDetails.TotalFreightValue = CommonFunction.NullToDecimalZero(dataRow["Column7"]);
                            IDTDetails.TotalFreightCurrency = CommonFunction.NullToEmpty(dataRow["Column8"]);
                            IDTDetails.TotalInsValue = CommonFunction.NullToDecimalZero(dataRow["Column9"]);

                            IDTDetails.TotalInsCurrency = CommonFunction.NullToEmpty(dataRow["Column10"]);
                            IDTDetails.MiscCharge = CommonFunction.NullToDecimalZero(dataRow["Column11"]);
                            IDTDetails.InvoiceMiscCurrency = CommonFunction.NullToEmpty(dataRow["Column12"]);
                            IDTDetails.ExchangeRate = CommonFunction.NullToDecimalZero(dataRow["Column13"]);
                            IDTDetails.CustomTariffHeading = CommonFunction.NullToEmpty(dataRow["Column14"]);
                            IDTDetails.CentralExciseTariffHeading = CommonFunction.NullToEmpty(dataRow["Column15"]);
                            IDTDetails.ProductDescription = CommonFunction.NullToEmpty(dataRow["Column16"]);
                            IDTDetails.Quantity = CommonFunction.NullToDecimalZero(dataRow["Column17"]);
                            IDTDetails.UnitofProductQuantity = CommonFunction.NullToEmpty(dataRow["Column18"]);
                            IDTDetails.UnitPrice = CommonFunction.NullToDecimalZero(dataRow["Column19"]);

                            IDTDetails.ProductAmount = CommonFunction.NullToDecimalZero(dataRow["Column20"]);
                            IDTDetails.Freight = CommonFunction.NullToDecimalZero(dataRow["Column21"]);
                            IDTDetails.Insurance = CommonFunction.NullToDecimalZero(dataRow["Column22"]);
                            IDTDetails.Cifvalue = CommonFunction.NullToDecimalZero(dataRow["Column23"]);
                            IDTDetails.Loading = CommonFunction.NullToDecimalZero(dataRow["Column24"]);
                            IDTDetails.BasicDutyRate = CommonFunction.NullToDecimalZero(dataRow["Column25"]);
                            IDTDetails.BasicDuty = CommonFunction.NullToDecimalZero(dataRow["Column26"]);
                            IDTDetails.AddlDutyExciseDutyRate = CommonFunction.NullToDecimalZero(dataRow["Column27"]);
                            IDTDetails.AddlDutyExciseDutyAmount = CommonFunction.NullToDecimalZero(dataRow["Column28"]);
                            IDTDetails.Sadrate = CommonFunction.NullToDecimalZero(dataRow["Column29"]);

                            IDTDetails.Sadamount = CommonFunction.NullToDecimalZero(dataRow["Column30"]);
                            IDTDetails.AddlDutySubSec5Rate = CommonFunction.NullToDecimalZero(dataRow["Column31"]);
                            IDTDetails.AddlDutySubSec5Amount = CommonFunction.NullToDecimalZero(dataRow["Column32"]);
                            IDTDetails.EducationCessRateExcise = CommonFunction.NullToDecimalZero(dataRow["Column33"]);
                            IDTDetails.EducationCessAmountExcise = CommonFunction.NullToDecimalZero(dataRow["Column34"]);
                            IDTDetails.SecondaryhigherEducationCessRateExcise = CommonFunction.NullToDecimalZero(dataRow["Column35"]);
                            IDTDetails.SecondaryhigherEducationCessAmountExcise = CommonFunction.NullToDecimalZero(dataRow["Column36"]);
                            IDTDetails.SocialWelfareSurchargeRateCustoms = CommonFunction.NullToDecimalZero(dataRow["Column37"]);
                            IDTDetails.SocialWelfareSurchargeAmountCustoms = CommonFunction.NullToDecimalZero(dataRow["Column38"]);
                            IDTDetails.SecondaryhigherEducationCessRateCustoms = CommonFunction.NullToDecimalZero(dataRow["Column39"]);
                            IDTDetails.SecondaryhigherEducationCessAmountCustoms = CommonFunction.NullToDecimalZero(dataRow["Column40"]);
                            
                            
                            
                            IDTDetails.AssessableValue = CommonFunction.NullToDecimalZero(dataRow["Column41"]);
                            IDTDetails.TotalAssessable = CommonFunction.NullToDecimalZero(dataRow["Column42"]);
                            IDTDetails.TotalBasicDuty = CommonFunction.NullToDecimalZero(dataRow["Column43"]);
                            IDTDetails.TotalSurcharge = CommonFunction.NullToDecimalZero(dataRow["Column44"]);
                            IDTDetails.TotalCvd = CommonFunction.NullToDecimalZero(dataRow["Column45"]);
                            IDTDetails.TotalSad  = CommonFunction.NullToDecimalZero(dataRow["Column46"]);
                            IDTDetails.TotalEducationCess = CommonFunction.NullToDecimalZero(dataRow["Column47"]);
                            IDTDetails.TotalEducationCessExcise = CommonFunction.NullToDecimalZero(dataRow["Column48"]);


                            IDTDetails.TotalSocialWelfareSurchargeCustoms = CommonFunction.NullToDecimalZero(dataRow["Column49"]);
                            IDTDetails.TotalSechigherEducationCess = CommonFunction.NullToDecimalZero(dataRow["Column50"]);
                            IDTDetails.TotalSechigherEducationCessExcise = CommonFunction.NullToDecimalZero(dataRow["Column51"]);
                            IDTDetails.TotalSechigherEducationCessCustoms = CommonFunction.NullToDecimalZero(dataRow["Column52"]);
                            IDTDetails.TotalAdditonalDutySubSec5 = CommonFunction.NullToDecimalZero(dataRow["Column53"]);
                            IDTDetails.TotalDuty = CommonFunction.NullToDecimalZero(dataRow["Column54"]);
                            IDTDetails.SplExciseDutySchedIiRate = CommonFunction.NullToDecimalZero(dataRow["Column55"]);
                            IDTDetails.SplExciseDutySchedIiAmount = CommonFunction.NullToDecimalZero(dataRow["Column56"]);
                            IDTDetails.Model = CommonFunction.NullToEmpty(dataRow["Column57"]);
                            IDTDetails.CessdutyRate = CommonFunction.NullToDecimalZero(dataRow["Column58"]);
                            IDTDetails.Cessduty = CommonFunction.NullToDecimalZero(dataRow["Column59"]);
                            IDTDetails.ModeofTransport = CommonFunction.NullToEmpty(dataRow["Column60"]);
                            IDTDetails.Consignor = CommonFunction.NullToEmpty(dataRow["Column61"]);
                            IDTDetails.TaxCode = CommonFunction.NullToEmpty(dataRow["Column62"]);
                            IDTDetails.Igstrate = CommonFunction.NullToDecimalZero(dataRow["Column63"]);
                            IDTDetails.Igstamount= CommonFunction.NullToDecimalZero(dataRow["Column64"]);
                            IDTDetails.AssessableValue = CommonFunction.NullToDecimalZero(dataRow["Column65"]);
                            IDTDetails.BENo_IDT = CommonFunction.NullToEmpty(dataRow["Column66"]);
                            lstIDTDetails.Add(IDTDetails);
                        }
                        break;
                    }

                }

                return lstIDTDetails;
            }

            catch (Exception)
            {

                throw;
            }
        }

        public partial class CHAIDTDetailDto
        {
            public int Id { get; set; }
            public int Boeid { get; set; }
            public string JobNo { get; set; }
            public string Beno { get; set; }
            public DateTime? Bedate { get; set; }
            public string InvoiceNo { get; set; }
            public string InvoiceDate { get; set; }
            public decimal? TotalnvoiceValue { get; set; }
            public string TotalInoviceCurrency { get; set; }
            public decimal? TotalFreightValue { get; set; }
            public string TotalFreightCurrency { get; set; }
            public decimal? TotalInsValue { get; set; }
            public string TotalInsCurrency { get; set; }
            public decimal? MIDTCharge { get; set; }
            public string InvoiceMIDTCurrency { get; set; }
            public decimal? ExchangeRate { get; set; }
            public string CustomTariffHeading { get; set; }
            public string CentralExciseTariffHeading { get; set; }
            public string ProductDescription { get; set; }
            public decimal? Quantity { get; set; }
            public string UnitofProductQuantity { get; set; }
            public decimal? UnitPrice { get; set; }
            public decimal? ProductAmount { get; set; }
            public decimal? Freight { get; set; }
            public decimal? Insurance { get; set; }
            public decimal? Cifvalue { get; set; }
            public decimal? Loading { get; set; }
            public decimal? BasicDutyRate { get; set; }
            public decimal? BasicDuty { get; set; }
            public decimal? AddlDutyExciseDutyRate { get; set; }
            public decimal? AddlDutyExciseDutyAmount { get; set; }
            public decimal? AddlDutySubSec5Rate { get; set; }
            public decimal? AddlDutySubSec5Amount { get; set; }
            public decimal? EducationCessRateExcise { get; set; }
            public decimal? EducationCessAmountExcise { get; set; }
            public decimal? SecondaryhigherEducationCessRateExcise { get; set; }
            public decimal? SecondaryhigherEducationCessAmountExcise { get; set; }
            public decimal? SocialWelfareSurchargeRateCustoms { get; set; }
            public decimal? SocialWelfareSurchargeAmountCustoms { get; set; }
            public decimal? SecondaryhigherEducationCessRateCustoms { get; set; }
            public decimal? SecondaryhigherEducationCessAmountCustoms { get; set; }
            public decimal? AssessableValue { get; set; }
            public decimal? TotalAssessable { get; set; }
            public decimal? TotalBasicDuty { get; set; }
            public decimal? TotalSurcharge { get; set; }
            public decimal? TotalCvd { get; set; }
            public decimal? TotalEducationCess { get; set; }
            public decimal? TotalEducationCessExcise { get; set; }
            public decimal? TotalSocialWelfareSurchargeCustoms { get; set; }
            public decimal? TotalSechigherEducationCess { get; set; }
            public decimal? TotalSechigherEducationCessExcise { get; set; }
            public decimal? TotalSechigherEducationCessCustoms { get; set; }
            public decimal? TotalAdditonalDutySubSec5 { get; set; }
            public decimal? TotalDuty { get; set; }
            public decimal? SplExciseDutySchedIiRate { get; set; }
            public decimal? SplExciseDutySchedIiAmount { get; set; }
            public string Model { get; set; }
            public decimal? CessdutyRate { get; set; }
            public decimal? Cessduty { get; set; }
            public string ModeofTransport { get; set; }
            public string Consignor { get; set; }
            public decimal? Igstrate { get; set; }
            public decimal? Igstamount { get; set; }
            public decimal? Sadrate { get; set; }
            public decimal? Sadamount { get; set; }
            public decimal? TotalSad { get; set; }
            public string TaxCode { get; set; }
        }
    }

   
}
