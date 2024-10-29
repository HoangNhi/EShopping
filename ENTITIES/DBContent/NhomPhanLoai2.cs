using System;
using System.Collections.Generic;

namespace ENTITIES.DbContent;

public partial class NhomPhanLoai2
{
    public Guid Id { get; set; }

    public Guid NhomPhanLoai1Id { get; set; }

    public string Name { get; set; } = null!;

    public int Price { get; set; }

    public virtual ICollection<ChiTietDonHang> ChiTietDonHangs { get; set; } = new List<ChiTietDonHang>();

    public virtual NhomPhanLoai1 NhomPhanLoai1 { get; set; } = null!;
}
