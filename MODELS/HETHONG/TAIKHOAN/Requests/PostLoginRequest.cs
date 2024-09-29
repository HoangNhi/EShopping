using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace MODELS.HETHONG.TAIKHOAN.Requests
{
    public class PostLoginRequest
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Tên đăng nhập không được để trống")]
        public string Username { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Mật khẩu không được để trống")]
        public string Password { get; set; }
        public bool RememberMe { get; set; } = false;
    }

    public class PostLoginRequestValidator : AbstractValidator<PostLoginRequest>
    {
        public PostLoginRequestValidator()
        {
            RuleFor(x => x.Username).NotEmpty().WithMessage("Tên đăng nhập không được để trống");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Mật khẩu không được để trống");
        }
    }
}
