using AutoMapper;
using ENTITIES.DbContent;
using MODELS.Base;
using MODELS.CHUCNANG.CHITIETDONHANG.Dtos;
using MODELS.CHUCNANG.CHITIETDONHANG.Requests;
using MODELS.CHUCNANG.HOADON.Dtos;
using MODELS.HETHONG.LOG;
using System.IdentityModel.Tokens.Jwt;

namespace BE.Services.CHUCNANG.CHITIETHOADON
{
    public class ChiTietDonHangService : ICHITIETDONHANGService
    {
        private readonly EShoppingContext _context;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;
        public ChiTietDonHangService(EShoppingContext context, IMapper mapper, IHttpContextAccessor contextAccessor)
        {
            _context = context;
            _mapper = mapper;
            _contextAccessor = contextAccessor;
        }
        public BaseResponse<string> Delete(DeleteListRequest request)
        {
            throw new NotImplementedException();
        }

        public BaseResponse<List<MODELChiTietDonHang>> GetById(GetByIdRequest request)
        {
            var res = new BaseResponse<List<MODELChiTietDonHang>>();
            try
            {
                var item = _context.ChiTietDonHangs.Where(x => x.HoaDonId == request.Id);
                if (item != null)
                {
                    var result = _mapper.Map<List<MODELChiTietDonHang>>(item);
                    res.Data = result;
                }
                else
                {
                    res.Error = true;
                    res.Message = "Not Found!";
                    res.StatusCode = 404;
                }
            }
            catch (Exception ex)
            {
                res.Error = true;
                res.Message = ex.Message;
                res.StatusCode = 500;
            }
            return res;
        }

        public BaseResponse<MODELChiTietDonHang> Update(ChiTietDonHangRequests request)
        {
            var userId = _contextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Name)?.Value;
            var NhatKiDTO = new NhatKiDTO();
            var response = new BaseResponse<MODELChiTietDonHang>();
            try
            {
                var add = _mapper.Map<ChiTietDonHang>(request);
                //Lưu vào nhật kí
                NhatKiDTO.Name = "Chi tiết đơn hàng";
                NhatKiDTO.Id = Guid.NewGuid();
                NhatKiDTO.Event = "Cập nhật";
                NhatKiDTO.Date = DateTime.Now;
                NhatKiDTO.UserId = Guid.Parse(userId);
                NhatKiDTO.TargetId = add.Id;
                // Lưu vào Database
                _context.NhatKis.Add(_mapper.Map<NhatKi>(NhatKiDTO));
                _context.ChiTietDonHangs.Update(add);
                _context.SaveChanges();
                response.Data = _mapper.Map<MODELChiTietDonHang>(add);
            }
            catch (Exception ex)
            {
                response.Error = true;
                response.Message = ex.Message;
            }
            // Trả về dữ liệu

            return response;
        }
    }
}
