using MODELS.DANHMUC.NHOMPHANLOAI1.Dtos;
using MODELS.DANHMUC.NHOMPHANLOAI2.Dtos;
using MODELS.DANHMUC.SANPHAM.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODELS.CHUCNANG.GIOHANG.Dtos
{
    public class MODELGioHang
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public Guid SanPhamId { get; set; }

        public Guid NhomPhanLoai1Id { get; set; }

        public Guid NhanPhanLoai2Id { get; set; }

        public int Quantity { get; set; }
        public DateTime DateCreated { get; set; }

        public MODELSanPham? sanPham { get; set; } = null;

        public MODELNhomPhanLoai1? nhomPhanLoai1 { get; set; } = null;

        public MODELNhomPhanLoai2? nhomPhanLoai2 { get; set; } = null;


    }
}
