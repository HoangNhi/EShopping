using System;
using System.Collections.Generic;

namespace ENTITIES.DbContent;

public partial class NhanHieu
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string? ImageUrl { get; set; }

    public DateTime DateCreate { get; set; }

    public int Status { get; set; }

    public virtual ICollection<SanPham> SanPhams { get; set; } = new List<SanPham>();
}
