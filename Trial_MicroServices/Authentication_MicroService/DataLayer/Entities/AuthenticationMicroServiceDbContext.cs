using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Entities;

public partial class AuthenticationMicroServiceDbContext : DbContext
{
    public AuthenticationMicroServiceDbContext()
    {
    }

    public AuthenticationMicroServiceDbContext(DbContextOptions<AuthenticationMicroServiceDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<RegistrationMst> RegistrationMsts { get; set; }

    public virtual DbSet<TokenMst> TokenMsts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

    }
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Server=ARCHE-ITD450\\SQLEXPRESS;Database=AuthenticationMicroService_DB;TrustServerCertificate=True;MultipleActiveResultSets=True;User Id=sa;Password=123");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<RegistrationMst>(entity =>
        {
            entity.HasKey(e => e.RegistrationId).HasName("PK__Registra__6EF58810DE599441");

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

        modelBuilder.Entity<TokenMst>(entity =>
        {
            entity.HasKey(e => e.TokenId).HasName("PK__TokenMst__658FEEEABEA2F8C7");

            entity.ToTable("TokenMst");

            entity.Property(e => e.TokenCreated).HasColumnType("datetime");
            entity.Property(e => e.TokenExpires).HasColumnType("datetime");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
