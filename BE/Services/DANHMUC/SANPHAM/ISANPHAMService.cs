using MODELS.Base;
using MODELS.BASE;
using MODELS.DANHMUC.NHANHIEU.Dtos;
using MODELS.DANHMUC.NHANHIEU.Requests;
using MODELS.DANHMUC.SANPHAM.Dtos;
using MODELS.DANHMUC.SANPHAM.Requests;

namespace BE.Services.DANHMUC.SANPHAM
{
    public interface ISANPHAMService
    {
        BaseResponse<GetListPagingResponse> GetListPaging(GetListPagingRequest request);
        BaseResponse<MODELSanPham> GetById(GetByIdRequest request);
        BaseResponse<SanPhamRequests> GetByPost(GetByIdRequest request);
        BaseResponse<MODELSanPham> Create(SanPhamRequests request);
        BaseResponse<MODELSanPham> Update(SanPhamRequests request);
        BaseResponse<string> Delete(DeleteListRequest request);
        BaseResponse<GetListPagingResponse> GetCustom(SanPhamCustomRequest request);
        BaseResponse<SanPhamResponse> GetProduct(GetByIdRequest request);
        BaseResponse<SanPhamResponse> PostProduct(SanPhamRequestAll request);
        BaseResponse<SanPhamResponse> PutProduct(SanPhamRequestAll request);
    }
}
