using System;
using System.Collections.Generic;

namespace ENTITIES.DbContent;

public partial class BinhLuan
{
    public int Id { get; set; }

    public Guid SanPhamId { get; set; }

    public int UserId { get; set; }

    public double Rate { get; set; }

    public string? Content { get; set; }

    public DateTime DateCreate { get; set; }

    public virtual SanPham SanPham { get; set; } = null!;

    public virtual ICollection<TraLoiBinhLuan> TraLoiBinhLuans { get; set; } = new List<TraLoiBinhLuan>();
}
