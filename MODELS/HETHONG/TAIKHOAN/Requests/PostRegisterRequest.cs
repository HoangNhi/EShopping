using FluentValidation;
using MODELS.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODELS.HETHONG.TAIKHOAN.Requests
{
    public class PostRegisterRequest : BaseRequest
    {
        public Guid Id { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Họ và tên không được để trống")]
        public string HoVaTen { get; set; } = null!;
        [Required(AllowEmptyStrings = false, ErrorMessage = "Email không được để trống")]
        [EmailAddress(ErrorMessage = "Email không đúng định dạng")]
        public string Username { get; set; } = null!;
        [Required(AllowEmptyStrings = false, ErrorMessage = "Email không được để trống")]
        [EmailAddress(ErrorMessage = "Email không đúng định dạng")]
        public string Email { get; set; } = null!;
        [Required(AllowEmptyStrings = false, ErrorMessage = "Mật khẩu không được để trống")]
        public string Password { get; set; } = null!;
        public string? RePassword { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Số điện thoại không được để trống")]
        public string PhoneNumber { get; set; } = null!;
    }

    public class PostRegisterRequestValidator : AbstractValidator<PostRegisterRequest>
    {
        public PostRegisterRequestValidator()
        {
            RuleFor(x => x.HoVaTen).NotEmpty().WithMessage("Họ và tên không được để trống");
            RuleFor(x => x.Username).NotEmpty().WithMessage("Email không được để trống").EmailAddress().WithMessage("Email không đúng định dạng");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email không được để trống").EmailAddress().WithMessage("Email không đúng định dạng");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Mật khẩu không được để trống");
            RuleFor(x => x.RePassword).NotEmpty().WithMessage("Nhập lại mật khẩu không được để trống");
            RuleFor(x => x.RePassword).Equal(x => x.Password).WithMessage("Nhập lại mật khẩu không khớp");
            RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("Số điện thoại không được để trống");
        }
    }
}
