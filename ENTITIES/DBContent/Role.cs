using System;
using System.Collections.Generic;

namespace ENTITIES.DbContent;

public partial class Role
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public DateTime DateCreate { get; set; }

    public string CreateBy { get; set; } = null!;

    public bool Status { get; set; }

    public virtual ICollection<ApplicationUser> ApplicationUsers { get; set; } = new List<ApplicationUser>();

    public virtual ICollection<PhanQuyen> PhanQuyens { get; set; } = new List<PhanQuyen>();
}
