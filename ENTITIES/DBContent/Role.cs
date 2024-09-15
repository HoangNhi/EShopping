using System;
using System.Collections.Generic;

namespace ENTITIES.DBContent;

public partial class Role
{
    public Guid Id { get; set; }

    public string TenGoi { get; set; } = null!;

    public DateTime NgayTao { get; set; }

    public string NguoiTao { get; set; } = null!;

    public DateTime? NgaySua { get; set; }

    public string? NguoiSua { get; set; }

    public DateTime? NgayXoa { get; set; }

    public string? NguoiXoa { get; set; }

    public bool IsActived { get; set; }

    public bool IsDeleted { get; set; }

    public virtual ICollection<ApplicationUser> ApplicationUsers { get; set; } = new List<ApplicationUser>();

    public virtual ICollection<PhanQuyen> PhanQuyens { get; set; } = new List<PhanQuyen>();
}
