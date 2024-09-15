using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ENTITIES.DBContent;

public partial class EShoppingContext : DbContext
{
    public EShoppingContext(DbContextOptions<EShoppingContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ApplicationUser> ApplicationUsers { get; set; }

    public virtual DbSet<BinhLuan> BinhLuans { get; set; }

    public virtual DbSet<NhomPhanLoai1> NhomPhanLoai1s { get; set; }

    public virtual DbSet<PhanQuyen> PhanQuyens { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<SanPham> SanPhams { get; set; }

    public virtual DbSet<TheLoai> TheLoais { get; set; }

    public virtual DbSet<ThuongHieu> ThuongHieus { get; set; }

    public virtual DbSet<TraLoiBinhLuan> TraLoiBinhLuans { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ApplicationUser>(entity =>
        {
            entity.ToTable("ApplicationUser");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.NgaySua).HasColumnType("datetime");
            entity.Property(e => e.NgayTao).HasColumnType("datetime");
            entity.Property(e => e.NgayXoa).HasColumnType("datetime");
            entity.Property(e => e.NguoiSua).HasMaxLength(256);
            entity.Property(e => e.NguoiTao).HasMaxLength(256);
            entity.Property(e => e.NguoiXoa).HasMaxLength(256);
            entity.Property(e => e.Password).HasMaxLength(500);
            entity.Property(e => e.PasswordSalt).HasMaxLength(100);
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Username)
                .HasMaxLength(256)
                .IsUnicode(false);
            entity.Property(e => e.Vertify).HasDefaultValue(false);

            entity.HasOne(d => d.Role).WithMany(p => p.ApplicationUsers)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ApplicationUser_Role");
        });

        modelBuilder.Entity<BinhLuan>(entity =>
        {
            entity.ToTable("BinhLuan");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.NgayTao).HasColumnType("datetime");
        });

        modelBuilder.Entity<NhomPhanLoai1>(entity =>
        {
            entity.ToTable("NhomPhanLoai1");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.NgaySua).HasColumnType("datetime");
            entity.Property(e => e.NgayTao).HasColumnType("datetime");
            entity.Property(e => e.NgayXoa).HasColumnType("datetime");
            entity.Property(e => e.NguoiSua).HasMaxLength(256);
            entity.Property(e => e.NguoiTao).HasMaxLength(256);
            entity.Property(e => e.NguoiXoa).HasMaxLength(256);
            entity.Property(e => e.TenGoi).HasMaxLength(50);
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
            entity.Property(e => e.NgaySua).HasColumnType("datetime");
            entity.Property(e => e.NgayTao).HasColumnType("datetime");
            entity.Property(e => e.NgayXoa).HasColumnType("datetime");
            entity.Property(e => e.NguoiSua).HasMaxLength(256);
            entity.Property(e => e.NguoiTao).HasMaxLength(256);
            entity.Property(e => e.NguoiXoa).HasMaxLength(256);
            entity.Property(e => e.TenGoi).HasMaxLength(256);
        });

        modelBuilder.Entity<SanPham>(entity =>
        {
            entity.ToTable("SanPham");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(256)
                .IsUnicode(false);
            entity.Property(e => e.NgaySua).HasColumnType("datetime");
            entity.Property(e => e.NgayTao).HasColumnType("datetime");
            entity.Property(e => e.NgayXoa).HasColumnType("datetime");
            entity.Property(e => e.NguoiSua).HasMaxLength(256);
            entity.Property(e => e.NguoiTao).HasMaxLength(256);
            entity.Property(e => e.NguoiXoa).HasMaxLength(256);
            entity.Property(e => e.Price).HasColumnType("money");

            entity.HasOne(d => d.TheLoai).WithMany(p => p.SanPhams)
                .HasForeignKey(d => d.TheLoaiId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SanPham_TheLoai");

            entity.HasOne(d => d.ThuongHieu).WithMany(p => p.SanPhams)
                .HasForeignKey(d => d.ThuongHieuId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SanPham_ThuongHieu");
        });

        modelBuilder.Entity<TheLoai>(entity =>
        {
            entity.ToTable("TheLoai");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(256)
                .IsUnicode(false);
            entity.Property(e => e.Name).HasMaxLength(256);
            entity.Property(e => e.NgaySua).HasColumnType("datetime");
            entity.Property(e => e.NgayTao).HasColumnType("datetime");
            entity.Property(e => e.NgayXoa).HasColumnType("datetime");
            entity.Property(e => e.NguoiSua).HasMaxLength(256);
            entity.Property(e => e.NguoiTao).HasMaxLength(256);
            entity.Property(e => e.NguoiXoa).HasMaxLength(256);
        });

        modelBuilder.Entity<ThuongHieu>(entity =>
        {
            entity.ToTable("ThuongHieu");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(256)
                .IsUnicode(false);
            entity.Property(e => e.Name).HasMaxLength(256);
            entity.Property(e => e.NgaySua).HasColumnType("datetime");
            entity.Property(e => e.NgayTao).HasColumnType("datetime");
            entity.Property(e => e.NgayXoa).HasColumnType("datetime");
            entity.Property(e => e.NguoiSua).HasMaxLength(256);
            entity.Property(e => e.NguoiTao).HasMaxLength(256);
            entity.Property(e => e.NguoiXoa).HasMaxLength(256);
        });

        modelBuilder.Entity<TraLoiBinhLuan>(entity =>
        {
            entity.ToTable("TraLoiBinhLuan");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.NgayTao).HasColumnType("datetime");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
