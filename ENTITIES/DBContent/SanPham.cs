using System;
using System.Collections.Generic;

namespace ENTITIES.DBContent;

public partial class SanPham
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public decimal Price { get; set; }

    public string Decription { get; set; } = null!;

    public string ImageUrl { get; set; } = null!;

    public int TheLoaiId { get; set; }

    public int ThuongHieuId { get; set; }

    public DateTime NgayTao { get; set; }

    public string NguoiTao { get; set; } = null!;

    public DateTime NgaySua { get; set; }

    public string NguoiSua { get; set; } = null!;

    public DateTime? NgayXoa { get; set; }

    public string? NguoiXoa { get; set; }

    public bool IsActived { get; set; }

    public bool IsDeleted { get; set; }

    public virtual TheLoai TheLoai { get; set; } = null!;

    public virtual ThuongHieu ThuongHieu { get; set; } = null!;
}
