using MODELS.Base;
using MODELS.CHUCNANG.DIACHI.Dtos;
using MODELS.CHUCNANG.DIACHI.Request;
using MODELS.DANHMUC.BINHLUAN.Dtos;
using MODELS.DANHMUC.BINHLUAN.Request;

namespace BE.Services.CHUCNANG.DIACHI
{
    public interface IDIACHIService
    {
        BaseResponse<List<MODELDiaChi>> GetById(GetByIdRequest request);
        BaseResponse<MODELDiaChi> IsDefault(GetByIdRequest request);
        BaseResponse<MODELDiaChi> Create(DiaChiRequests request);
        BaseResponse<MODELDiaChi> Update(DiaChiRequests request);
        BaseResponse<string> Delete(GetByIdRequest request);
    }
}
