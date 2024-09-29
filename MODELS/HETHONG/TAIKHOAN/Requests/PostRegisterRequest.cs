using FluentValidation;

namespace MODELS.HETHONG.TAIKHOAN.Requests
{
    public class PostRegisterRequest
    {
        public Guid Id { get; set; }

        public string Fullname { get; set; } = null!;

        public string Username { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string? RePassword { get; set; }

        public string PhoneNumber { get; set; } = null!;
    }

    public class PostRegisterRequestValidator : AbstractValidator<PostRegisterRequest>
    {
        public PostRegisterRequestValidator()
        {
            RuleFor(x => x.Fullname).NotEmpty().WithMessage("Họ và tên không được để trống");
            RuleFor(x => x.Username).NotEmpty().WithMessage("Email không được để trống").EmailAddress().WithMessage("Email không đúng định dạng");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email không được để trống").EmailAddress().WithMessage("Email không đúng định dạng");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Mật khẩu không được để trống");
            RuleFor(x => x.RePassword).NotEmpty().WithMessage("Nhập lại mật khẩu không được để trống");
            RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("Số điện thoại không được để trống");
        }
    }
}
