using MODELS.Base;
using MODELS.BASE;
using MODELS.CHUCNANG.GIOHANG.Dtos;
using MODELS.CHUCNANG.GIOHANG.Requests;
using MODELS.DANHMUC.SANPHAM.Dtos;
using MODELS.DANHMUC.SANPHAM.Requests;

namespace BE.Services.CHUCNANG.GIOHANG
{
    public interface IGIOHANGService
    {
        BaseResponse<GetListPagingResponse> GetListPaging(GetListPagingRequest request);
        BaseResponse<MODELGioHang> GetById(GetByIdRequest request);
        BaseResponse<GioHangRequests> GetByPost(GetByIdRequest request);
        BaseResponse<MODELGioHang> Create(GioHangRequests request);
        BaseResponse<MODELGioHang> Update(GioHangRequests request);
        BaseResponse<string> Delete(DeleteListRequest request);
    }
}
