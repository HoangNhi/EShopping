using MODELS.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODELS.HETHONG.TAIKHOAN.Requests
{
    public class PostRegisterRequest : BaseRequest
    {
        public Guid Id { get; set; }
        public string HoVaTen { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
    }
}
