using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODELS.HETHONG.TAIKHOAN.Requests
{
    public class PostLoginRequest
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Tên đăng nhập không được để trống")]
        public string Username { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Mật khẩu không được để trống")]
        public string Password { get; set; }
    }
}
