using MODELS.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODELS.CHUCNANG.DIACHI.Request
{
    public class DiaChiRequests : BaseRequest
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public string Name { get; set; } = null!;

        public string PhoneNumber { get; set; } = null!;

        public string Address { get; set; } = null!;

    }
}
