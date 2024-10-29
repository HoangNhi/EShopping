using MODELS.Base;
using MODELS.BASE;
using MODELS.HETHONG.NHOMQUYEN.Dtos;
using MODELS.HETHONG.NHOMQUYEN.Requests;

namespace BE.Services.HETHONG.NHOMQUYEN
{
    public interface INHOMQUYENService
    {
        BaseResponse<GetListPagingResponse> GetListPaging(GetListPagingRequest request);
        BaseResponse<MODELNhomQuyen> GetById(GetByIdRequest request);
        BaseResponse<NhomQuyenRequest> GetByPost(GetByIdRequest request);
        BaseResponse<MODELNhomQuyen> Create(NhomQuyenRequest request);
        BaseResponse<MODELNhomQuyen> Update(NhomQuyenRequest request);
        BaseResponse<string> Delete(DeleteListRequest id);
    }
}
