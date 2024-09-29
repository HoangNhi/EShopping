using System;
using System.Collections.Generic;

namespace ENTITIES.DbContent;

public partial class SanPham_Anh
{
    public Guid Id { get; set; }

    public Guid SanPhamId { get; set; }

    public string ImageURL { get; set; } = null!;

    public DateTime DateCreate { get; set; }

    public Guid CreateById { get; set; }

    public virtual SanPham SanPham { get; set; } = null!;
}
