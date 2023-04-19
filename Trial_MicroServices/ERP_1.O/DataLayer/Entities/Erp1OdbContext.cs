using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Entities;

public partial class Erp1OdbContext : DbContext
{
    public Erp1OdbContext()
    {
    }

    public Erp1OdbContext(DbContextOptions<Erp1OdbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<UserMst> UserMsts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

    }
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=ERP1.ODb;TrustServerCertificate=True;MultipleActiveResultSets=True;User Id=sa;Password=123");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserMst>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UserMst__3214EC071D44C803");

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
