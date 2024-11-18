using MODELS.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODELS.DANHMUC.NHOMPHANLOAI1.Request
{
    public class NhomPhanLoai1Requests : BaseRequest
    {
        public Guid Id { get; set; }

        public Guid SanPhamId { get; set; }

        public string Name { get; set; } = null!;

        public string? ImageURL { get; set; }
    }
}
