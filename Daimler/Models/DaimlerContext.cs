using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Daimler.Models
{
    public partial class DaimlerContext : DbContext
    {
        public DaimlerContext()
        {
        }

        public DaimlerContext(DbContextOptions<DaimlerContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AttachmentFileList> AttachmentFileLists { get; set; }
        public virtual DbSet<Chadtdetail> Chadtdetails { get; set; }
        public virtual DbSet<Chaiscdetail> Chaiscdetails { get; set; }
        public virtual DbSet<DutyPaymentRequestDetail> DutyPaymentRequestDetail { get; set; }
        public virtual DbSet<DutyPaymentRequestHeader> DutyPaymentRequestHeaders { get; set; }
        public virtual DbSet<Login> Logins { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                // optionsBuilder.UseSqlServer("Server=DESKTOP-BT3QLP0\\SQLEXPRESS2014;Database=Daimler;Trusted_Connection=True;");
                //optionsBuilder.UseSqlServer("Server=DESKTOP-D8TIGTN\\SQLEXPRESS;Database=Daimler;Trusted_Connection=True;");
                optionsBuilder.UseSqlServer("Server=DESKTOP-ORSQT9S\\SQLEXPRESS2019;Database=Daimler;Trusted_Connection=True;");
                //optionsBuilder.UseSqlServer("Server=DESKTOP-7VEKNVU;Database=Daimler;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<AttachmentFileList>(entity =>
            {
                entity.ToTable("AttachmentFileList");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Binary).HasColumnType("image");

                entity.Property(e => e.Extension).HasMaxLength(50);

                entity.Property(e => e.FileLocation).HasColumnType("ntext");

                entity.Property(e => e.FileName).HasMaxLength(350);

                entity.Property(e => e.SourceId)
                    .HasMaxLength(25)
                    .HasColumnName("SourceID");

                entity.Property(e => e.Type).HasMaxLength(10);

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<Chadtdetail>(entity =>
            {
                entity.ToTable("CHADTDetail");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AddlDutyExciseDutyAmount)
                    .HasColumnType("money")
                    .HasColumnName("AddlDuty_ExciseDuty_Amount");

                entity.Property(e => e.AddlDutyExciseDutyRate)
                    .HasColumnType("money")
                    .HasColumnName("AddlDuty_ExciseDuty_Rate");

                entity.Property(e => e.AddlDutySubSec5Amount)
                    .HasColumnType("money")
                    .HasColumnName("AddlDuty_Sub_Sec5_Amount");

                entity.Property(e => e.AddlDutySubSec5Rate)
                    .HasColumnType("money")
                    .HasColumnName("AddlDuty_Sub_Sec5_Rate");

                entity.Property(e => e.AssessableValue).HasColumnType("money");

                entity.Property(e => e.BasicDuty).HasColumnType("money");

                entity.Property(e => e.BasicDutyRate).HasColumnType("money");

                entity.Property(e => e.Bedate)
                    .HasColumnType("datetime")
                    .HasColumnName("BEDate");

                entity.Property(e => e.Beno)
                    .HasMaxLength(100)
                    .HasColumnName("BENo");

                entity.Property(e => e.Boeid).HasColumnName("BOEID");

                entity.Property(e => e.CentralExciseTariffHeading).HasMaxLength(50);

                entity.Property(e => e.Cessduty)
                    .HasColumnType("money")
                    .HasColumnName("CESSDuty");

                entity.Property(e => e.CessdutyRate)
                    .HasColumnType("money")
                    .HasColumnName("CESSDutyRate");

                entity.Property(e => e.Cifvalue)
                    .HasColumnType("money")
                    .HasColumnName("CIFValue");

                entity.Property(e => e.Consignor).HasMaxLength(250);

                entity.Property(e => e.CustomTariffHeading).HasMaxLength(50);

                entity.Property(e => e.EducationCessAmountExcise)
                    .HasColumnType("money")
                    .HasColumnName("EducationCessAmount_Excise");

                entity.Property(e => e.EducationCessRateExcise)
                    .HasColumnType("money")
                    .HasColumnName("EducationCessRate_Excise");

                entity.Property(e => e.ExchangeRate).HasColumnType("money");

                entity.Property(e => e.Freight).HasColumnType("money");

                entity.Property(e => e.Igstamount)
                    .HasColumnType("money")
                    .HasColumnName("IGSTAmount");

                entity.Property(e => e.Igstrate)
                    .HasColumnType("money")
                    .HasColumnName("IGSTRate");

                entity.Property(e => e.Insurance).HasColumnType("money");

                entity.Property(e => e.InvoiceDate).HasMaxLength(50);

                entity.Property(e => e.InvoiceMiscCurrency)
                    .HasMaxLength(10)
                    .IsFixedLength(true);

                entity.Property(e => e.InvoiceNo).HasMaxLength(100);

                entity.Property(e => e.JobNo).HasMaxLength(100);

                entity.Property(e => e.Loading).HasColumnType("money");

                entity.Property(e => e.MiscCharge).HasColumnType("money");

                entity.Property(e => e.Model).HasMaxLength(50);

                entity.Property(e => e.ModeofTransport).HasMaxLength(150);

                entity.Property(e => e.ProductAmount).HasColumnType("money");

                entity.Property(e => e.ProductDescription).HasMaxLength(250);

                entity.Property(e => e.Quantity).HasColumnType("money");

                entity.Property(e => e.Sadamount)
                    .HasColumnType("money")
                    .HasColumnName("SADAmount");

                entity.Property(e => e.Sadrate)
                    .HasColumnType("money")
                    .HasColumnName("SADRate");

                entity.Property(e => e.SecondaryhigherEducationCessAmountCustoms)
                    .HasColumnType("money")
                    .HasColumnName("SecondaryhigherEducationCessAmount_Customs");

                entity.Property(e => e.SecondaryhigherEducationCessAmountExcise)
                    .HasColumnType("money")
                    .HasColumnName("SecondaryhigherEducationCessAmount_Excise");

                entity.Property(e => e.SecondaryhigherEducationCessRateCustoms)
                    .HasColumnType("money")
                    .HasColumnName("SecondaryhigherEducationCessRate_Customs");

                entity.Property(e => e.SecondaryhigherEducationCessRateExcise)
                    .HasColumnType("money")
                    .HasColumnName("SecondaryhigherEducationCessRate_Excise");

                entity.Property(e => e.SocialWelfareSurchargeAmountCustoms)
                    .HasColumnType("money")
                    .HasColumnName("SocialWelfareSurchargeAmount_Customs");

                entity.Property(e => e.SocialWelfareSurchargeRateCustoms)
                    .HasColumnType("money")
                    .HasColumnName("SocialWelfareSurchargeRate_Customs");

                entity.Property(e => e.SplExciseDutySchedIiAmount)
                    .HasColumnType("money")
                    .HasColumnName("SplExciseDuty_Sched_II_Amount");

                entity.Property(e => e.SplExciseDutySchedIiRate)
                    .HasColumnType("money")
                    .HasColumnName("SplExciseDuty_Sched_II_Rate");

                entity.Property(e => e.TaxCode).HasMaxLength(50);

                entity.Property(e => e.TotalAdditonalDutySubSec5)
                    .HasColumnType("money")
                    .HasColumnName("TotalAdditonalDuty_Sub_Sec5");

                entity.Property(e => e.TotalAssessable).HasColumnType("money");

                entity.Property(e => e.TotalBasicDuty).HasColumnType("money");

                entity.Property(e => e.TotalCvd)
                    .HasColumnType("money")
                    .HasColumnName("TotalCVD");

                entity.Property(e => e.TotalDuty).HasColumnType("money");

                entity.Property(e => e.TotalEducationCess).HasColumnType("money");

                entity.Property(e => e.TotalEducationCessExcise)
                    .HasColumnType("money")
                    .HasColumnName("TotalEducationCess_Excise");

                entity.Property(e => e.TotalFreightCurrency)
                    .HasMaxLength(10)
                    .IsFixedLength(true);

                entity.Property(e => e.TotalFreightValue).HasColumnType("money");

                entity.Property(e => e.TotalInoviceCurrency)
                    .HasMaxLength(10)
                    .IsFixedLength(true);

                entity.Property(e => e.TotalInsCurrency)
                    .HasMaxLength(10)
                    .HasColumnName("TotalIns_Currency")
                    .IsFixedLength(true);

                entity.Property(e => e.TotalInsValue)
                    .HasColumnType("money")
                    .HasColumnName("TotalIns_Value");

                entity.Property(e => e.TotalSad)
                    .HasColumnType("money")
                    .HasColumnName("TotalSAD");

                entity.Property(e => e.TotalSechigherEducationCess).HasColumnType("money");

                entity.Property(e => e.TotalSechigherEducationCessCustoms)
                    .HasColumnType("money")
                    .HasColumnName("TotalSechigherEducationCess_Customs");

                entity.Property(e => e.TotalSechigherEducationCessExcise)
                    .HasColumnType("money")
                    .HasColumnName("TotalSechigherEducationCess_Excise");

                entity.Property(e => e.TotalSocialWelfareSurchargeCustoms)
                    .HasColumnType("money")
                    .HasColumnName("TotalSocialWelfareSurcharge_Customs");

                entity.Property(e => e.TotalSurcharge).HasColumnType("money");

                entity.Property(e => e.TotalnvoiceValue).HasColumnType("money");

                entity.Property(e => e.UnitPrice).HasColumnType("money");

                entity.Property(e => e.UnitofProductQuantity)
                    .HasMaxLength(10)
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<Chaiscdetail>(entity =>
            {
                entity.ToTable("CHAISCDetail");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AddlDutyExciseDutyAmount)
                    .HasColumnType("money")
                    .HasColumnName("AddlDuty_ExciseDuty_Amount");

                entity.Property(e => e.AddlDutyExciseDutyRate)
                    .HasColumnType("money")
                    .HasColumnName("AddlDuty_ExciseDuty_Rate");

                entity.Property(e => e.AddlDutySubSec5Amount)
                    .HasColumnType("money")
                    .HasColumnName("AddlDuty_Sub_Sec5_Amount");

                entity.Property(e => e.AddlDutySubSec5Rate)
                    .HasColumnType("money")
                    .HasColumnName("AddlDuty_Sub_Sec5_Rate");

                entity.Property(e => e.AssessableValue).HasColumnType("money");

                entity.Property(e => e.BasicDuty).HasColumnType("money");

                entity.Property(e => e.BasicDutyRate).HasColumnType("money");

                entity.Property(e => e.Bedate)
                    .HasColumnType("datetime")
                    .HasColumnName("BEDate");

                entity.Property(e => e.Beno)
                    .HasMaxLength(100)
                    .HasColumnName("BENo");

                entity.Property(e => e.Boeid).HasColumnName("BOEID");

                entity.Property(e => e.CentralExciseTariffHeading).HasMaxLength(50);

                entity.Property(e => e.Cessduty)
                    .HasColumnType("money")
                    .HasColumnName("CESSDuty");

                entity.Property(e => e.CessdutyRate)
                    .HasColumnType("money")
                    .HasColumnName("CESSDutyRate");

                entity.Property(e => e.Cifvalue)
                    .HasColumnType("money")
                    .HasColumnName("CIFValue");

                entity.Property(e => e.Consignor).HasMaxLength(250);

                entity.Property(e => e.CustomTariffHeading).HasMaxLength(50);

                entity.Property(e => e.EducationCessAmountExcise)
                    .HasColumnType("money")
                    .HasColumnName("EducationCessAmount_Excise");

                entity.Property(e => e.EducationCessRateExcise)
                    .HasColumnType("money")
                    .HasColumnName("EducationCessRate_Excise");

                entity.Property(e => e.ExchangeRate).HasColumnType("money");

                entity.Property(e => e.Freight).HasColumnType("money");

                entity.Property(e => e.Igstamount)
                    .HasColumnType("money")
                    .HasColumnName("IGSTAmount");

                entity.Property(e => e.Igstrate)
                    .HasColumnType("money")
                    .HasColumnName("IGSTRate");

                entity.Property(e => e.Insurance).HasColumnType("money");

                entity.Property(e => e.InvoiceDate).HasMaxLength(50);

                entity.Property(e => e.InvoiceMiscCurrency)
                    .HasMaxLength(10)
                    .IsFixedLength(true);

                entity.Property(e => e.InvoiceNo).HasMaxLength(100);

                entity.Property(e => e.JobNo).HasMaxLength(100);

                entity.Property(e => e.Loading).HasColumnType("money");

                entity.Property(e => e.MiscCharge).HasColumnType("money");

                entity.Property(e => e.Model).HasMaxLength(50);

                entity.Property(e => e.ModeofTransport).HasMaxLength(150);

                entity.Property(e => e.ProductAmount).HasColumnType("money");

                entity.Property(e => e.ProductDescription).HasMaxLength(250);

                entity.Property(e => e.Quantity).HasColumnType("money");

                entity.Property(e => e.SecondaryhigherEducationCessAmountCustoms)
                    .HasColumnType("money")
                    .HasColumnName("SecondaryhigherEducationCessAmount_Customs");

                entity.Property(e => e.SecondaryhigherEducationCessAmountExcise)
                    .HasColumnType("money")
                    .HasColumnName("SecondaryhigherEducationCessAmount_Excise");

                entity.Property(e => e.SecondaryhigherEducationCessRateCustoms)
                    .HasColumnType("money")
                    .HasColumnName("SecondaryhigherEducationCessRate_Customs");

                entity.Property(e => e.SecondaryhigherEducationCessRateExcise)
                    .HasColumnType("money")
                    .HasColumnName("SecondaryhigherEducationCessRate_Excise");

                entity.Property(e => e.SocialWelfareSurchargeAmountCustoms)
                    .HasColumnType("money")
                    .HasColumnName("SocialWelfareSurchargeAmount_Customs");

                entity.Property(e => e.SocialWelfareSurchargeRateCustoms)
                    .HasColumnType("money")
                    .HasColumnName("SocialWelfareSurchargeRate_Customs");

                entity.Property(e => e.SplExciseDutySchedIiAmount)
                    .HasColumnType("money")
                    .HasColumnName("SplExciseDuty_Sched_II_Amount");

                entity.Property(e => e.SplExciseDutySchedIiRate)
                    .HasColumnType("money")
                    .HasColumnName("SplExciseDuty_Sched_II_Rate");

                entity.Property(e => e.TotalAdditonalDutySubSec5)
                    .HasColumnType("money")
                    .HasColumnName("TotalAdditonalDuty_Sub_Sec5");

                entity.Property(e => e.TotalAssessable).HasColumnType("money");

                entity.Property(e => e.TotalBasicDuty).HasColumnType("money");

                entity.Property(e => e.TotalCvd)
                    .HasColumnType("money")
                    .HasColumnName("TotalCVD");

                entity.Property(e => e.TotalDuty).HasColumnType("money");

                entity.Property(e => e.TotalEducationCess).HasColumnType("money");

                entity.Property(e => e.TotalEducationCessExcise)
                    .HasColumnType("money")
                    .HasColumnName("TotalEducationCess_Excise");

                entity.Property(e => e.TotalFreightCurrency)
                    .HasMaxLength(10)
                    .IsFixedLength(true);

                entity.Property(e => e.TotalFreightValue).HasColumnType("money");

                entity.Property(e => e.TotalInoviceCurrency)
                    .HasMaxLength(10)
                    .IsFixedLength(true);

                entity.Property(e => e.TotalInsCurrency)
                    .HasMaxLength(10)
                    .HasColumnName("TotalIns_Currency")
                    .IsFixedLength(true);

                entity.Property(e => e.TotalInsValue)
                    .HasColumnType("money")
                    .HasColumnName("TotalIns_Value");

                entity.Property(e => e.TotalSechigherEducationCess).HasColumnType("money");

                entity.Property(e => e.TotalSechigherEducationCessCustoms)
                    .HasColumnType("money")
                    .HasColumnName("TotalSechigherEducationCess_Customs");

                entity.Property(e => e.TotalSechigherEducationCessExcise)
                    .HasColumnType("money")
                    .HasColumnName("TotalSechigherEducationCess_Excise");

                entity.Property(e => e.TotalSocialWelfareSurchargeCustoms)
                    .HasColumnType("money")
                    .HasColumnName("TotalSocialWelfareSurcharge_Customs");

                entity.Property(e => e.TotalSurcharge).HasColumnType("money");

                entity.Property(e => e.TotalnvoiceValue).HasColumnType("money");

                entity.Property(e => e.UnitPrice).HasColumnType("money");

                entity.Property(e => e.UnitofProductQuantity)
                    .HasMaxLength(10)
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<DutyPaymentRequestDetail>(entity =>
            {
                entity.ToTable("DutyPaymentRequestDetail");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Boeduty)
                    .HasColumnType("money")
                    .HasColumnName("BOEDuty");

                entity.Property(e => e.Boeno)
                    .HasMaxLength(250)
                    .HasColumnName("BOENo");

                entity.Property(e => e.DutyValue).HasColumnType("money");

                entity.Property(e => e.Fine).HasColumnType("money");

                entity.Property(e => e.HeaderId).HasColumnName("HeaderID");

                entity.Property(e => e.IdtExcelAttachmentId)
                    .HasMaxLength(1000)
                    .HasColumnName("IDT_Excel_AttachmentID");

                entity.Property(e => e.IdtPdfAttachmentId).HasColumnName("IDT_PDF_AttachmentID");

                entity.Property(e => e.Interest).HasColumnType("money");

                entity.Property(e => e.InvoiceNo).HasMaxLength(250);

                entity.Property(e => e.IscExcelAttachmentId).HasColumnName("ISC_Excel_AttachmentID");

                entity.Property(e => e.IscPdfAttachmentId).HasColumnName("ISC_PDF_AttachmentID");

                entity.Property(e => e.LineId).HasColumnName("LineID");

                entity.Property(e => e.Penalty).HasColumnType("money");

                entity.Property(e => e.PortCode).HasMaxLength(50);

                entity.Property(e => e.RefNo).HasMaxLength(250);

                entity.HasOne(d => d.Header)
                    .WithMany(p => p.DutyPaymentRequestDetails)
                    .HasForeignKey(d => d.HeaderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DutyPaymentRequestDetail_DutyPaymentRequestHeaderID");

                entity.HasOne(d => d.IdtPdfAttachment)
                    .WithMany(p => p.DutyPaymentRequestDetailIdtPdfAttachments)
                    .HasForeignKey(d => d.IdtPdfAttachmentId)
                    .HasConstraintName("FK_DutyPaymentRequestDetail_DutyPaymentRequestHeaderIDT_PDF_AttachmentID");

                entity.HasOne(d => d.IscExcelAttachment)
                    .WithMany(p => p.DutyPaymentRequestDetailIscExcelAttachments)
                    .HasForeignKey(d => d.IscExcelAttachmentId)
                    .HasConstraintName("FK_DutyPaymentRequestDetail_DutyPaymentRequestHeaderISC_Excel_AttachmentID");

                entity.HasOne(d => d.IscPdfAttachment)
                    .WithMany(p => p.DutyPaymentRequestDetailIscPdfAttachments)
                    .HasForeignKey(d => d.IscPdfAttachmentId)
                    .HasConstraintName("FK_DutyPaymentRequestDetail_DutyPaymentRequestHeaderISC_PDF_AttachmentID");
            });

            modelBuilder.Entity<DutyPaymentRequestHeader>(entity =>
            {
                entity.ToTable("DutyPaymentRequestHeader");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DocumentReference).HasMaxLength(350);

                entity.Property(e => e.Dprno)
                    .HasMaxLength(350)
                    .HasColumnName("DPRNo");

                entity.Property(e => e.FileName).HasMaxLength(250);

                entity.Property(e => e.Status)
                    .HasMaxLength(10)
                    .IsFixedLength(true);

                entity.Property(e => e.UploadedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<Login>(entity =>
            {
                entity.ToTable("Login");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
