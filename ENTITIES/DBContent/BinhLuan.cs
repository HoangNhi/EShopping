using System;
using System.Collections.Generic;

namespace ENTITIES.DbContent;

public partial class BinhLuan
{
    public int Id { get; set; }

    public Guid SanPhamId { get; set; }

    public int UserId { get; set; }

    public double DanhGia { get; set; }

    public string? YKien { get; set; }

    public DateTime NgayTao { get; set; }

    public virtual SanPham SanPham { get; set; } = null!;

    public virtual ICollection<TraLoiBinhLuan> TraLoiBinhLuans { get; set; } = new List<TraLoiBinhLuan>();
}
