using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODELS.HETHONG.TAIKHOAN.Requests
{
    public class ChangePasswordRequest
    {
        public string Password { get; set; }
        public string NewPassWord { get; set; }
    }
}
