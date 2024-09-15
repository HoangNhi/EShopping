using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODELS.Base
{
    public class BaseModel
    {
        public DateTime NgayTao { get; set; }
        public string NguoiTao { get; set; } = null!;
        public DateTime NgaySua { get; set; }
        public string NguoiSua { get; set; } = null!;
        public bool IsActived { get; set; }

    }
}
