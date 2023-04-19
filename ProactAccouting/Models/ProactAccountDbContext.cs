using Microsoft.EntityFrameworkCore;

namespace ProactAccouting.Models;

public partial class ProactAccountDbContext : DbContext
{
    public ProactAccountDbContext()
    {
    }

    public ProactAccountDbContext(DbContextOptions<ProactAccountDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<LevelFifthMst> LevelFifthMsts { get; set; }

    public virtual DbSet<LevelFirstMst> LevelFirstMsts { get; set; }

    public virtual DbSet<LevelFourthMst> LevelFourthMsts { get; set; }

    public virtual DbSet<LevelSecondMst> LevelSecondMsts { get; set; }

    public virtual DbSet<LevelThirdMst> LevelThirdMsts { get; set; }

    public virtual DbSet<TblCodeMst> TblCodeMsts { get; set; }

    //    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
    //        => optionsBuilder.UseSqlServer("Server=ARCHE-ITL406\\SQLEXPRESS;Database=ProactAccount_db;User Id=sa;Password=123;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<LevelFifthMst>(entity =>
        {
            entity.HasKey(e => e.LevelFifthId).HasName("PK__LevelFif__D2115A1CFC9AB71B");

            entity.ToTable("LevelFifthMst");

            entity.Property(e => e.Code).HasMaxLength(20);
            entity.Property(e => e.CodeName).HasMaxLength(100);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.CreditDebit).HasMaxLength(10);
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

            entity.HasOne(d => d.LevelFourth).WithMany(p => p.LevelFifthMsts)
                .HasForeignKey(d => d.LevelFourthId)
                .HasConstraintName("FK__LevelFift__Level__32E0915F");
        });

        modelBuilder.Entity<LevelFirstMst>(entity =>
        {
            entity.HasKey(e => e.LevelFirstId).HasName("PK__LevelFir__95DA05DC2554AAB2");

            entity.ToTable("LevelFirstMst");

            entity.Property(e => e.Code).HasMaxLength(20);
            entity.Property(e => e.CodeName).HasMaxLength(100);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<LevelFourthMst>(entity =>
        {
            entity.HasKey(e => e.LevelFourthId).HasName("PK__LevelFou__E20AB5F820F8D925");

            entity.ToTable("LevelFourthMst");

            entity.Property(e => e.Code).HasMaxLength(20);
            entity.Property(e => e.CodeName).HasMaxLength(100);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.CreditDebit).HasMaxLength(10);
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

            entity.HasOne(d => d.LevelThird).WithMany(p => p.LevelFourthMsts)
                .HasForeignKey(d => d.LevelThirdId)
                .HasConstraintName("FK__LevelFour__Level__300424B4");
        });

        modelBuilder.Entity<LevelSecondMst>(entity =>
        {
            entity.HasKey(e => e.LevelSecondId).HasName("PK__LevelSec__53A6812A40AF06C2");

            entity.ToTable("LevelSecondMst");

            entity.Property(e => e.Code).HasMaxLength(20);
            entity.Property(e => e.CodeName).HasMaxLength(100);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

            entity.HasOne(d => d.LevelFirst).WithMany(p => p.LevelSecondMsts)
                .HasForeignKey(d => d.LevelFirstId)
                .HasConstraintName("FK__LevelSeco__Level__2A4B4B5E");
        });

        modelBuilder.Entity<LevelThirdMst>(entity =>
        {
            entity.HasKey(e => e.LevelThirdId).HasName("PK__LevelThi__E788A4EECACDCEF8");

            entity.ToTable("LevelThirdMst");

            entity.Property(e => e.Code).HasMaxLength(20);
            entity.Property(e => e.CodeName).HasMaxLength(100);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.CreditDebit).HasMaxLength(10);
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

            entity.HasOne(d => d.LevelSecond).WithMany(p => p.LevelThirdMsts)
                .HasForeignKey(d => d.LevelSecondId)
                .HasConstraintName("FK__LevelThir__Level__2D27B809");
        });

        modelBuilder.Entity<TblCodeMst>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TblCodeM__3214EC073338A5C3");

            entity.ToTable("TblCodeMst");

            entity.Property(e => e.Code).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.CodeName).HasMaxLength(255);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
