using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODELS.HETHONG.NHOMQUYEN.Dtos
{
    public class MODELNhomQuyen
    {
        public Guid Id { get; set; }

        public string TenGoi { get; set; } = null!;

        public string? Icon { get; set; }

        public int Status { get; set; } = 1;

        public DateTime DateCreate {  get; set; }
    }
}
