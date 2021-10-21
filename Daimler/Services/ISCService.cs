using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.IO;
using ExcelDataReader;
using Daimler.Models;

namespace Daimler.Services
{
    public class ISCService
    {
        private readonly DaimlerContext _context;

        public List<Chaiscdetail> ReadDatafromExcelWorkSheet4ISC(string fileName,string BOENo,int headerId)
        {
            var count = 0;
            try
            {


                //Variable Declaration
                var lstISCDetais = new List<Chaiscdetail>();

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

                          var  ISCDetais = new Chaiscdetail();
                            ISCDetais.Boeid = headerId;
                            ISCDetais.JobNo = CommonFunction.NullToEmpty(dataRow["Column0"]);
                            ISCDetais.Beno = CommonFunction.NullToEmpty(dataRow["Column1"]);

                            //if (BOENo != ISCDetais.Beno)
                            //{
                            //    return null;
                            //}

                            if (CommonFunction.NullToEmpty(dataRow["Column2"]) != "")
                            {
                                ISCDetais.Bedate = convertstringtoDatetime(CommonFunction.NullToEmpty(dataRow["Column2"]));
                            }
                            
                            ISCDetais.InvoiceNo = CommonFunction.NullToEmpty(dataRow["Column3"]);
                            ISCDetais.InvoiceDate = CommonFunction.NullToEmpty(dataRow["Column4"]);
                            ISCDetais.TotalnvoiceValue = CommonFunction.NullToDecimalZero(dataRow["Column5"]);
                            ISCDetais.TotalInoviceCurrency = CommonFunction.NullToEmpty(dataRow["Column6"]);
                            ISCDetais.TotalFreightValue = CommonFunction.NullToDecimalZero(dataRow["Column7"]);
                            ISCDetais.TotalFreightCurrency = CommonFunction.NullToEmpty(dataRow["Column8"]);
                            ISCDetais.TotalInsValue = CommonFunction.NullToDecimalZero(dataRow["Column9"]);
                            ISCDetais.TotalInsCurrency = CommonFunction.NullToEmpty(dataRow["Column10"]);
                            ISCDetais.MiscCharge = CommonFunction.NullToDecimalZero(dataRow["Column11"]);
                            ISCDetais.InvoiceMiscCurrency = CommonFunction.NullToEmpty(dataRow["Column12"]);
                            ISCDetais.ExchangeRate = CommonFunction.NullToDecimalZero(dataRow["Column13"]);
                            ISCDetais.CustomTariffHeading = CommonFunction.NullToEmpty(dataRow["Column14"]);
                            ISCDetais.CentralExciseTariffHeading = CommonFunction.NullToEmpty(dataRow["Column15"]);
                            ISCDetais.ProductDescription = CommonFunction.NullToEmpty(dataRow["Column16"]);
                            ISCDetais.Quantity = CommonFunction.NullToDecimalZero(dataRow["Column17"]);
                            ISCDetais.UnitofProductQuantity = CommonFunction.NullToEmpty(dataRow["Column18"]);
                            ISCDetais.UnitPrice = CommonFunction.NullToDecimalZero(dataRow["Column19"]);
                            ISCDetais.ProductAmount = CommonFunction.NullToDecimalZero(dataRow["Column20"]);
                            ISCDetais.Freight = CommonFunction.NullToDecimalZero(dataRow["Column21"]);
                            ISCDetais.Insurance = CommonFunction.NullToDecimalZero(dataRow["Column22"]);
                            ISCDetais.Cifvalue = CommonFunction.NullToDecimalZero(dataRow["Column23"]);
                            ISCDetais.Loading = CommonFunction.NullToDecimalZero(dataRow["Column24"]);
                            ISCDetais.BasicDutyRate = CommonFunction.NullToDecimalZero(dataRow["Column25"]);
                            ISCDetais.BasicDuty = CommonFunction.NullToDecimalZero(dataRow["Column26"]);
                            ISCDetais.AddlDutyExciseDutyRate = CommonFunction.NullToDecimalZero(dataRow["Column27"]);
                            ISCDetais.AddlDutyExciseDutyAmount = CommonFunction.NullToDecimalZero(dataRow["Column28"]);
                            ISCDetais.AddlDutySubSec5Rate = CommonFunction.NullToDecimalZero(dataRow["Column29"]);
                            ISCDetais.AddlDutySubSec5Amount = CommonFunction.NullToDecimalZero(dataRow["Column30"]);
                            ISCDetais.EducationCessRateExcise = CommonFunction.NullToDecimalZero(dataRow["Column31"]);
                            ISCDetais.EducationCessAmountExcise = CommonFunction.NullToDecimalZero(dataRow["Column32"]);                                 
                            ISCDetais.SecondaryhigherEducationCessRateExcise = CommonFunction.NullToDecimalZero(dataRow["Column33"]);
                            ISCDetais.SecondaryhigherEducationCessAmountExcise = CommonFunction.NullToDecimalZero(dataRow["Column34"]);                            
                            ISCDetais.SocialWelfareSurchargeRateCustoms = CommonFunction.NullToDecimalZero(dataRow["Column35"]);
                            ISCDetais.SocialWelfareSurchargeAmountCustoms = CommonFunction.NullToDecimalZero(dataRow["Column36"]);
                            ISCDetais.SecondaryhigherEducationCessRateCustoms = CommonFunction.NullToDecimalZero(dataRow["Column37"]);
                            ISCDetais.SecondaryhigherEducationCessAmountExcise = CommonFunction.NullToDecimalZero(dataRow["Column38"]);
                            ISCDetais.AssessableValue = CommonFunction.NullToDecimalZero(dataRow["Column39"]);
                            ISCDetais.TotalAssessable = CommonFunction.NullToDecimalZero(dataRow["Column40"]);
                            ISCDetais.TotalBasicDuty = CommonFunction.NullToDecimalZero(dataRow["Column41"]);
                            ISCDetais.TotalSurcharge = CommonFunction.NullToDecimalZero(dataRow["Column42"]);
                            ISCDetais.TotalCvd = CommonFunction.NullToDecimalZero(dataRow["Column43"]);
                            ISCDetais.TotalEducationCess = CommonFunction.NullToDecimalZero(dataRow["Column44"]);
                            ISCDetais.TotalEducationCessExcise = CommonFunction.NullToDecimalZero(dataRow["Column45"]);
                            ISCDetais.TotalSocialWelfareSurchargeCustoms = CommonFunction.NullToDecimalZero(dataRow["Column46"]);
                            ISCDetais.TotalSechigherEducationCess = CommonFunction.NullToDecimalZero(dataRow["Column47"]);
                            ISCDetais.TotalSechigherEducationCessExcise = CommonFunction.NullToDecimalZero(dataRow["Column48"]);
                            ISCDetais.TotalSechigherEducationCessCustoms = CommonFunction.NullToDecimalZero(dataRow["Column49"]);
                            ISCDetais.TotalAdditonalDutySubSec5 = CommonFunction.NullToDecimalZero(dataRow["Column50"]);
                            ISCDetais.TotalDuty = CommonFunction.NullToDecimalZero(dataRow["Column51"]);
                            ISCDetais.SplExciseDutySchedIiRate = CommonFunction.NullToDecimalZero(dataRow["Column52"]);
                            ISCDetais.SplExciseDutySchedIiAmount = CommonFunction.NullToDecimalZero(dataRow["Column53"]);
                            ISCDetais.Model = CommonFunction.NullToEmpty(dataRow["Column54"]);
                            ISCDetais.CessdutyRate = CommonFunction.NullToDecimalZero(dataRow["Column55"]);
                            ISCDetais.Cessduty = CommonFunction.NullToDecimalZero(dataRow["Column56"]);
                            ISCDetais.ModeofTransport = CommonFunction.NullToEmpty(dataRow["Column57"]);
                            ISCDetais.Consignor = CommonFunction.NullToEmpty(dataRow["Column58"]);
                            ISCDetais.Igstrate = CommonFunction.NullToDecimalZero(dataRow["Column59"]);
                            ISCDetais.Igstamount = CommonFunction.NullToDecimalZero(dataRow["Column60"]);

                            lstISCDetais.Add(ISCDetais);
                        }
                        break;
                    }

                }
                 
                return lstISCDetais;
            }

            catch (Exception ex)
            {

                throw ex;
            }
        }

        private DateTime? convertstringtoDatetime(string values)
        {
            try
            {
                return Convert.ToDateTime(values);
            }
            catch (Exception ex)
            {

                return null;
            }
        }
        public partial class ChaiscdetailsDto
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
            public decimal? MiscCharge { get; set; }
            public string InvoiceMiscCurrency { get; set; }
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
        }

    }
}
