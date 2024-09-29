using System;
using System.Collections.Generic;

namespace ENTITIES.DbContent;

public partial class DiaChi
{
    public int Id { get; set; }

    public Guid ApplicationUserId { get; set; }

    public string Name { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string Address { get; set; } = null!;

    public bool IsDefault { get; set; }

    public DateOnly DateCreate { get; set; }

    public virtual ApplicationUser ApplicationUser { get; set; } = null!;
}
