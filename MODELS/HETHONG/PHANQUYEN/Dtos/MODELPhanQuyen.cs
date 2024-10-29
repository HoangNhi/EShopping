using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODELS.HETHONG.PHANQUYEN.Dtos
{
    public class MODELPhanQuyen
    {
        public Guid Id { get; set; }

        public Guid RoleId { get; set; }

        public string ControllerName { get; set; } = null!;

        public bool IsXem { get; set; }

        public bool IsThem { get; set; }

        public bool IsCapNhat { get; set; }

        public bool IsXoa { get; set; }
    }
}
