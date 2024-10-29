using System;
using System.Collections.Generic;

namespace ENTITIES.DbContent;

public partial class HoaDon
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public int Total { get; set; }

    public DateTime DateCreate { get; set; }

    public Guid DiaChiId { get; set; }

    public string? PhoneNumber { get; set; }

    /// <summary>
    /// 1: Chờ thanh toán, 2: Chờ giao hàng, 3: Đã giao, 4: Đã hủy
    /// </summary>
    public int Status { get; set; }

    public virtual ICollection<ChiTietDonHang> ChiTietDonHangs { get; set; } = new List<ChiTietDonHang>();

    public virtual DiaChi DiaChi { get; set; } = null!;

    public virtual ApplicationUser User { get; set; } = null!;
}
