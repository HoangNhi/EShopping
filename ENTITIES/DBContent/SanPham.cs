using System;
using System.Collections.Generic;

namespace ENTITIES.DbContent;

public partial class SanPham
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public decimal Price { get; set; }

    public string Description { get; set; } = null!;

    public string ImageUrl { get; set; } = null!;

    public string TheLoaiId { get; set; } = null!;

    public Guid NhanHieuId { get; set; }

    public DateTime DateCreate { get; set; }

    public int? Quantity { get; set; }

    public int? Sold { get; set; }

    public bool Status { get; set; }

    public virtual ICollection<BinhLuan> BinhLuans { get; set; } = new List<BinhLuan>();

    public virtual ICollection<CauHinhSanPham> CauHinhSanPhams { get; set; } = new List<CauHinhSanPham>();

    public virtual ICollection<ChiTietDonHang> ChiTietDonHangs { get; set; } = new List<ChiTietDonHang>();

    public virtual NhanHieu NhanHieu { get; set; } = null!;

    public virtual ICollection<NhomPhanLoai1> NhomPhanLoai1s { get; set; } = new List<NhomPhanLoai1>();

    public virtual ICollection<SanPham_Anh> SanPham_Anhs { get; set; } = new List<SanPham_Anh>();

    public virtual TheLoai TheLoai { get; set; } = null!;
}
