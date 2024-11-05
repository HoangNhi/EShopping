using System;
using System.Collections.Generic;

namespace ENTITIES.DbContent;

public partial class TheLoai
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? ImageUrl { get; set; }

    public DateTime DateCreate { get; set; }

    public int Status { get; set; }

    public virtual ICollection<SanPham> SanPhams { get; set; } = new List<SanPham>();
}
