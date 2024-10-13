using System;
using System.Collections.Generic;

namespace ENTITIES.DbContent;

public partial class ApplicationUser
{
    public Guid Id { get; set; }

    public string Username { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string FullName { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string PasswordSalt { get; set; } = null!;

    public string? Address { get; set; }

    public string? PhoneNumber { get; set; }

    public Guid RoleId { get; set; }

    public bool Vertify { get; set; }

    public DateTime DateCreate { get; set; }

    public bool Status { get; set; }

    public virtual ICollection<DiaChi> DiaChis { get; set; } = new List<DiaChi>();

    public virtual ICollection<HoaDon> HoaDons { get; set; } = new List<HoaDon>();

    public virtual ICollection<NhatKi> NhatKis { get; set; } = new List<NhatKi>();

    public virtual Role Role { get; set; } = null!;
}
