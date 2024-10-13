using System;
using System.Collections.Generic;

namespace ENTITIES.DbContent;

public partial class GioHang
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public Guid SanPhamId { get; set; }

    public Guid NhomPhanLoai1Id { get; set; }

    public Guid NhanPhanLoai2Id { get; set; }

    public int Quantity { get; set; }

    public DateTime DateCreated { get; set; }
}
