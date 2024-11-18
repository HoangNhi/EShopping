using MODELS.Base;
using MODELS.BASE;
using MODELS.DANHMUC.CAUHINHSANPHAM.Dtos;
using MODELS.DANHMUC.CAUHINHSANPHAM.Request;
using MODELS.DANHMUC.SANPHAM.Dtos;
using MODELS.DANHMUC.SANPHAM.Requests;

namespace BE.Services.DANHMUC.CAUHINHSANPHAM
{
    public interface ICAUHINHSANPHAMService
    {
        BaseResponse<GetListPagingResponse> GetListPaging(GetListPagingRequest request);
        BaseResponse<List<MODELCauHinhSanPham>> GetById(GetByIdRequest request);
        BaseResponse<CauHinhSanPhamRequests> GetByPost(GetByIdRequest request);
        BaseResponse<List<MODELCauHinhSanPham>> Create(List<CauHinhSanPhamRequests> request);
        BaseResponse<List<MODELCauHinhSanPham>> Update(List<CauHinhSanPhamRequests> request);
        BaseResponse<string> Delete(DeleteListRequest request);
    }
}
