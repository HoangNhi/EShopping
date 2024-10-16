using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODELS.HETHONG.NHOMQUYEN.Requests
{
    public class PostNhomQuyen
    {
        public Guid Id { get; set; }

        public string TenGoi { get; set; } = null!;

        public string Icon { get; set; } = null!;

        public bool Status { get; set; }
    }
}
