﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODELS.DANHMUC.SANPHAM.Dtos
{
    public class MODELSanPham
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public decimal Price { get; set; }

        public string Description { get; set; } = null!;

        public string ImageUrl { get; set; } = null!;

        public string TheLoaiId { get; set; } = null!;

        public Guid NhanHieuId { get; set; }

        public DateTime DateCreate { get; set; }

        public bool? IsNew { get; set; }

        public bool? IsBestSelling { get; set; }

        public bool? IsSale { get; set; }

        public int? Quantity { get; set; }

        public int? Sold { get; set; }

        public int Status { get; set; }
    }
}
