using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Entities;

public partial class ErpDbContext : DbContext
{
    public ErpDbContext()
    {
    }

    public ErpDbContext(DbContextOptions<ErpDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CompanyMst> CompanyMsts { get; set; }

    public virtual DbSet<DesignationMst> DesignationMsts { get; set; }

    public virtual DbSet<EmployementTypeMst> EmployementTypeMsts { get; set; }

    public virtual DbSet<GenderMst> GenderMsts { get; set; }

    public virtual DbSet<RegistrationMst> RegistrationMsts { get; set; }

    public virtual DbSet<ReportingManagerMst> ReportingManagerMsts { get; set; }

    public virtual DbSet<RequirementMst> RequirementMsts { get; set; }

    public virtual DbSet<RoleMst> RoleMsts { get; set; }

    public virtual DbSet<TokenMst> TokenMsts { get; set; }

    public virtual DbSet<UserMst> UserMsts { get; set; }


	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		if (!optionsBuilder.IsConfigured)
		{
/*#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
			optionsBuilder.UseSqlServer("Server=192.168.1.199,1433;user=sa;password=sa@2022;Database=WaltCapitalDB;Trusted_Connection=False;");*/
		}
	}

	/*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    #warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
            => optionsBuilder.UseSqlServer("Server=ARCHE-ITD450\\SQLEXPRESS;Database=ERP_DB;TrustServerCertificate=True;MultipleActiveResultSets=True;User Id=sa;Password=123");*/

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CompanyMst>(entity =>
        {
            entity.HasKey(e => e.CompanyId).HasName("PK__CompanyM__2D971CACF66E9C7B");

            entity.ToTable("CompanyMst");

            entity.Property(e => e.CompanyName).HasMaxLength(250);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<DesignationMst>(entity =>
        {
            entity.HasKey(e => e.DesignationId).HasName("PK__Designat__BABD60DEA99131CA");

            entity.ToTable("DesignationMst");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Designation).HasMaxLength(250);
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<EmployementTypeMst>(entity =>
        {
            entity.HasKey(e => e.EmployementTypeId).HasName("PK__Employem__367C7E1000688AB1");

            entity.ToTable("EmployementTypeMst");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.EmployementType).HasMaxLength(250);
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<GenderMst>(entity =>
        {
            entity.HasKey(e => e.GenderId).HasName("PK__GenderMs__4E24E9F71168A38B");

            entity.ToTable("GenderMst");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Gender).HasMaxLength(250);
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<RegistrationMst>(entity =>
        {
            entity.HasKey(e => e.RegistrationId).HasName("PK__Registra__6EF58810B6BB0FC5");

            entity.ToTable("RegistrationMst");

            entity.Property(e => e.Address).HasMaxLength(250);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(250);
            entity.Property(e => e.FirstName).HasMaxLength(250);
            entity.Property(e => e.LastName).HasMaxLength(250);
            entity.Property(e => e.MobileNo).HasMaxLength(250);
            entity.Property(e => e.Password).HasMaxLength(250);
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<ReportingManagerMst>(entity =>
        {
            entity.HasKey(e => e.ReportingManagerId).HasName("PK__Reportin__0C57AC94006E650A");

            entity.ToTable("ReportingManagerMst");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.ReportingManagerName).HasMaxLength(250);
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<RequirementMst>(entity =>
        {
            entity.HasKey(e => e.RequirementId).HasName("PK__Requirem__7DF11E5D80F2C42B");

            entity.ToTable("RequirementMst");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.MainSkills).HasMaxLength(250);
            entity.Property(e => e.MandatorySkill).HasMaxLength(250);
            entity.Property(e => e.Pocname)
                .HasMaxLength(250)
                .HasColumnName("POCName");
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<RoleMst>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__RoleMst__8AFACE1AE7852756");

            entity.ToTable("RoleMst");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Role).HasMaxLength(250);
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<TokenMst>(entity =>
        {
            entity.HasKey(e => e.TokenId).HasName("PK__TokenMst__658FEEEA1148266B");

            entity.ToTable("TokenMst");

            entity.Property(e => e.TokenCreated).HasColumnType("datetime");
            entity.Property(e => e.TokenExpires).HasColumnType("datetime");
        });

        modelBuilder.Entity<UserMst>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UserMst__3214EC07E54676AD");

            entity.ToTable("UserMst");

            entity.Property(e => e.AccountNumber).HasMaxLength(250);
            entity.Property(e => e.AdharCardNumber).HasMaxLength(250);
            entity.Property(e => e.BankName).HasMaxLength(250);
            entity.Property(e => e.BloodGroup).HasMaxLength(250);
            entity.Property(e => e.Branch).HasMaxLength(250);
            entity.Property(e => e.ConfirmPassword).HasMaxLength(250);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.CurrentAddress).HasMaxLength(250);
            entity.Property(e => e.Department).HasMaxLength(250);
            entity.Property(e => e.Designation).HasMaxLength(250);
            entity.Property(e => e.Dob)
                .HasColumnType("datetime")
                .HasColumnName("DOB");
            entity.Property(e => e.Email).HasMaxLength(250);
            entity.Property(e => e.EmergencyContact).HasMaxLength(250);
            entity.Property(e => e.EmployeeCode).HasMaxLength(250);
            entity.Property(e => e.EmployeePersonalEmailId).HasMaxLength(250);
            entity.Property(e => e.FullName).HasMaxLength(250);
            entity.Property(e => e.Ifsccode)
                .HasMaxLength(250)
                .HasColumnName("IFSCCode");
            entity.Property(e => e.JoinDate).HasColumnType("datetime");
            entity.Property(e => e.Location).HasMaxLength(250);
            entity.Property(e => e.OfferDate).HasColumnType("datetime");
            entity.Property(e => e.PancardNumber)
                .HasMaxLength(250)
                .HasColumnName("PANCardNumber");
            entity.Property(e => e.Password).HasMaxLength(250);
            entity.Property(e => e.PermanentAddress).HasMaxLength(250);
            entity.Property(e => e.PfaccountNumber)
                .HasMaxLength(250)
                .HasColumnName("PFAccountNumber");
            entity.Property(e => e.PhoneNumber).HasMaxLength(250);
            entity.Property(e => e.PostCode).HasMaxLength(250);
            entity.Property(e => e.ProbationPeriod).HasMaxLength(250);
            entity.Property(e => e.Salary).HasMaxLength(250);
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            entity.Property(e => e.UserName).HasMaxLength(250);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
