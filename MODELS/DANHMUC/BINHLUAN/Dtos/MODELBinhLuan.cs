﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODELS.DANHMUC.BINHLUAN.Dtos
{
    public class MODELBinhLuan
    {
        public int Id { get; set; }

        public Guid SanPhamId { get; set; }

        public Guid UserId { get; set; }

        public double Rate { get; set; }

        public string? Content { get; set; }

        public DateTime DateCreate { get; set; }
    }
}
