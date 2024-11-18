using MODELS.DANHMUC.NHOMPHANLOAI2.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODELS.DANHMUC.NHOMPHANLOAI1.Dtos
{
    public class NhomPhanLoai
    {
        public Guid Id { get; set; }

        public Guid SanPhamId { get; set; }

        public string Name { get; set; } = null!;

        public string? ImageURL { get; set; }

        public List<MODELNhomPhanLoai2> NhomPhanLoai2 { get; set; } = null;
    }
}
