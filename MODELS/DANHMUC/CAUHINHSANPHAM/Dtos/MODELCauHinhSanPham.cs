using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODELS.DANHMUC.CAUHINHSANPHAM.Dtos
{
    public class MODELCauHinhSanPham
    {
        public Guid Id { get; set; }

        public Guid SanPhamId { get; set; }

        public string Name { get; set; } = null!;

        public string Detail { get; set; } = null!;
    }
}
