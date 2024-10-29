using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODELS.HETHONG.ROLE.Dtos
{
    public class MODELRole
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public DateTime DateCreate { get; set; }

        public string CreateBy { get; set; } = null!;

        public bool Status { get; set; }
    }
}
