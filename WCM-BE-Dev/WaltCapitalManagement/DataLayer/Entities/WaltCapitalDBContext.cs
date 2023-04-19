using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DataLayer.Entities
{
    public partial class WaltCapitalDBContext : DbContext
    {
        public WaltCapitalDBContext()
        {
        }

        public WaltCapitalDBContext(DbContextOptions<WaltCapitalDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AccessCategoryMst> AccessCategoryMsts { get; set; } = null!;
        public virtual DbSet<AccessCategoryPermissionMst> AccessCategoryPermissionMsts { get; set; } = null!;
        public virtual DbSet<AccessCategoryTypeMst> AccessCategoryTypeMsts { get; set; } = null!;
        public virtual DbSet<AccountTypeMst> AccountTypeMsts { get; set; } = null!;
        public virtual DbSet<ActivityLogMst> ActivityLogMsts { get; set; } = null!;
        public virtual DbSet<Amlmst> Amlmsts { get; set; } = null!;
        public virtual DbSet<CityCustomMst> CityCustomMsts { get; set; } = null!;
        public virtual DbSet<CityMst> CityMsts { get; set; } = null!;
        public virtual DbSet<ClientCsvdataLogMst> ClientCsvdataLogMsts { get; set; } = null!;
        public virtual DbSet<ClientCsvdataMst> ClientCsvdataMsts { get; set; } = null!;
        public virtual DbSet<ClientTransactionMst> ClientTransactionMsts { get; set; } = null!;
        public virtual DbSet<ClientTypeMst> ClientTypeMsts { get; set; } = null!;
        public virtual DbSet<CountryCustomMst> CountryCustomMsts { get; set; } = null!;
        public virtual DbSet<CountryMst> CountryMsts { get; set; } = null!;
        public virtual DbSet<CsvdataMst> CsvdataMsts { get; set; } = null!;
        public virtual DbSet<CsvfileUploadLogMst> CsvfileUploadLogMsts { get; set; } = null!;
        public virtual DbSet<CurrencyMst> CurrencyMsts { get; set; } = null!;
        public virtual DbSet<DailyEventEmailMst> DailyEventEmailMsts { get; set; } = null!;
        public virtual DbSet<DisclaimerMst> DisclaimerMsts { get; set; } = null!;
        public virtual DbSet<DocumentTypeMst> DocumentTypeMsts { get; set; } = null!;
        public virtual DbSet<DueDiligenceMst> DueDiligenceMsts { get; set; } = null!;
        public virtual DbSet<ExternalAccountDetail> ExternalAccountDetails { get; set; } = null!;
        public virtual DbSet<FactSheetMst> FactSheetMsts { get; set; } = null!;
        public virtual DbSet<FundBenchMarkMst> FundBenchMarkMsts { get; set; } = null!;
        public virtual DbSet<FundDynamicFieldHistoryMst> FundDynamicFieldHistoryMsts { get; set; } = null!;
        public virtual DbSet<FundDynamicFieldMst> FundDynamicFieldMsts { get; set; } = null!;
        public virtual DbSet<FundDynamicInputPriceMst> FundDynamicInputPriceMsts { get; set; } = null!;
        public virtual DbSet<FundFeeCalculationDetail> FundFeeCalculationDetails { get; set; } = null!;
        public virtual DbSet<FundFeesMst> FundFeesMsts { get; set; } = null!;
        public virtual DbSet<FundHistoryMst> FundHistoryMsts { get; set; } = null!;
        public virtual DbSet<FundMst> FundMsts { get; set; } = null!;
        public virtual DbSet<Ifamst> Ifamsts { get; set; } = null!;
        public virtual DbSet<LinkMst> LinkMsts { get; set; } = null!;
        public virtual DbSet<MeetingMst> MeetingMsts { get; set; } = null!;
        public virtual DbSet<OfficeMst> OfficeMsts { get; set; } = null!;
        public virtual DbSet<PermissionCredentialMst> PermissionCredentialMsts { get; set; } = null!;
        public virtual DbSet<PersonalityTypeMst> PersonalityTypeMsts { get; set; } = null!;
        public virtual DbSet<PricingMst> PricingMsts { get; set; } = null!;
        public virtual DbSet<RoleMst> RoleMsts { get; set; } = null!;
        public virtual DbSet<RunFeesDetail> RunFeesDetails { get; set; } = null!;
        public virtual DbSet<ServiceProviderMst> ServiceProviderMsts { get; set; } = null!;
        public virtual DbSet<ServiceProviderTypeMst> ServiceProviderTypeMsts { get; set; } = null!;
        public virtual DbSet<StateCustomMst> StateCustomMsts { get; set; } = null!;
        public virtual DbSet<StateMst> StateMsts { get; set; } = null!;
        public virtual DbSet<UserDocumentMst> UserDocumentMsts { get; set; } = null!;
        public virtual DbSet<UserMst> UserMsts { get; set; } = null!;
        public virtual DbSet<UserTokenMst> UserTokenMsts { get; set; } = null!;
        public virtual DbSet<WaltCapConsultantMst> WaltCapConsultantMsts { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=192.168.1.199,1433;user=sa;password=sa@2022;Database=WaltCapitalDB;Trusted_Connection=False;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccessCategoryMst>(entity =>
            {
                entity.ToTable("AccessCategoryMst");

                entity.Property(e => e.AccessCategory).HasMaxLength(100);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<AccessCategoryPermissionMst>(entity =>
            {
                entity.ToTable("AccessCategoryPermissionMst");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<AccessCategoryTypeMst>(entity =>
            {
                entity.ToTable("AccessCategoryTypeMst");

                entity.Property(e => e.AccessCategoryType).HasMaxLength(100);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<AccountTypeMst>(entity =>
            {
                entity.ToTable("AccountTypeMst");

                entity.Property(e => e.AccountType).HasMaxLength(100);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<ActivityLogMst>(entity =>
            {
                entity.ToTable("ActivityLogMst");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Apiurl).HasColumnName("APIURL");

                entity.Property(e => e.ExecutionDate).HasColumnType("datetime");

                entity.Property(e => e.MethodType).HasMaxLength(10);
            });

            modelBuilder.Entity<Amlmst>(entity =>
            {
                entity.ToTable("AMLMst");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.ValidTill).HasColumnType("datetime");
            });

            modelBuilder.Entity<CityCustomMst>(entity =>
            {
                entity.HasKey(e => e.CityId)
                    .HasName("PK__CityCust__F2D21B76FCD2EB5F");

                entity.ToTable("CityCustomMst");

                entity.Property(e => e.CityName).HasMaxLength(255);
            });

            modelBuilder.Entity<CityMst>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("CityMst");

                entity.Property(e => e.CityName).HasMaxLength(250);

                entity.Property(e => e.Latitude).HasMaxLength(250);

                entity.Property(e => e.Longitude).HasMaxLength(250);
            });

            modelBuilder.Entity<ClientCsvdataLogMst>(entity =>
            {
                entity.ToTable("ClientCSVDataLogMst");

                entity.Property(e => e.ClientCsvfileName)
                    .HasMaxLength(255)
                    .HasColumnName("ClientCSVFileName");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Extension).HasMaxLength(10);

                entity.Property(e => e.FileSize).HasMaxLength(50);
            });

            modelBuilder.Entity<ClientCsvdataMst>(entity =>
            {
                entity.ToTable("ClientCSVDataMst");

                entity.Property(e => e.AccountFundAllocation).HasColumnName("AccountFUndAllocation");

                entity.Property(e => e.AdviserCode).HasMaxLength(255);

                entity.Property(e => e.AnnuityIncomeAnniversary).HasColumnType("datetime");

                entity.Property(e => e.ClientName).HasMaxLength(255);

                entity.Property(e => e.GroupRaemployer).HasColumnName("GroupRAEmployer");

                entity.Property(e => e.InceptionDate).HasColumnType("datetime");

                entity.Property(e => e.PriceDate).HasColumnType("datetime");

                entity.Property(e => e.Section14AdvisorFeeRenewal).HasColumnType("datetime");

                entity.Property(e => e.UnitPriceCents).HasColumnName("UnitPrice(Cents)");
            });

            modelBuilder.Entity<ClientTransactionMst>(entity =>
            {
                entity.ToTable("ClientTransactionMst");

                entity.Property(e => e.AllocateTo).HasMaxLength(255);

                entity.Property(e => e.AmountBalance)
                    .HasColumnType("decimal(38, 8)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Ifa).HasColumnName("IFA");

                entity.Property(e => e.IfaupFrontFee).HasColumnName("IFAUpFrontFee");

                entity.Property(e => e.TransactionDate).HasColumnType("datetime");

                entity.Property(e => e.TransactionDateTime).HasColumnType("datetime");

                entity.Property(e => e.TransactionIn)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.TransactionType)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.UnitBalance)
                    .HasColumnType("decimal(38, 8)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.UnitType).HasMaxLength(50);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<ClientTypeMst>(entity =>
            {
                entity.ToTable("ClientTypeMst");

                entity.Property(e => e.ClientType).HasMaxLength(100);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<CountryCustomMst>(entity =>
            {
                entity.HasKey(e => e.CountryId)
                    .HasName("PK__CountryC__10D1609F1228B553");

                entity.ToTable("CountryCustomMst");

                entity.Property(e => e.CountryName).HasMaxLength(255);
            });

            modelBuilder.Entity<CountryMst>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("CountryMst");

                entity.Property(e => e.Capital).HasMaxLength(250);

                entity.Property(e => e.CountryName).HasMaxLength(250);

                entity.Property(e => e.Currency).HasMaxLength(250);

                entity.Property(e => e.CurrencyName).HasMaxLength(250);

                entity.Property(e => e.CurrencySymbol).HasMaxLength(250);

                entity.Property(e => e.Emoji).HasMaxLength(250);

                entity.Property(e => e.EmojiU).HasMaxLength(250);

                entity.Property(e => e.Iso2).HasMaxLength(250);

                entity.Property(e => e.Iso3).HasMaxLength(250);

                entity.Property(e => e.Latitude).HasMaxLength(250);

                entity.Property(e => e.Longitude).HasMaxLength(250);

                entity.Property(e => e.Native).HasMaxLength(250);

                entity.Property(e => e.NumericCode).HasMaxLength(250);

                entity.Property(e => e.PhoneCode).HasMaxLength(250);

                entity.Property(e => e.Region).HasMaxLength(250);

                entity.Property(e => e.SubRegion).HasMaxLength(250);

                entity.Property(e => e.Tld).HasMaxLength(250);
            });

            modelBuilder.Entity<CsvdataMst>(entity =>
            {
                entity.ToTable("CSVDataMst");

                entity.Property(e => e.AccountNo).HasMaxLength(50);

                entity.Property(e => e.Category).HasMaxLength(255);

                entity.Property(e => e.CsvfileId).HasColumnName("CSVFileId");

                entity.Property(e => e.InvDate).HasColumnType("datetime");

                entity.Property(e => e.Share).HasMaxLength(255);

                entity.Property(e => e.Surname).HasMaxLength(255);
            });

            modelBuilder.Entity<CsvfileUploadLogMst>(entity =>
            {
                entity.ToTable("CSVFileUploadLogMst");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.CsvfileName)
                    .HasMaxLength(255)
                    .HasColumnName("CSVFileName");

                entity.Property(e => e.Extension).HasMaxLength(10);

                entity.Property(e => e.FileSize).HasMaxLength(50);
            });

            modelBuilder.Entity<CurrencyMst>(entity =>
            {
                entity.ToTable("CurrencyMst");

                entity.Property(e => e.BaseValue).HasMaxLength(250);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.CurrencyName).HasMaxLength(50);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<DailyEventEmailMst>(entity =>
            {
                entity.ToTable("DailyEventEmailMst");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.EmailFor).HasMaxLength(250);

                entity.Property(e => e.Gender).HasMaxLength(250);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<DisclaimerMst>(entity =>
            {
                entity.ToTable("DisclaimerMst");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.ValidTill).HasColumnType("datetime");
            });

            modelBuilder.Entity<DocumentTypeMst>(entity =>
            {
                entity.ToTable("DocumentTypeMst");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DocumentTypeName).HasMaxLength(250);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<DueDiligenceMst>(entity =>
            {
                entity.ToTable("DueDiligenceMst");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.ValidTill).HasColumnType("datetime");
            });

            modelBuilder.Entity<ExternalAccountDetail>(entity =>
            {
                entity.Property(e => e.AccountCode).HasMaxLength(100);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<FactSheetMst>(entity =>
            {
                entity.ToTable("FactSheetMst");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Email).HasMaxLength(250);

                entity.Property(e => e.Fsp)
                    .HasMaxLength(250)
                    .HasColumnName("FSP");

                entity.Property(e => e.InceptionDate).HasColumnType("datetime");

                entity.Property(e => e.PortfolioManager).HasMaxLength(250);

                entity.Property(e => e.Recommended).HasMaxLength(250);

                entity.Property(e => e.Sector).HasMaxLength(250);

                entity.Property(e => e.Telephone).HasMaxLength(250);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<FundBenchMarkMst>(entity =>
            {
                entity.ToTable("FundBenchMarkMst");

                entity.Property(e => e.BenchMarkDate).HasColumnType("datetime");

                entity.Property(e => e.BenchMarkName).HasMaxLength(255);

                entity.Property(e => e.BenchMarkValue).HasColumnType("decimal(38, 8)");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.IsAddMode)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.IsDeleted)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<FundDynamicFieldHistoryMst>(entity =>
            {
                entity.ToTable("FundDynamicFieldHistoryMst");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Label).HasMaxLength(255);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.Value).HasMaxLength(255);
            });

            modelBuilder.Entity<FundDynamicFieldMst>(entity =>
            {
                entity.ToTable("FundDynamicFieldMst");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Label).HasMaxLength(255);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.Value).HasMaxLength(255);
            });

            modelBuilder.Entity<FundDynamicInputPriceMst>(entity =>
            {
                entity.ToTable("FundDynamicInputPriceMst");

                entity.Property(e => e.BalanceDate).HasColumnType("datetime");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Label).HasMaxLength(255);

                entity.Property(e => e.UnitType).HasMaxLength(255);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.Value).HasColumnType("decimal(38, 8)");
            });

            modelBuilder.Entity<FundFeeCalculationDetail>(entity =>
            {
                entity.Property(e => e.AuditFees).HasColumnType("decimal(38, 8)");

                entity.Property(e => e.BalanceDate).HasColumnType("datetime");

                entity.Property(e => e.BankBalance).HasColumnType("decimal(38, 8)");

                entity.Property(e => e.ComplianceFees).HasColumnType("decimal(38, 8)");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DynPrcInpLabel).HasMaxLength(255);

                entity.Property(e => e.FeesPaid).HasColumnType("decimal(38, 8)");

                entity.Property(e => e.FeesPayable).HasColumnType("decimal(38, 8)");

                entity.Property(e => e.Hwm)
                    .HasColumnType("decimal(38, 8)")
                    .HasColumnName("HWM");

                entity.Property(e => e.IfaannualFee)
                    .HasColumnType("decimal(38, 8)")
                    .HasColumnName("IFAAnnualFee");

                entity.Property(e => e.Ifafees)
                    .HasColumnType("decimal(38, 8)")
                    .HasColumnName("IFAFees");

                entity.Property(e => e.IfainitialFee)
                    .HasColumnType("decimal(38, 8)")
                    .HasColumnName("IFAInitialFee");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.ManFees).HasColumnType("decimal(38, 8)");

                entity.Property(e => e.PerfFees).HasColumnType("decimal(38, 8)");

                entity.Property(e => e.PostUnits).HasColumnType("decimal(38, 8)");

                entity.Property(e => e.PreUnits).HasColumnType("decimal(38, 8)");

                entity.Property(e => e.ProfitOffHwm)
                    .HasColumnType("decimal(38, 8)")
                    .HasColumnName("ProfitOffHWM");

                entity.Property(e => e.StartValue).HasColumnType("decimal(38, 8)");

                entity.Property(e => e.TotalFeesDue).HasColumnType("decimal(38, 8)");

                entity.Property(e => e.TotalTheorValue).HasColumnType("decimal(38, 8)");

                entity.Property(e => e.TstheoValue)
                    .HasColumnType("decimal(38, 8)")
                    .HasColumnName("TSTheoValue");

                entity.Property(e => e.TstotalValue)
                    .HasColumnType("decimal(38, 8)")
                    .HasColumnName("TSTotalValue");

                entity.Property(e => e.UnitChanges).HasColumnType("decimal(38, 8)");

                entity.Property(e => e.UnitPrice).HasColumnType("decimal(38, 8)");

                entity.Property(e => e.UnitPriceNav).HasColumnType("decimal(38, 8)");

                entity.Property(e => e.UnitType).HasMaxLength(255);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<FundFeesMst>(entity =>
            {
                entity.ToTable("FundFeesMst");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.FeesName).HasMaxLength(255);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<FundHistoryMst>(entity =>
            {
                entity.ToTable("FundHistoryMst");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Currency).HasMaxLength(250);

                entity.Property(e => e.FundName).HasMaxLength(250);

                entity.Property(e => e.InceptionDate).HasColumnType("datetime");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.IsVatapplicable).HasColumnName("IsVATApplicable");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.Vat).HasColumnName("VAT");
            });

            modelBuilder.Entity<FundMst>(entity =>
            {
                entity.ToTable("FundMst");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Currency).HasMaxLength(250);

                entity.Property(e => e.FundName).HasMaxLength(250);

                entity.Property(e => e.InceptionDate).HasColumnType("datetime");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.IsVatapplicable).HasColumnName("IsVATApplicable");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.Vat).HasColumnName("VAT");
            });

            modelBuilder.Entity<Ifamst>(entity =>
            {
                entity.ToTable("IFAMst");

                entity.Property(e => e.BuildingName).HasMaxLength(250);

                entity.Property(e => e.City).HasMaxLength(250);

                entity.Property(e => e.CompRegNumber).HasMaxLength(250);

                entity.Property(e => e.CompanyName).HasMaxLength(250);

                entity.Property(e => e.Consultant).HasMaxLength(250);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DateOfBirth).HasColumnType("datetime");

                entity.Property(e => e.Email).HasMaxLength(250);

                entity.Property(e => e.FirstName).HasMaxLength(250);

                entity.Property(e => e.FloorOfficeNumber).HasMaxLength(250);

                entity.Property(e => e.FscaregistrationNo)
                    .HasMaxLength(250)
                    .HasColumnName("FSCARegistrationNo");

                entity.Property(e => e.Ifa)
                    .HasMaxLength(100)
                    .HasColumnName("IFA");

                entity.Property(e => e.IfapractiseNo)
                    .HasMaxLength(250)
                    .HasColumnName("IFAPractiseNo");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.IsFscaregistration).HasColumnName("IsFSCARegistration");

                entity.Property(e => e.LastDate).HasMaxLength(250);

                entity.Property(e => e.MobileNumber).HasMaxLength(250);

                entity.Property(e => e.PersonChecked).HasMaxLength(250);

                entity.Property(e => e.PositionHeld).HasMaxLength(250);

                entity.Property(e => e.Postalcode).HasMaxLength(250);

                entity.Property(e => e.Province).HasMaxLength(250);

                entity.Property(e => e.ResponsiblePersonTitle).HasMaxLength(250);

                entity.Property(e => e.SarstaxNumber)
                    .HasMaxLength(250)
                    .HasColumnName("SARSTaxNumber");

                entity.Property(e => e.StreetName).HasMaxLength(250);

                entity.Property(e => e.Suburb).HasMaxLength(250);

                entity.Property(e => e.Surname).HasMaxLength(250);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.Vatnumber)
                    .HasMaxLength(250)
                    .HasColumnName("VATNumber");

                entity.Property(e => e.WorkNumber).HasMaxLength(250);
            });

            modelBuilder.Entity<LinkMst>(entity =>
            {
                entity.ToTable("LinkMst");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ExpiredDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<MeetingMst>(entity =>
            {
                entity.ToTable("MeetingMst");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.ReminderDate).HasColumnType("datetime");

                entity.Property(e => e.ReminderTime).HasColumnType("datetime");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.Venue).HasMaxLength(255);
            });

            modelBuilder.Entity<OfficeMst>(entity =>
            {
                entity.ToTable("OfficeMst");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Office).HasMaxLength(100);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<PermissionCredentialMst>(entity =>
            {
                entity.ToTable("PermissionCredentialMst");

                entity.Property(e => e.Pwd).HasMaxLength(255);
            });

            modelBuilder.Entity<PersonalityTypeMst>(entity =>
            {
                entity.ToTable("PersonalityTypeMst");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.PersonalityType).HasMaxLength(255);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<PricingMst>(entity =>
            {
                entity.ToTable("PricingMst");

                entity.Property(e => e.AuditFees).HasColumnType("decimal(38, 8)");

                entity.Property(e => e.ComplianceFees).HasColumnType("decimal(38, 8)");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DynPrcInpTotal).HasColumnType("decimal(38, 8)");

                entity.Property(e => e.FeesPayable).HasColumnType("decimal(38, 8)");

                entity.Property(e => e.Hwm)
                    .HasColumnType("decimal(38, 8)")
                    .HasColumnName("HWM");

                entity.Property(e => e.Ifafees)
                    .HasColumnType("decimal(38, 8)")
                    .HasColumnName("IFAFees");

                entity.Property(e => e.InceptionDate).HasColumnType("datetime");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.ManFees).HasColumnType("decimal(38, 8)");

                entity.Property(e => e.PerfFees).HasColumnType("decimal(38, 8)");

                entity.Property(e => e.TotalFeesDue).HasColumnType("decimal(38, 8)");

                entity.Property(e => e.TotalTheorValue).HasColumnType("decimal(38, 8)");

                entity.Property(e => e.TransactionDate).HasColumnType("datetime");

                entity.Property(e => e.UnitPriceNav).HasColumnType("decimal(38, 8)");

                entity.Property(e => e.UnitType).HasMaxLength(255);

                entity.Property(e => e.Units).HasColumnType("decimal(38, 8)");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<RoleMst>(entity =>
            {
                entity.ToTable("RoleMst");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.RoleName).HasMaxLength(50);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<RunFeesDetail>(entity =>
            {
                entity.ToTable("RunFeesDetail");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.LastAmount).HasColumnType("decimal(38, 8)");

                entity.Property(e => e.LastRunDate).HasColumnType("datetime");

                entity.Property(e => e.NextRunDate).HasColumnType("datetime");

                entity.Property(e => e.PendingAmount).HasColumnType("decimal(38, 8)");

                entity.Property(e => e.Total).HasColumnType("decimal(38, 8)");

                entity.Property(e => e.TotalIncVat)
                    .HasColumnType("decimal(38, 8)")
                    .HasColumnName("TotalIncVAT");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.Vat)
                    .HasColumnType("decimal(38, 8)")
                    .HasColumnName("VAT");
            });

            modelBuilder.Entity<ServiceProviderMst>(entity =>
            {
                entity.ToTable("ServiceProviderMst");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.ServiceProvider).HasMaxLength(100);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<ServiceProviderTypeMst>(entity =>
            {
                entity.ToTable("ServiceProviderTypeMst");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.ServiceProviderType).HasMaxLength(100);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<StateCustomMst>(entity =>
            {
                entity.HasKey(e => e.StateId)
                    .HasName("PK__StateCus__C3BA3B3AF3D227E8");

                entity.ToTable("StateCustomMst");

                entity.Property(e => e.StateName).HasMaxLength(255);
            });

            modelBuilder.Entity<StateMst>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("StateMst");

                entity.Property(e => e.Latitude).HasMaxLength(250);

                entity.Property(e => e.Longitude).HasMaxLength(250);

                entity.Property(e => e.StateCode).HasMaxLength(250);

                entity.Property(e => e.StateName).HasMaxLength(250);
            });

            modelBuilder.Entity<UserDocumentMst>(entity =>
            {
                entity.ToTable("UserDocumentMst");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<UserMst>(entity =>
            {
                entity.ToTable("UserMst");

                entity.Property(e => e.AccountHolder).HasMaxLength(50);

                entity.Property(e => e.AccountNo).HasMaxLength(50);

                entity.Property(e => e.AdminMonthlyFee).HasMaxLength(50);

                entity.Property(e => e.AmlupdatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("AMLUpdatedDate");

                entity.Property(e => e.AnnualAdvisorFees).HasMaxLength(250);

                entity.Property(e => e.AnnualManagementFee).HasMaxLength(50);

                entity.Property(e => e.Bank).HasMaxLength(50);

                entity.Property(e => e.BranchCode).HasMaxLength(50);

                entity.Property(e => e.BrokerageRate).HasMaxLength(50);

                entity.Property(e => e.City).HasMaxLength(50);

                entity.Property(e => e.ClientAccNo).HasMaxLength(50);

                entity.Property(e => e.CompRegNumber).HasMaxLength(50);

                entity.Property(e => e.CompanyName).HasMaxLength(250);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Dcs).HasColumnName("DCS");

                entity.Property(e => e.Dob)
                    .HasColumnType("datetime")
                    .HasColumnName("DOB");

                entity.Property(e => e.DueDiligenceUpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.Faserial)
                    .HasMaxLength(100)
                    .HasColumnName("FASerial");

                entity.Property(e => e.FirstName).HasMaxLength(20);

                entity.Property(e => e.FlatBrokerageRate).HasMaxLength(50);

                entity.Property(e => e.FloorandOfficeNo).HasMaxLength(50);

                entity.Property(e => e.Fsca)
                    .HasMaxLength(50)
                    .HasColumnName("FSCA");

                entity.Property(e => e.HomeName).HasMaxLength(100);

                entity.Property(e => e.Idnumber)
                    .HasMaxLength(50)
                    .HasColumnName("IDNumber");

                entity.Property(e => e.Ifa).HasColumnName("IFA");

                entity.Property(e => e.InitialFee).HasMaxLength(50);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.IsAml).HasColumnName("IsAML");

                entity.Property(e => e.IsFscaactive).HasColumnName("IsFSCAactive");

                entity.Property(e => e.IsVatapplicable).HasColumnName("IsVATApplicable");

                entity.Property(e => e.LastDateChecked).HasColumnType("datetime");

                entity.Property(e => e.LastName).HasMaxLength(20);

                entity.Property(e => e.MaritalStatus).HasMaxLength(50);

                entity.Property(e => e.Mcs).HasColumnName("MCS");

                entity.Property(e => e.MiddleName).HasMaxLength(255);

                entity.Property(e => e.MobileNo).HasMaxLength(20);

                entity.Property(e => e.NickName).HasMaxLength(50);

                entity.Property(e => e.PerformanceFee).HasMaxLength(50);

                entity.Property(e => e.PersonChecked).HasMaxLength(250);

                entity.Property(e => e.PositionHeld).HasMaxLength(50);

                entity.Property(e => e.PostalCode).HasMaxLength(10);

                entity.Property(e => e.ResponsiblePerson).HasMaxLength(50);

                entity.Property(e => e.Role).HasMaxLength(50);

                entity.Property(e => e.Salutation).HasMaxLength(10);

                entity.Property(e => e.SarstaxNo)
                    .HasMaxLength(50)
                    .HasColumnName("SARSTaxNo");

                entity.Property(e => e.SpouseDob)
                    .HasColumnType("datetime")
                    .HasColumnName("SpouseDOB");

                entity.Property(e => e.SpouseName).HasMaxLength(100);

                entity.Property(e => e.StreetName).HasMaxLength(100);

                entity.Property(e => e.StreetNo).HasMaxLength(50);

                entity.Property(e => e.Suburb).HasMaxLength(50);

                entity.Property(e => e.SwiftCode).HasMaxLength(50);

                entity.Property(e => e.Tfsa).HasColumnName("TFSA");

                entity.Property(e => e.TrustRegNo).HasMaxLength(50);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.Vatno)
                    .HasMaxLength(50)
                    .HasColumnName("VATNo");

                entity.Property(e => e.WcfundAdministration).HasColumnName("WCFundAdministration");

                entity.Property(e => e.WorkNo).HasMaxLength(20);
            });

            modelBuilder.Entity<UserTokenMst>(entity =>
            {
                entity.ToTable("UserTokenMst");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ExpiredOn).HasColumnType("datetime");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<WaltCapConsultantMst>(entity =>
            {
                entity.ToTable("WaltCapConsultantMst");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.WaltCapConsultant).HasMaxLength(100);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
