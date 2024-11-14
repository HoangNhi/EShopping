using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ENTITIES.DbContent;

public partial class EShoppingContext : DbContext
{
    public EShoppingContext(DbContextOptions<EShoppingContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ApplicationUser> ApplicationUsers { get; set; }

    public virtual DbSet<BinhLuan> BinhLuans { get; set; }

    public virtual DbSet<CauHinhSanPham> CauHinhSanPhams { get; set; }

    public virtual DbSet<ChiTietDonHang> ChiTietDonHangs { get; set; }

    public virtual DbSet<DiaChi> DiaChis { get; set; }

    public virtual DbSet<GioHang> GioHangs { get; set; }

    public virtual DbSet<HoaDon> HoaDons { get; set; }

    public virtual DbSet<NhanHieu> NhanHieus { get; set; }

    public virtual DbSet<NhatKi> NhatKis { get; set; }

    public virtual DbSet<NhomPhanLoai1> NhomPhanLoai1s { get; set; }

    public virtual DbSet<NhomPhanLoai2> NhomPhanLoai2s { get; set; }

    public virtual DbSet<PHANQUYEN_NHOMQUYEN> PHANQUYEN_NHOMQUYENs { get; set; }

    public virtual DbSet<PhanQuyen> PhanQuyens { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<SanPham> SanPhams { get; set; }

    public virtual DbSet<SanPham_Anh> SanPham_Anhs { get; set; }

    public virtual DbSet<TheLoai> TheLoais { get; set; }

    public virtual DbSet<TraLoiBinhLuan> TraLoiBinhLuans { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ApplicationUser>(entity =>
        {
            entity.ToTable("ApplicationUser");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.DateCreate).HasColumnType("datetime");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FullName).HasMaxLength(256);
            entity.Property(e => e.IsGoogle).HasDefaultValue(false);
            entity.Property(e => e.Password).HasMaxLength(500);
            entity.Property(e => e.PasswordSalt).HasMaxLength(100);
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Username)
                .HasMaxLength(256)
                .IsUnicode(false);

            entity.HasOne(d => d.Role).WithMany(p => p.ApplicationUsers)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ApplicationUser_Role");
        });

        modelBuilder.Entity<BinhLuan>(entity =>
        {
            entity.ToTable("BinhLuan");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.DateCreate).HasColumnType("datetime");

            entity.HasOne(d => d.SanPham).WithMany(p => p.BinhLuans)
                .HasForeignKey(d => d.SanPhamId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BinhLuan_SanPham");
        });

        modelBuilder.Entity<CauHinhSanPham>(entity =>
        {
            entity.ToTable("CauHinhSanPham");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Detail)
                .HasMaxLength(80)
                .IsUnicode(false);
            entity.Property(e => e.Name).HasMaxLength(80);

            entity.HasOne(d => d.SanPham).WithMany(p => p.CauHinhSanPhams)
                .HasForeignKey(d => d.SanPhamId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CauHinhSanPham_SanPham");
        });

        modelBuilder.Entity<ChiTietDonHang>(entity =>
        {
            entity.ToTable("ChiTietDonHang");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.DateCreated).HasColumnType("datetime");

            entity.HasOne(d => d.HoaDon).WithMany(p => p.ChiTietDonHangs)
                .HasForeignKey(d => d.HoaDonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ChiTietDonHang_HoaDon");

            entity.HasOne(d => d.NhomPhanLoai1).WithMany(p => p.ChiTietDonHangs)
                .HasForeignKey(d => d.NhomPhanLoai1Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ChiTietDonHang_NhomPhanLoai1");

            entity.HasOne(d => d.NhomPhanLoai2).WithMany(p => p.ChiTietDonHangs)
                .HasForeignKey(d => d.NhomPhanLoai2Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ChiTietDonHang_NhomPhanLoai2");

            entity.HasOne(d => d.SanPham).WithMany(p => p.ChiTietDonHangs)
                .HasForeignKey(d => d.SanPhamId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ChiTietDonHang_SanPham");
        });

        modelBuilder.Entity<DiaChi>(entity =>
        {
            entity.ToTable("DiaChi");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Address).HasMaxLength(250);
            entity.Property(e => e.Name).HasMaxLength(150);
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.User).WithMany(p => p.DiaChis)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DiaChi_ApplicationUser");
        });

        modelBuilder.Entity<GioHang>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_GioHang_1");

            entity.ToTable("GioHang");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.DateCreated).HasColumnType("datetime");
        });

        modelBuilder.Entity<HoaDon>(entity =>
        {
            entity.ToTable("HoaDon");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.DateCreate).HasColumnType("datetime");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.Status)
                .HasDefaultValue(1)
                .HasComment("1: Chờ thanh toán, 2: Chờ giao hàng, 3: Đã giao, 4: Đã hủy");

            entity.HasOne(d => d.DiaChi).WithMany(p => p.HoaDons)
                .HasForeignKey(d => d.DiaChiId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HoaDon_DiaChi");

            entity.HasOne(d => d.User).WithMany(p => p.HoaDons)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HoaDon_ApplicationUser");
        });

        modelBuilder.Entity<NhanHieu>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_ThuongHieu");

            entity.ToTable("NhanHieu");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.DateCreate).HasColumnType("datetime");
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(256)
                .IsUnicode(false);
            entity.Property(e => e.Name).HasMaxLength(256);
        });

        modelBuilder.Entity<NhatKi>(entity =>
        {
            entity.ToTable("NhatKi");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.Event).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(100);

            entity.HasOne(d => d.User).WithMany(p => p.NhatKis)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_NhatKi_ApplicationUser");
        });

        modelBuilder.Entity<NhomPhanLoai1>(entity =>
        {
            entity.ToTable("NhomPhanLoai1");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.ImageURL)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Name).HasMaxLength(50);

            entity.HasOne(d => d.SanPham).WithMany(p => p.NhomPhanLoai1s)
                .HasForeignKey(d => d.SanPhamId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_NhomPhanLoai1_SanPham");
        });

        modelBuilder.Entity<NhomPhanLoai2>(entity =>
        {
            entity.ToTable("NhomPhanLoai2");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name).HasMaxLength(50);

            entity.HasOne(d => d.NhomPhanLoai1).WithMany(p => p.NhomPhanLoai2s)
                .HasForeignKey(d => d.NhomPhanLoai1Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_NhomPhanLoai2_NhomPhanLoai1");
        });

        modelBuilder.Entity<PHANQUYEN_NHOMQUYEN>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("PHANQUYEN_NHOMQUYEN");

            entity.Property(e => e.CreateBy).HasMaxLength(256);
            entity.Property(e => e.DateCreate).HasColumnType("datetime");
            entity.Property(e => e.Icon)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Status)
                .HasDefaultValue(1)
                .HasComment("1: Đang hoạt động, 0: Không hoạt động, -1: Đã xóa");
            entity.Property(e => e.TenGoi).HasMaxLength(256);
        });

        modelBuilder.Entity<PhanQuyen>(entity =>
        {
            entity.ToTable("PhanQuyen");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.ControllerName)
                .HasMaxLength(256)
                .IsUnicode(false);

            entity.HasOne(d => d.Role).WithMany(p => p.PhanQuyens)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PhanQuyen_Role");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.ToTable("Role");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreateBy).HasMaxLength(256);
            entity.Property(e => e.DateCreate).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(256);
        });

        modelBuilder.Entity<SanPham>(entity =>
        {
            entity.ToTable("SanPham");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.DateCreate).HasColumnType("datetime");
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(256)
                .IsUnicode(false);
            entity.Property(e => e.IsBestSelling).HasDefaultValue(false);
            entity.Property(e => e.IsNew).HasDefaultValue(false);
            entity.Property(e => e.IsSale).HasDefaultValue(false);
            entity.Property(e => e.Price).HasColumnType("money");
            entity.Property(e => e.TheLoaiId)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.NhanHieu).WithMany(p => p.SanPhams)
                .HasForeignKey(d => d.NhanHieuId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SanPham_ThuongHieu");

            entity.HasOne(d => d.TheLoai).WithMany(p => p.SanPhams)
                .HasForeignKey(d => d.TheLoaiId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SanPham_TheLoai");
        });

        modelBuilder.Entity<SanPham_Anh>(entity =>
        {
            entity.ToTable("SanPham_Anh");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.DateCreate).HasColumnType("datetime");
            entity.Property(e => e.ImageURL)
                .HasMaxLength(500)
                .IsUnicode(false);

            entity.HasOne(d => d.SanPham).WithMany(p => p.SanPham_Anhs)
                .HasForeignKey(d => d.SanPhamId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SanPham_Anh_SanPham");
        });

        modelBuilder.Entity<TheLoai>(entity =>
        {
            entity.ToTable("TheLoai");

            entity.Property(e => e.Id)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.DateCreate).HasColumnType("datetime");
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(256)
                .IsUnicode(false);
            entity.Property(e => e.Name).HasMaxLength(256);
        });

        modelBuilder.Entity<TraLoiBinhLuan>(entity =>
        {
            entity.ToTable("TraLoiBinhLuan");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.DateCreate).HasColumnType("datetime");

            entity.HasOne(d => d.BinhLuan).WithMany(p => p.TraLoiBinhLuans)
                .HasForeignKey(d => d.BinhLuanId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TraLoiBinhLuan_BinhLuan");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
