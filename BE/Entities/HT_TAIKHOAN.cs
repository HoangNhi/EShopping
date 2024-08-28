using System;
using System.Collections.Generic;

namespace BE.Entities;

public partial class HT_TAIKHOAN
{
    public Guid Id { get; set; }

    public string Username { get; set; } = null!;

    public string? HoLot { get; set; }

    public string Ten { get; set; } = null!;

    public string? Email { get; set; }

    public string? SoDienThoai { get; set; }

    public DateOnly? NgaySinh { get; set; }

    public int? GioiTinh { get; set; }

    public string? AnhDaiDien { get; set; }

    public string MatKhau { get; set; } = null!;

    public DateTime NgayTao { get; set; }

    public string NguoiTao { get; set; } = null!;

    public DateTime NgaySua { get; set; }

    public string NguoiSua { get; set; } = null!;

    public DateTime? NgayXoa { get; set; }

    public string? NguoiXoa { get; set; }

    public bool IsActived { get; set; }

    public bool IsDeleted { get; set; }
}
