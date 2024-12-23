﻿using System;
using System.Collections.Generic;

namespace ENTITIES.DbContent;

public partial class NhomPhanLoai1
{
    public Guid Id { get; set; }

    public Guid SanPhamId { get; set; }

    public string Name { get; set; } = null!;

    public string? ImageURL { get; set; }

    public virtual ICollection<ChiTietDonHang> ChiTietDonHangs { get; set; } = new List<ChiTietDonHang>();

    public virtual ICollection<NhomPhanLoai2> NhomPhanLoai2s { get; set; } = new List<NhomPhanLoai2>();

    public virtual SanPham SanPham { get; set; } = null!;
}
