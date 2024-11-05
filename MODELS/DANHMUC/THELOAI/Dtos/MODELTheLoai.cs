using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODELS.DANHMUC.THELOAI.Dtos
{
    public class MODELTheLoai
    {
        public string Id { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string? ImageUrl { get; set; }

        public DateTime DateCreate { get; set; }

        public int Status { get; set; }
    }
}
