using MODELS.Base;
using MODELS.BASE;
using MODELS.DANHMUC.NHANHIEU.Dtos;
using MODELS.DANHMUC.NHANHIEU.Requests;
using MODELS.DANHMUC.THELOAI.Dtos;
using MODELS.DANHMUC.THELOAI.Requests;

namespace BE.Services.DANHMUC.NHANHIEU
{
    public interface INHANHIEUService
    {
        BaseResponse<GetListPagingResponse> GetListPaging(GetListPagingRequest request);
        BaseResponse<MODELNhanHieu> GetById(GetByIdRequest request);
        BaseResponse<NhanHieuRequests> GetByPost(GetByIdRequest request);
        BaseResponse<MODELNhanHieu> Create(NhanHieuRequests request);
        BaseResponse<MODELNhanHieu> Update(NhanHieuRequests request);
        BaseResponse<string> Delete(DeleteListRequest request);
    }
}
