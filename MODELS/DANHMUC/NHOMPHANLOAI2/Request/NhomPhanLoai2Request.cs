using MODELS.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODELS.DANHMUC.NHOMPHANLOAI2.Request
{
    public class NhomPhanLoai2Request : BaseRequest
    {
        public Guid Id { get; set; }

        public Guid NhomPhanLoai1Id { get; set; }

        public string Name { get; set; } = null!;

        public int Price { get; set; }
    }
}
