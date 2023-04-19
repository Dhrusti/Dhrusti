using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Entities;

public partial class RevenueCycleDbContext : DbContext
{
    public RevenueCycleDbContext()
    {
    }

    public RevenueCycleDbContext(DbContextOptions<RevenueCycleDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AppointmentMst> AppointmentMsts { get; set; }

    public virtual DbSet<CallTypeMst> CallTypeMsts { get; set; }

    public virtual DbSet<NotificationMst> NotificationMsts { get; set; }

    public virtual DbSet<PatientEmailMst> PatientEmailMsts { get; set; }

    public virtual DbSet<PhysicianMst> PhysicianMsts { get; set; }

    public virtual DbSet<RemarkMst> RemarkMsts { get; set; }

    public virtual DbSet<RoleMst> RoleMsts { get; set; }

    public virtual DbSet<UserMst> UserMsts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

    }
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Server=ARCHE-ITD450\\SQLEXPRESS;user=sa;password=123;Database=RevenueCycleDB;Trusted_Connection=False;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AppointmentMst>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Appointm__3214EC079A57C482");

            entity.ToTable("AppointmentMst");

            entity.Property(e => e.AccountNo).HasMaxLength(50);
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
            entity.Property(e => e.PatientFirstName).HasMaxLength(50);
            entity.Property(e => e.PatientLastName).HasMaxLength(50);
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
            entity.HasKey(e => e.Id).HasName("PK__CallType__3214EC07615ADA6C");

            entity.ToTable("CallTypeMst");

            entity.Property(e => e.CallTypeName).HasMaxLength(50);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.IsActive)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<NotificationMst>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Notifica__3214EC075369842D");

            entity.ToTable("NotificationMst");

            entity.Property(e => e.AdminDescription).HasMaxLength(50);
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
            entity.HasKey(e => e.Id).HasName("PK__PatientE__3214EC072A75C7C0");

            entity.ToTable("PatientEmailMst");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.EmailFor).HasMaxLength(250);
            entity.Property(e => e.IsActive)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<PhysicianMst>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Physicia__3214EC07E22D56AA");

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
            entity.Property(e => e.SecretaryFirstName).HasMaxLength(20);
            entity.Property(e => e.SecretaryLastName).HasMaxLength(20);
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<RemarkMst>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__RemarkMs__3214EC0704BB037B");

            entity.ToTable("RemarkMst");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Datetime).HasColumnType("datetime");
            entity.Property(e => e.IsActive)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            entity.Property(e => e.Remark).HasMaxLength(20);
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<RoleMst>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__RoleMst__3214EC072E15F145");

            entity.ToTable("RoleMst");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.RoleName).HasMaxLength(50);
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<UserMst>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UserMst__3214EC0781AE609C");

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

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
