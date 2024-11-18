using MODELS.Base;
using MODELS.CHUCNANG.CHITIETDONHANG.Dtos;
using MODELS.CHUCNANG.CHITIETDONHANG.Requests;
using MODELS.CHUCNANG.GIOHANG.Dtos;
using MODELS.CHUCNANG.GIOHANG.Requests;

namespace BE.Services.CHUCNANG.CHITIETHOADON
{
    public interface ICHITIETDONHANGService
    {
        BaseResponse<List<MODELChiTietDonHang>> GetById(GetByIdRequest request);
        BaseResponse<MODELChiTietDonHang> Update(ChiTietDonHangRequests request);
        BaseResponse<string> Delete(DeleteListRequest request);
    }
}
