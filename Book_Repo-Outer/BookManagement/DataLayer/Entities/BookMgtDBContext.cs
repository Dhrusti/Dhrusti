using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DataLayer.Entities
{
    public partial class BookMgtDBContext : DbContext
    {
        public BookMgtDBContext()
        {
        }

        public BookMgtDBContext(DbContextOptions<BookMgtDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TblAccessMst> TblAccessMsts { get; set; } = null!;
        public virtual DbSet<TblBookDownloadMst> TblBookDownloadMsts { get; set; } = null!;
        public virtual DbSet<TblBookMst> TblBookMsts { get; set; } = null!;
        public virtual DbSet<TblCategoryMst> TblCategoryMsts { get; set; } = null!;
        public virtual DbSet<TblPermissionMst> TblPermissionMsts { get; set; } = null!;
        public virtual DbSet<TblSubCategoryMst> TblSubCategoryMsts { get; set; } = null!;
        public virtual DbSet<TblUserAccessPermission> TblUserAccessPermissions { get; set; } = null!;
        public virtual DbSet<TblUserMst> TblUserMsts { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//                optionsBuilder.UseSqlServer("Data Source=ARCHE-ITD450\\SQLEXPRESS;Initial Catalog=BookMgtDB;User Id=sa;Password=123");
//            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TblAccessMst>(entity =>
            {
                entity.HasKey(e => e.AccessId)
                    .HasName("PK__TblAcces__4130D05F7FB15520");

                entity.ToTable("TblAccessMst");

                entity.Property(e => e.AccessName)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<TblBookDownloadMst>(entity =>
            {
                entity.HasKey(e => e.BookUserId)
                    .HasName("PK__TblBookD__BFEA4EFEC04075FB");

                entity.ToTable("TblBookDownloadMst");

                entity.Property(e => e.BookUserId).HasColumnName("BookUserID");

                entity.Property(e => e.ContactNumber).HasMaxLength(20);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.EmailId).HasMaxLength(255);

                entity.Property(e => e.FirstName).HasMaxLength(255);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.LastNane).HasMaxLength(255);

                entity.Property(e => e.Location).HasMaxLength(255);

                entity.Property(e => e.UpdateOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<TblBookMst>(entity =>
            {
                entity.HasKey(e => e.BookId)
                    .HasName("PK__TblBookM__3DE0C227C7965DCE");

                entity.ToTable("TblBookMst");

                entity.Property(e => e.BookId).HasColumnName("BookID");

                entity.Property(e => e.AuthorName).HasMaxLength(255);

                entity.Property(e => e.BookName).HasMaxLength(255);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Edition).HasMaxLength(255);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Pdfpath).HasColumnName("PDFPath");

                entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.PublishDate).HasColumnType("datetime");

                entity.Property(e => e.Publisher).HasMaxLength(255);

                entity.Property(e => e.UpdateOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<TblCategoryMst>(entity =>
            {
                entity.HasKey(e => e.CategoryId)
                    .HasName("PK__TblCateg__19093A0BDCFDFACA");

                entity.ToTable("TblCategoryMst");

                entity.Property(e => e.CategoryName).HasMaxLength(255);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.UpdateOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<TblPermissionMst>(entity =>
            {
                entity.HasKey(e => e.Pid)
                    .HasName("PK__TblPermi__C577554034ED4D67");

                entity.ToTable("TblPermissionMst");

                entity.Property(e => e.Pid).HasColumnName("PId");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.PermissionName)
                    .HasMaxLength(250)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblSubCategoryMst>(entity =>
            {
                entity.HasKey(e => e.SubCategoryId)
                    .HasName("PK__TblSubCa__26BE5B19BD7E7281");

                entity.ToTable("TblSubCategoryMst");

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.SubCategoryName).HasMaxLength(255);

                entity.Property(e => e.UpdateOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<TblUserAccessPermission>(entity =>
            {
                entity.ToTable("TblUserAccessPermission");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<TblUserMst>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK__TblUserM__1788CCAC07BB2382");

                entity.ToTable("TblUserMst");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.Address).HasMaxLength(255);

                entity.Property(e => e.ContactNumber).HasMaxLength(20);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Email).HasMaxLength(255);

                entity.Property(e => e.FullName).HasMaxLength(255);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Password).HasMaxLength(255);

                entity.Property(e => e.UpdateOn).HasColumnType("datetime");

                entity.Property(e => e.UserName).HasMaxLength(255);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
