﻿using System;
using System.Collections.Generic;

namespace ENTITIES.DbContent;

public partial class NhatKi
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string Event { get; set; } = null!;

    public DateTime? Date { get; set; }

    public Guid TargetId { get; set; }

    public Guid UserId { get; set; }

    public virtual ApplicationUser User { get; set; } = null!;
}
