using System;
using System.Collections.Generic;

namespace ENTITIES.DBContent;

public partial class PhanQuyen
{
    public Guid Id { get; set; }

    public Guid RoleId { get; set; }

    public string ControllerName { get; set; } = null!;

    public bool IsXem { get; set; }

    public bool IsThem { get; set; }

    public bool IsCapNhat { get; set; }

    public bool IsXoa { get; set; }

    public virtual Role Role { get; set; } = null!;
}
