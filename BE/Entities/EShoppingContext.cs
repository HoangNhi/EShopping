using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BE.Entities;

public partial class EShoppingContext : DbContext
{
    public EShoppingContext(DbContextOptions<EShoppingContext> options)
        : base(options)
    {
    }

    public virtual DbSet<DM_SANPHAM> DM_SANPHAMs { get; set; }

    public virtual DbSet<DM_THELOAI> DM_THELOAIs { get; set; }

    public virtual DbSet<DM_THUONGHIEU> DM_THUONGHIEUs { get; set; }

    public virtual DbSet<HT_TAIKHOAN> HT_TAIKHOANs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DM_SANPHAM>(entity =>
        {
            entity.ToTable("DM_SANPHAM");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.ImageUrl).HasMaxLength(500);

            entity.HasOne(d => d.TheLoai).WithMany(p => p.DM_SANPHAMs)
                .HasForeignKey(d => d.TheLoaiId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DM_SANPHAM_DM_THELOAI");

            entity.HasOne(d => d.ThuongHieu).WithMany(p => p.DM_SANPHAMs)
                .HasForeignKey(d => d.ThuongHieuId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DM_SANPHAM_DM_THUONGHIEU");
        });

        modelBuilder.Entity<DM_THELOAI>(entity =>
        {
            entity.ToTable("DM_THELOAI");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.ImageUrl).HasMaxLength(500);
            entity.Property(e => e.NgaySua).HasColumnType("datetime");
            entity.Property(e => e.NgayTao).HasColumnType("datetime");
            entity.Property(e => e.NgayXoa).HasColumnType("datetime");
            entity.Property(e => e.NguoiSua)
                .HasMaxLength(256)
                .IsUnicode(false);
            entity.Property(e => e.NguoiTao)
                .HasMaxLength(256)
                .IsUnicode(false);
            entity.Property(e => e.NguoiXoa)
                .HasMaxLength(256)
                .IsUnicode(false);
        });

        modelBuilder.Entity<DM_THUONGHIEU>(entity =>
        {
            entity.ToTable("DM_THUONGHIEU");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.ImageUrl).HasMaxLength(500);
            entity.Property(e => e.NgaySua).HasColumnType("datetime");
            entity.Property(e => e.NgayTao).HasColumnType("datetime");
            entity.Property(e => e.NgayXoa).HasColumnType("datetime");
            entity.Property(e => e.NguoiSua)
                .HasMaxLength(256)
                .IsUnicode(false);
            entity.Property(e => e.NguoiTao)
                .HasMaxLength(256)
                .IsUnicode(false);
            entity.Property(e => e.NguoiXoa)
                .HasMaxLength(256)
                .IsUnicode(false);
        });

        modelBuilder.Entity<HT_TAIKHOAN>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("HT_TAIKHOAN");

            entity.Property(e => e.AnhDaiDien).HasMaxLength(500);
            entity.Property(e => e.Email)
                .HasMaxLength(256)
                .IsUnicode(false);
            entity.Property(e => e.HoLot).HasMaxLength(256);
            entity.Property(e => e.NgaySua).HasColumnType("datetime");
            entity.Property(e => e.NgayTao).HasColumnType("datetime");
            entity.Property(e => e.NgayXoa).HasColumnType("datetime");
            entity.Property(e => e.NguoiSua)
                .HasMaxLength(256)
                .IsUnicode(false);
            entity.Property(e => e.NguoiTao)
                .HasMaxLength(256)
                .IsUnicode(false);
            entity.Property(e => e.NguoiXoa)
                .HasMaxLength(256)
                .IsUnicode(false);
            entity.Property(e => e.SoDienThoai)
                .HasMaxLength(256)
                .IsUnicode(false);
            entity.Property(e => e.Ten).HasMaxLength(256);
            entity.Property(e => e.Username)
                .HasMaxLength(256)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
