using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Entities;

public partial class MedicalBillingDbContext : DbContext
{
    public MedicalBillingDbContext()
    {
    }

    public MedicalBillingDbContext(DbContextOptions<MedicalBillingDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ActivityLogMst> ActivityLogMsts { get; set; }

    public virtual DbSet<AppointmentMst> AppointmentMsts { get; set; }

    public virtual DbSet<CallTypeMst> CallTypeMsts { get; set; }

    public virtual DbSet<ClientMst> ClientMsts { get; set; }

    public virtual DbSet<DurationMst> DurationMsts { get; set; }

    public virtual DbSet<ExtensionMst> ExtensionMsts { get; set; }

    public virtual DbSet<NotificationMst> NotificationMsts { get; set; }

    public virtual DbSet<PatientEmailMst> PatientEmailMsts { get; set; }

    public virtual DbSet<PhysicianMst> PhysicianMsts { get; set; }

    public virtual DbSet<RemarkMst> RemarkMsts { get; set; }

    public virtual DbSet<RoleMst> RoleMsts { get; set; }

    public virtual DbSet<UserMst> UserMsts { get; set; }

    public virtual DbSet<UserTokenMst> UserTokenMsts { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Server=192.168.1.199,1433;user=sa;password=sa@2022;Database=MedicalBillingDB;Encrypt=False;Trusted_Connection=false;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ActivityLogMst>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Activity__3214EC276E31F865");

            entity.ToTable("ActivityLogMst");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Apiurl).HasColumnName("APIURL");
            entity.Property(e => e.ExecutionDate).HasColumnType("datetime");
            entity.Property(e => e.MethodType).HasMaxLength(10);
        });

        modelBuilder.Entity<AppointmentMst>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Appointm__3214EC070C65B25D");

            entity.ToTable("AppointmentMst");

            entity.Property(e => e.AccountNo).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.ActualAppoitmentDate).HasColumnType("datetime");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.DoctorGender).HasMaxLength(20);
            entity.Property(e => e.IdExpirationDate).HasColumnType("datetime");
            entity.Property(e => e.IsActive)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            entity.Property(e => e.IsAppoitmentVehicleOrworkInjury).HasDefaultValueSql("((1))");
            entity.Property(e => e.IsCovidPossitive).HasDefaultValueSql("((1))");
            entity.Property(e => e.IsIdCurrentOrExpired).HasMaxLength(50);
            entity.Property(e => e.IsMatchInsurance)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            entity.Property(e => e.IsVaccinated)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            entity.Property(e => e.LastAppoitmentDate).HasColumnType("datetime");
            entity.Property(e => e.NewAppoitmentDate).HasColumnType("datetime");
            entity.Property(e => e.PatientDob)
                .HasColumnType("datetime")
                .HasColumnName("PatientDOB");
            entity.Property(e => e.PatientEmail).HasMaxLength(50);
            entity.Property(e => e.PatientFirstName).HasMaxLength(20);
            entity.Property(e => e.PatientLastName).HasMaxLength(20);
            entity.Property(e => e.PatientMobileNo).HasMaxLength(20);
            entity.Property(e => e.Pcp)
                .HasMaxLength(50)
                .HasColumnName("PCP");
            entity.Property(e => e.PcpmobileNo)
                .HasMaxLength(20)
                .HasColumnName("PCPMobileNo");
            entity.Property(e => e.PrimaryInsuranceId).HasMaxLength(50);
            entity.Property(e => e.PrimaryInsuranceName).HasMaxLength(50);
            entity.Property(e => e.ReferingMd)
                .HasMaxLength(50)
                .HasColumnName("ReferingMD");
            entity.Property(e => e.ReferingMobileNo).HasMaxLength(20);
            entity.Property(e => e.SecondaryInsuranceId).HasMaxLength(50);
            entity.Property(e => e.SecondaryInsuranceName).HasMaxLength(50);
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.TaxId).HasMaxLength(20);
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<CallTypeMst>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CallType__3214EC077DBCB160");

            entity.ToTable("CallTypeMst");

            entity.Property(e => e.CallTypeName).HasMaxLength(50);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.IsActive)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<ClientMst>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ClientMs__3214EC07CDFDA031");

            entity.ToTable("ClientMst");

            entity.Property(e => e.AppoitmentEmail).HasMaxLength(50);
            entity.Property(e => e.City).HasMaxLength(50);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.DoctorEmail).HasMaxLength(50);
            entity.Property(e => e.FaxNo).HasMaxLength(20);
            entity.Property(e => e.FirstName).HasMaxLength(20);
            entity.Property(e => e.HomeName).HasMaxLength(100);
            entity.Property(e => e.InfoEmail).HasMaxLength(50);
            entity.Property(e => e.IsActive)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            entity.Property(e => e.LastName).HasMaxLength(20);
            entity.Property(e => e.MobileNo).HasMaxLength(20);
            entity.Property(e => e.OfficeName).HasMaxLength(20);
            entity.Property(e => e.PostalCode).HasMaxLength(10);
            entity.Property(e => e.StreetName).HasMaxLength(100);
            entity.Property(e => e.StreetNo).HasMaxLength(50);
            entity.Property(e => e.Suburb).HasMaxLength(50);
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<DurationMst>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Duration__3214EC0712B69E32");

            entity.ToTable("DurationMst");

            entity.Property(e => e.AppointmentId).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.EndDate).HasColumnType("datetime");
            entity.Property(e => e.IsActive)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            entity.Property(e => e.StartDate).HasColumnType("datetime");
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<ExtensionMst>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Extensio__3214EC0734DCD1E3");

            entity.ToTable("ExtensionMst");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.ExtensionName).HasMaxLength(50);
            entity.Property(e => e.IsActive)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<NotificationMst>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Notifica__3214EC07E7C9C8DF");

            entity.ToTable("NotificationMst");

            entity.Property(e => e.AdminDescriptionTitle).HasMaxLength(50);
            entity.Property(e => e.ApprovalStatus).HasMaxLength(50);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.DescriptionTitle).HasMaxLength(50);
            entity.Property(e => e.IsActive)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<PatientEmailMst>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PatientE__3214EC07218B8D2F");

            entity.ToTable("PatientEmailMst");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.EmailFor).HasMaxLength(250);
            entity.Property(e => e.IsActive)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            entity.Property(e => e.PatientEmail)
                .HasMaxLength(50)
                .HasColumnName("PatientEMail");
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<PhysicianMst>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Physicia__3214EC074AD96C67");

            entity.ToTable("PhysicianMst");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.DoctorDegreeName1).HasMaxLength(20);
            entity.Property(e => e.DoctorDegreeName2).HasMaxLength(20);
            entity.Property(e => e.DoctorDegreeName3).HasMaxLength(20);
            entity.Property(e => e.DoctorEmail).HasMaxLength(50);
            entity.Property(e => e.DoctorFirstName).HasMaxLength(20);
            entity.Property(e => e.DoctorLastName).HasMaxLength(20);
            entity.Property(e => e.DoctorMobileNo).HasMaxLength(20);
            entity.Property(e => e.IsActive)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            entity.Property(e => e.SecretaryFirstName)
                .HasMaxLength(20)
                .HasColumnName("secretaryFirstName");
            entity.Property(e => e.SecretaryLastName)
                .HasMaxLength(20)
                .HasColumnName("secretaryLastName");
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<RemarkMst>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__RemarkMs__3214EC0792C7D820");

            entity.ToTable("RemarkMst");

            entity.Property(e => e.AppointmentId).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.IsActive)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            entity.Property(e => e.Remark).HasMaxLength(20);
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<RoleMst>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__RoleMst__3214EC078C6C1BA9");

            entity.ToTable("RoleMst");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.IsActive)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            entity.Property(e => e.RoleName).HasMaxLength(50);
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<UserMst>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UserMst__3214EC07DE30B75F");

            entity.ToTable("UserMst");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Dob)
                .HasColumnType("datetime")
                .HasColumnName("DOB");
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.FirstName).HasMaxLength(20);
            entity.Property(e => e.IsActive)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            entity.Property(e => e.LastName).HasMaxLength(20);
            entity.Property(e => e.MobileNo).HasMaxLength(20);
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            entity.Property(e => e.UserName).HasMaxLength(50);
        });

        modelBuilder.Entity<UserTokenMst>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UserToke__3214EC07D600D90E");

            entity.ToTable("UserTokenMst");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.ExpiredOn).HasColumnType("datetime");
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
