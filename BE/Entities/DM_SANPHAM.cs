using System;
using System.Collections.Generic;

namespace BE.Entities;

public partial class DM_SANPHAM
{
    public Guid Id { get; set; }

    public Guid ThuongHieuId { get; set; }

    public Guid TheLoaiId { get; set; }

    public string Name { get; set; } = null!;

    public double Price { get; set; }

    public string? Decription { get; set; }

    public string? ImageUrl { get; set; }

    public virtual DM_THELOAI TheLoai { get; set; } = null!;

    public virtual DM_THUONGHIEU ThuongHieu { get; set; } = null!;
}
