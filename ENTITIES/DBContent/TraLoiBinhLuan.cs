﻿using System;
using System.Collections.Generic;

namespace ENTITIES.DBContent;

public partial class TraLoiBinhLuan
{
    public int Id { get; set; }

    public int BinhLuanId { get; set; }

    public string Content { get; set; } = null!;

    public DateTime NgayTao { get; set; }
}
