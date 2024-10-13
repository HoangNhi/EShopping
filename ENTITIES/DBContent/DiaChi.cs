using System;
using System.Collections.Generic;

namespace ENTITIES.DbContent;

public partial class DiaChi
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public string Name { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string Address { get; set; } = null!;

    public bool IsDefault { get; set; }

    public DateOnly DateCreate { get; set; }

    public virtual ICollection<HoaDon> HoaDons { get; set; } = new List<HoaDon>();

    public virtual ApplicationUser User { get; set; } = null!;
}
