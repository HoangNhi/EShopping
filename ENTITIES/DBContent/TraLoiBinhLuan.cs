using System;
using System.Collections.Generic;

namespace ENTITIES.DbContent;

public partial class TraLoiBinhLuan
{
    public int Id { get; set; }

    public int BinhLuanId { get; set; }

    public string Content { get; set; } = null!;

    public DateTime DateCreate { get; set; }

    public virtual BinhLuan BinhLuan { get; set; } = null!;
}
