using AutoMapper;
using ENTITIES.DbContent;
using Microsoft.EntityFrameworkCore;
using MODELS.Base;
using MODELS.BASE;
using MODELS.CHUCNANG.HOADON.Dtos;
using MODELS.CHUCNANG.HOADON.Requests;
using MODELS.DANHMUC.SANPHAM.Dtos;
using System.IdentityModel.Tokens.Jwt;
using MODELS.HETHONG.LOG;
using MODELS.CHUCNANG.CHITIETDONHANG.Dtos;

namespace BE.Services.CHUCNANG.HOADON
{
    public class HOADONService : IHOADONService
    {
        private readonly EShoppingContext _context;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;
        public HOADONService(EShoppingContext context, IMapper mapper, IHttpContextAccessor contextAccessor)
        {
            _context = context;
            _mapper = mapper;
            _contextAccessor = contextAccessor;
        }
        public BaseResponse<MODELHoaDon> Create(HoaDonRequests request)
        {
            var userId = _contextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Name)?.Value;
            var NhatKiDTO = new NhatKiDTO();
            var response = new BaseResponse<MODELHoaDon>();
            try
            {
                var add = _mapper.Map<HoaDon>(request);
                add.Id = Guid.NewGuid();
                add.DateCreate = DateTime.UtcNow;
                foreach (var item in request.ChiTietDonHangRequests) 
                {
                    var i = _mapper.Map<ChiTietDonHang>(item);
                    i.Id = Guid.NewGuid();
                    i.HoaDonId = add.Id;
                    i.DateCreated = DateTime.UtcNow;
                    _context.ChiTietDonHangs.Add(i);
                }
                //Lưu vào nhật kí
                NhatKiDTO.Name = "Hoá đơn";
                NhatKiDTO.Id = Guid.NewGuid();
                NhatKiDTO.Event = "Tạo";
                NhatKiDTO.Date = DateTime.Now;
                NhatKiDTO.UserId = Guid.Parse(userId);
                NhatKiDTO.TargetId = add.Id;
                _context.NhatKis.Add(_mapper.Map<NhatKi>(NhatKiDTO));
                // Lưu vào Database
                _context.HoaDons.Add(add);
                _context.SaveChanges();

                // Trả về dữ liệu
                response.Data = _mapper.Map<MODELHoaDon>(add);
            }
            catch (Exception ex)
            {
                response.Error = true;
                response.Message = ex.Message;
            }
            return response;
        }

        public BaseResponse<string> Delete(DeleteListRequest request)
        {
            var userId = _contextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Name)?.Value;
            var NhatKiDTO = new NhatKiDTO();
            var response = new BaseResponse<string>();

            try
            {
                foreach (var id in request.Ids)
                {
                    var delete = _context.HoaDons.Find(id);
                    if (delete != null)
                    {
                        delete.Status = -1;
                        delete.DateCreate = DateTime.Now;
                        _context.HoaDons.Update(delete);
                        //Lưu vào nhật kí
                        NhatKiDTO.Name = "Sản phẩm";
                        NhatKiDTO.Id = Guid.NewGuid();
                        NhatKiDTO.Event = "Xoá";
                        NhatKiDTO.Date = DateTime.Now;
                        NhatKiDTO.UserId = Guid.Parse(userId);
                        NhatKiDTO.TargetId = delete.Id;
                        _context.NhatKis.Add(_mapper.Map<NhatKi>(NhatKiDTO));
                    }
                    else
                    {
                        throw new Exception("Không tìm thấy dữ liệu");
                    }
                }

                _context.SaveChanges();
                response.Data = String.Join(",", request);
            }
            catch (Exception ex)
            {
                response.Error = true;
                response.Message = ex.Message;
            }
            return response;
        }

        public BaseResponse<List<MODELHoaDon>> GetById(GetByIdRequest request)
        {
            var res = new BaseResponse<List<MODELHoaDon>>();
            try
            {
                var item = _context.HoaDons.Where(x => x.UserId == request.Id);
                if (item != null)
                {
                    var result = _mapper.Map<List<MODELHoaDon>>(item);
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

        public BaseResponse<HoaDonResponse> GetByPost(GetByIdRequest request)
        {
            var res = new BaseResponse<HoaDonResponse>();
            try
            {
                var item = _context.HoaDons.Find(request.Id);
                if (item != null)
                {
                    var result = _mapper.Map<HoaDonResponse>(item);
                    var cthd = _context.ChiTietDonHangs.Where(x => x.HoaDonId == result.Id).ToList();
                    if (cthd != null)
                    {
                        result.chiTietHoaDon = _mapper.Map<List<MODELChiTietDonHang>>(cthd);
                    }
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

        public BaseResponse<GetListPagingResponse> GetListPaging(GetListPagingRequest request)
        {
            var res = new BaseResponse<GetListPagingResponse>();
            try
            {
                var data = new List<MODELHoaDon>();
                var result = _context.HoaDons.OrderByDescending(hd => hd.DateCreate).Skip((request.PageIndex - 1) * request.RowsPerPage).Take(request.RowsPerPage).ToList();
                data = _mapper.Map<List<MODELHoaDon>>(result);
                
                var page = new GetListPagingResponse();
                page.PageIndex = request.PageIndex;
                page.TotalRow = _context.HoaDons.Count();
                page.Data = data;
                res.Data = page;
            }
            catch (Exception ex)
            {
                res.Error = true;
                res.Message = ex.Message;
                res.StatusCode = 500;
            }
            return res;
        }

        public BaseResponse<MODELHoaDon> Update(HoaDonRequests request)
        {
            var userId = _contextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Name)?.Value;
            var NhatKiDTO = new NhatKiDTO();
            var response = new BaseResponse<MODELHoaDon>();
            try
            {
                var add = _mapper.Map<HoaDon>(request);
                add.DateCreate = DateTime.Now;
                //Lưu vào nhật kí
                NhatKiDTO.Name = "Sản phẩm";
                NhatKiDTO.Id = Guid.NewGuid();
                NhatKiDTO.Event = "Cập nhật";
                NhatKiDTO.Date = DateTime.Now;
                NhatKiDTO.UserId = Guid.Parse(userId);
                NhatKiDTO.TargetId = add.Id;
                // Lưu vào Database
                _context.NhatKis.Add(_mapper.Map<NhatKi>(NhatKiDTO));
                _context.HoaDons.Update(add);
                _context.SaveChanges();
                response.Data = _mapper.Map<MODELHoaDon>(add);
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
