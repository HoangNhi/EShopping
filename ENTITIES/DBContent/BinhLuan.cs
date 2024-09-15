using System;
using System.Collections.Generic;

namespace ENTITIES.DbContent;

public partial class BinhLuan
{
    public int Id { get; set; }

    public int SanPhamId { get; set; }

    public int UserId { get; set; }

    public double DanhGia { get; set; }

    public string? YKien { get; set; }

    public DateTime NgayTao { get; set; }
}
