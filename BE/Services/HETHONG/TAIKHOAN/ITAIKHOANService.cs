using MODELS.Base;
using MODELS.HETHONG.TAIKHOAN.Dtos;
using MODELS.HETHONG.TAIKHOAN.Requests;

namespace BE.Services.HETHONG.TAIKHOAN
{
    public interface ITAIKHOANService
    {
        Task<BaseResponse<MODELTaiKhoan>> Login(PostLoginRequest request);
        Task<BaseResponse<MODELTaiKhoan>> Register(PostRegisterRequest request);
        Task<BaseResponse<MODELTaiKhoan>> ConfirmEmail(string UserIdEncode);
    }
}
