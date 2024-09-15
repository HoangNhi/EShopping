using System;
using System.Collections.Generic;

namespace ENTITIES.DbContent;

public partial class ApplicationUser
{
    public Guid Id { get; set; }

    public string HoVaTen { get; set; } = null!;

    public string Username { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string PasswordSalt { get; set; } = null!;

    public string? Address { get; set; }

    public string PhoneNumber { get; set; } = null!;

    public Guid RoleId { get; set; }

    public bool? Vertify { get; set; }

    public DateTime NgayTao { get; set; }

    public string NguoiTao { get; set; } = null!;

    public DateTime? NgaySua { get; set; }

    public string? NguoiSua { get; set; }

    public DateTime? NgayXoa { get; set; }

    public string? NguoiXoa { get; set; }

    public bool IsActived { get; set; }

    public bool IsDeleted { get; set; }

    public virtual Role Role { get; set; } = null!;
}
