using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODELS.CHUCNANG.DIACHI.Dtos
{
    public class MODELDiaChi
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public string Name { get; set; } = null!;

        public string PhoneNumber { get; set; } = null!;

        public string Address { get; set; } = null!;

        public bool IsDefault { get; set; } = false;

        public DateOnly DateCreate { get; set; }


    }
}
