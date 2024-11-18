using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODELS.DANHMUC.TRALOIBINHLUAN.Dtos
{
    public class MODELTraLoiBinhLuan
    {
        public int Id { get; set; }

        public int BinhLuanId { get; set; }

        public string Content { get; set; } = null!;

        public DateTime DateCreate { get; set; }
    }
}
