using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODELS.DANHMUC.ANHSAN_HAM.Dtos
{
    public class MODELSanPhamAnh
    {
        public Guid Id { get; set; }

        public Guid SanPhamId { get; set; }

        public string ImageURL { get; set; } = null!;

        public DateTime DateCreate { get; set; } = DateTime.Now;

        public Guid CreateById { get; set; } 
    }
}
