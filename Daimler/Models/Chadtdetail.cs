using System;
using System.Collections.Generic;

#nullable disable

namespace Daimler.Models
{
    public partial class Chadtdetail
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
        public decimal? Sadrate { get; set; }
        public decimal? Sadamount { get; set; }
        public decimal? TotalSad { get; set; }
        public string TaxCode { get; set; }
        public string BENo_IDT { get; set; }
        public int? AttachmentID { get; set; }
    }
}
