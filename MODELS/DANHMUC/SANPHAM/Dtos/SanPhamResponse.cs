using MODELS.DANHMUC.ANHSAN_HAM.Dtos;
using MODELS.DANHMUC.BINHLUAN.Dtos;
using MODELS.DANHMUC.CAUHINHSANPHAM.Dtos;
using MODELS.DANHMUC.NHANHIEU.Dtos;
using MODELS.DANHMUC.NHOMPHANLOAI1.Dtos;
using MODELS.DANHMUC.NHOMPHANLOAI2.Dtos;
using MODELS.DANHMUC.THELOAI.Dtos;
using MODELS.DANHMUC.TRALOIBINHLUAN.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODELS.DANHMUC.SANPHAM.Dtos
{
    public class SanPhamResponse
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

        public List<MODELSanPhamAnh>? anhSanPham { get; set; } = null;

        public MODELTheLoai? theLoai { get; set; } = null;

        public MODELNhanHieu? nhanHieu { get; set; } = null;

        public List<MODELCauHinhSanPham>? cauHinhSanPham { get; set; } = null;

        public List<NhomPhanLoai>? nhomPhanLoai { get; set; } = null;

        public List<BinhLuanResponse>? BinhLuan { get; set; } = null;

        


    }
}
