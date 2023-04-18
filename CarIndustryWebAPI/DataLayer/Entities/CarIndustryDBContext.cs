using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DataLayer.Entities
{
    public partial class CarIndustryDBContext : DbContext
    {
        public CarIndustryDBContext()
        {
        }

        public CarIndustryDBContext(DbContextOptions<CarIndustryDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<BrandMst> BrandMsts { get; set; } = null!;
        public virtual DbSet<CarMst> CarMsts { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        //            if (!optionsBuilder.IsConfigured)
        //            {
        //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        //                optionsBuilder.UseSqlServer("Server=ARCHE-ITD450\\SQLEXPRESS;user=sa;password=123;Database=CarIndustryDB;Trusted_Connection=False;");
        //            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BrandMst>(entity =>
            {
                entity.ToTable("BrandMst");

                entity.Property(e => e.Brand).HasMaxLength(250);
            });

            modelBuilder.Entity<CarMst>(entity =>
            {
                entity.ToTable("CarMst");

                entity.Property(e => e.BuyTime).HasColumnType("datetime");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Model).HasMaxLength(250);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
