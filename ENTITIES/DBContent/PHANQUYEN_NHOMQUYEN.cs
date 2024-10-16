using System;
using System.Collections.Generic;

namespace ENTITIES.DbContent;

public partial class PHANQUYEN_NHOMQUYEN
{
    public Guid Id { get; set; }

    public string TenGoi { get; set; } = null!;

    public string Icon { get; set; } = null!;

    public bool Status { get; set; }
}
