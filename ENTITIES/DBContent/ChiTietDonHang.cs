using System;
using System.Collections.Generic;

namespace ENTITIES.DbContent;

public partial class ChiTietDonHang
{
    public Guid Id { get; set; }

    public Guid HoaDonId { get; set; }

    public Guid SanPhamId { get; set; }

    public Guid NhomPhanLoai1Id { get; set; }

    public Guid NhomPhanLoai2Id { get; set; }

    public int Quantity { get; set; }

    public int? TotalPrice { get; set; }

    public bool? Status { get; set; }

    public DateTime DateCreated { get; set; }

    public virtual HoaDon HoaDon { get; set; } = null!;

    public virtual NhomPhanLoai1 NhomPhanLoai1 { get; set; } = null!;

    public virtual NhomPhanLoai2 NhomPhanLoai2 { get; set; } = null!;

    public virtual SanPham SanPham { get; set; } = null!;
}
