using System.ComponentModel.DataAnnotations;

namespace MODELS.HETHONG.TAIKHOAN.Requests
{
    public class PostRegisterRequest
    {
        public Guid Id { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Họ và tên không được để trống")]
        public string Fullname { get; set; } = null!;
        public string Username { get; set; } = null!;
        [Required(AllowEmptyStrings = false, ErrorMessage = "Email không được để trống")]
        public string Email { get; set; } = null!;
        [Required(AllowEmptyStrings = false, ErrorMessage = "Mật khẩu không được để trống")]
        public string Password { get; set; } = null!;
        [Required(AllowEmptyStrings = false, ErrorMessage = "Xác nhận mật khẩu không được để trống")]
        public string RePassword { get; set; } = null!;

    }
}
