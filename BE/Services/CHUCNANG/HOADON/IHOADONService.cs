using MODELS.Base;
using MODELS.BASE;
using MODELS.CHUCNANG.HOADON.Dtos;
using MODELS.CHUCNANG.HOADON.Requests;

namespace BE.Services.CHUCNANG.HOADON
{
    public interface IHOADONService
    {
        BaseResponse<GetListPagingResponse> GetListPaging(GetListPagingRequest request);
        BaseResponse<List<MODELHoaDon>> GetById(GetByIdRequest request);
        BaseResponse<HoaDonResponse> GetByPost(GetByIdRequest request);
        BaseResponse<MODELHoaDon> Create(HoaDonRequests request);
        BaseResponse<MODELHoaDon> Update(HoaDonRequests request);
        BaseResponse<string> Delete(DeleteListRequest request);
    }
}
