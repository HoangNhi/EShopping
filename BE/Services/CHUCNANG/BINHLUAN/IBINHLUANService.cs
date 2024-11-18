using MODELS.Base;
using MODELS.BASE;
using MODELS.CHUCNANG.HOADON.Dtos;
using MODELS.CHUCNANG.HOADON.Requests;
using MODELS.DANHMUC.BINHLUAN.Dtos;
using MODELS.DANHMUC.BINHLUAN.Request;

namespace BE.Services.CHUCNANG.BINHLUAN
{
    public interface IBINHLUANService
    {
        BaseResponse<GetListPagingResponse> GetListPaging(GetListPagingRequest request);
        BaseResponse<BinhLuanResponse> GetById(GetByIdRequest request);
        BaseResponse<bool> IsAllow(Guid UserId, Guid SanPhamId);
        BaseResponse<MODELBinhLuan> Create(BinhLuanRequests request);
        BaseResponse<MODELBinhLuan> Update(BinhLuanRequests request);
        BaseResponse<string> Delete(DeleteListRequest request);
    }
}
