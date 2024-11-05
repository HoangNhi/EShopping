using MODELS.Base;
using MODELS.BASE;
using MODELS.DANHMUC.THELOAI.Dtos;
using MODELS.DANHMUC.THELOAI.Requests;

namespace BE.Services.DANHMUC.THELOAI
{
    public interface ITHELOAIService
    {
        BaseResponse<GetListPagingResponse> GetListPaging(GetListPagingRequest request);
        BaseResponse<MODELTheLoai> GetById(GetByIdRequest request);
        BaseResponse<TheLoaiRequest> GetByPost(GetByIdRequest request);
        BaseResponse<MODELTheLoai> Create(TheLoaiRequest request);
        BaseResponse<MODELTheLoai> Update(TheLoaiRequest request);
        BaseResponse<string> Delete(DeleteListRequest id);
    }
}
