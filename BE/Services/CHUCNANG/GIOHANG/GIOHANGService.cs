using AutoMapper;
using ENTITIES.DbContent;
using MODELS.Base;
using MODELS.BASE;
using MODELS.CHUCNANG.GIOHANG.Dtos;
using MODELS.CHUCNANG.GIOHANG.Requests;
using MODELS.DANHMUC.SANPHAM.Dtos;
using MODELS.HETHONG.LOG;
using System.IdentityModel.Tokens.Jwt;

namespace BE.Services.CHUCNANG.GIOHANG
{
    public class GIOHANGService : IGIOHANGService
    {
        private readonly EShoppingContext _context;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;
        public GIOHANGService(EShoppingContext context, IMapper mapper, IHttpContextAccessor contextAccessor)
        {
            _context = context;
            _mapper = mapper;
            _contextAccessor = contextAccessor;
        }

        public BaseResponse<MODELGioHang> Create(GioHangRequests request)
        {
            var userId = _contextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Name)?.Value;
            var log = new NhatKiDTO();
            var response = new BaseResponse<MODELGioHang>();
            try
            {
                var checkData = _context.GioHangs.Where(
                    x => x.UserId == request.UserId 
                    &&
                    x.SanPhamId == request.SanPhamId
                ).ToList();

                if (checkData.Count > 0)
                {
                    foreach (var check in checkData) 
                    {
                        check.Quantity = request.Quantity;
                        _context.GioHangs.Update(check);
                        response.Data = _mapper.Map<MODELGioHang>(check);
                        log.Name = "Giỏ hàng";
                        log.Id = Guid.NewGuid();
                        log.Event = "Cập nhật";
                        log.Date = DateTime.Now;
                        log.UserId = Guid.Parse(userId);
                        log.TargetId = check.Id;
                        _context.NhatKis.Add(_mapper.Map<NhatKi>(log));
                    }
                }
                else
                {
                    var add = _mapper.Map<GioHang>(request);
                    add.Id = request.Id == Guid.Empty ? Guid.NewGuid() : request.Id;
                    add.DateCreated = DateTime.Now;
                    _context.GioHangs.Add(add);
                    response.Data = _mapper.Map<MODELGioHang>(add);
                    //Lưu vào nhật kí
                    log.Name = "Giỏ hàng";
                    log.Id = Guid.NewGuid();
                    log.Event = "Thêm";
                    log.Date = DateTime.Now;
                    log.UserId = Guid.Parse(userId);
                    log.TargetId = add.Id;
                    _context.NhatKis.Add(_mapper.Map<NhatKi>(log));
                }
                //Lưu vào Database
                _context.SaveChanges();
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
            var log = new NhatKiDTO();
            var response = new BaseResponse<string>();
            try
            {
                foreach (var id in request.Ids)
                {
                    var delete = _context.GioHangs.Find(id);
                    if (delete != null)
                    {
                        _context.GioHangs.Remove(delete);
                        log.Name = "Giỏ hàng";
                        log.Id = Guid.NewGuid();
                        log.Event = "Cập nhật";
                        log.Date = DateTime.Now;
                        log.UserId = Guid.Parse(userId);
                        log.TargetId = delete.Id;
                        _context.NhatKis.Add(_mapper.Map<NhatKi>(log));
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

        public BaseResponse<MODELGioHang> GetById(GetByIdRequest request)
        {
            throw new NotImplementedException();
        }

        public BaseResponse<GioHangRequests> GetByPost(GetByIdRequest request)
        {
            throw new NotImplementedException();
        }

        public BaseResponse<GetListPagingResponse> GetListPaging(GetListPagingRequest request)
        {
            var res = new BaseResponse<GetListPagingResponse>();
            try
            {
                var data = new List<MODELGioHang>();
                var result = _context.GioHangs.Skip((request.PageIndex - 1) * request.RowsPerPage).Take(request.RowsPerPage).ToList();
                    data = _mapper.Map<List<MODELGioHang>>(result);
                var page = new GetListPagingResponse();
                page.PageIndex = request.PageIndex;
                page.TotalRow = _context.GioHangs.Count();
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

        public BaseResponse<MODELGioHang> Update(GioHangRequests request)
        {
            throw new NotImplementedException();
        }
    }
}
