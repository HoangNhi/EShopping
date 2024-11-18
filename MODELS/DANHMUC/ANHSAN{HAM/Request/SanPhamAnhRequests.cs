using MODELS.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODELS.DANHMUC.ANHSAN_HAM.Request
{
    public class SanPhamAnhRequests : BaseRequest
    {
        public Guid Id { get; set; }

        public Guid SanPhamId { get; set; }

        public string ImageURL { get; set; } = null!;

        public DateTime DateCreate { get; set; }

        public Guid CreateById { get; set; }
    }
}
