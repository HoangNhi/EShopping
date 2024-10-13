using System;
using System.Collections.Generic;

namespace ENTITIES.DbContent;

public partial class CauHinhSanPham
{
    public Guid Id { get; set; }

    public Guid SanPhamId { get; set; }

    public string Name { get; set; } = null!;

    public string Detail { get; set; } = null!;

    public virtual SanPham SanPham { get; set; } = null!;
}
