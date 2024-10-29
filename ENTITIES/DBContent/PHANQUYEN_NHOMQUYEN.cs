using System;
using System.Collections.Generic;

namespace ENTITIES.DbContent;

public partial class PHANQUYEN_NHOMQUYEN
{
    public Guid Id { get; set; }

    public string TenGoi { get; set; } = null!;

    public string? Icon { get; set; }

    public DateTime DateCreate { get; set; }

    public string CreateBy { get; set; } = null!;

    /// <summary>
    /// 1: Đang hoạt động, 0: Không hoạt động, -1: Đã xóa
    /// </summary>
    public int Status { get; set; }
}
