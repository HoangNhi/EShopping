using MODELS.Base;
using MODELS.BASE;
using MODELS.DANHMUC.SANPHAM.Dtos;
using MODELS.DANHMUC.SANPHAM.Requests;
using MODELS.HETHONG.LOG;

namespace BE.Services.HETHONG.LOG
{
    public interface ILOGService
    {
        BaseResponse<GetListPagingResponse> GetAllListPaging(GetListPagingRequest request);
        BaseResponse<GetListPagingResponse> GetListPagingSanPham(GetListPagingRequest request);
        BaseResponse<GetListPagingResponse> GetListPagingTheLoai(GetListPagingRequest request);
        BaseResponse<GetListPagingResponse> GetListPagingNhanHieu(GetListPagingRequest request);
        BaseResponse<GetListPagingResponse> GetListPagingGioHang(GetListPagingRequest request);
        BaseResponse<GetListPagingResponse> GetListPagingHoaDon(GetListPagingRequest request);
        BaseResponse<GetListPagingResponse> GetListPagingBinhLuan(GetListPagingRequest request);
        BaseResponse<GetListPagingResponse> GetListPagingTaiKhoan(GetListPagingRequest request);



    }
}
