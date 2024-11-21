using MODELS.Base;
using MODELS.BASE;
using MODELS.HETHONG.TAIKHOAN.Dtos;
using MODELS.HETHONG.TAIKHOAN.Requests;

namespace BE.Services.HETHONG.TAIKHOAN
{
    public interface ITAIKHOANService
    {
        Task<BaseResponse<GetListPagingResponse>> GetListPaging(GetListPagingRequest request);
        Task<BaseResponse<MODELTaiKhoan>> GetById(GetByIdRequest request);
        Task<BaseResponse<MODELTaiKhoan>> Login(PostLoginRequest request);
        Task<BaseResponse<MODELTaiKhoan>> Register(PostRegisterRequest request);
        Task<BaseResponse<MODELTaiKhoan>> GoogleRegister(GoogleRegisterRequest request);
        Task<BaseResponse<MODELTaiKhoan>> ConfirmEmail(string UserIdEncode);
        Task<BaseResponse<MODELTaiKhoan>> ChangePassword(ChangePasswordRequest request);
    }
}
