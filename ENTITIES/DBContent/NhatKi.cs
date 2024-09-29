using System;
using System.Collections.Generic;

namespace ENTITIES.DbContent;

public partial class NhatKi
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string Event { get; set; } = null!;

    public DateOnly Date { get; set; }

    public int TargetId { get; set; }

    public Guid ApplicationUserId { get; set; }

    public virtual ApplicationUser ApplicationUser { get; set; } = null!;
}
