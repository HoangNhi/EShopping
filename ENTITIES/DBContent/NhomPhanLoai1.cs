using System;
using System.Collections.Generic;

namespace ENTITIES.DBContent;

public partial class NhomPhanLoai1
{
    public int Id { get; set; }

    public int SanPhamId { get; set; }

    public string TenGoi { get; set; } = null!;

    public DateTime NgayTao { get; set; }

    public string NguoiTao { get; set; } = null!;

    public DateTime NgaySua { get; set; }

    public string NguoiSua { get; set; } = null!;

    public DateTime? NgayXoa { get; set; }

    public string? NguoiXoa { get; set; }

    public bool IsActived { get; set; }

    public bool IsDeleted { get; set; }
}
