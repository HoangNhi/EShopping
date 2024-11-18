using AutoMapper;
using ENTITIES.DbContent;
using Microsoft.EntityFrameworkCore;
using MODELS.Base;
using MODELS.BASE;
using MODELS.DANHMUC.BINHLUAN.Dtos;
using MODELS.DANHMUC.BINHLUAN.Request;
using MODELS.DANHMUC.SANPHAM.Dtos;
using MODELS.HETHONG.LOG;
using System.IdentityModel.Tokens.Jwt;

namespace BE.Services.CHUCNANG.BINHLUAN
{
    public class BINHLUANService : IBINHLUANService
    {
        private readonly EShoppingContext _context;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;
        public BINHLUANService(EShoppingContext context, IMapper mapper, IHttpContextAccessor contextAccessor)
        {
            _context = context;
            _mapper = mapper;
            _contextAccessor = contextAccessor;
        }
        public BaseResponse<MODELBinhLuan> Create(BinhLuanRequests request)
        {
            var userId = _contextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Name)?.Value;
            var NhatKiDTO = new NhatKiDTO();
            var response = new BaseResponse<MODELBinhLuan>();
            try
            {

                var add = _mapper.Map<BinhLuan>(request);
                add.Id = _context.BinhLuans.Max(x => x.Id) + 1;
                add.DateCreate = DateTime.Now;

                //Lưu vào nhật kí
                NhatKiDTO.Name = "Bình luận";
                NhatKiDTO.Id = Guid.NewGuid();
                NhatKiDTO.Event = "Thêm";
                NhatKiDTO.Date = DateTime.Now;
                NhatKiDTO.UserId = Guid.Parse(userId);
                NhatKiDTO.TargetId = Guid.Parse(add.Id.ToString());
                _context.NhatKis.Add(_mapper.Map<NhatKi>(NhatKiDTO));
                // Lưu vào Database
                _context.BinhLuans.Add(add);
                _context.SaveChanges();

                // Trả về dữ liệu
                response.Data = _mapper.Map<MODELBinhLuan>(add);
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
                    var delete = _context.BinhLuans.Find(id);
                    if (delete != null)
                    {
                        _context.BinhLuans.Remove(delete);
                        //Lưu vào nhật kí
                        NhatKiDTO.Name = "Bình luận";
                        NhatKiDTO.Id = Guid.NewGuid();
                        NhatKiDTO.Event = "Xoá";
                        NhatKiDTO.Date = DateTime.Now;
                        NhatKiDTO.UserId = Guid.Parse(userId);
                        NhatKiDTO.UserId = Guid.Parse(delete.Id.ToString());
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

        public BaseResponse<BinhLuanResponse> GetById(GetByIdRequest request)
        {
            var res = new BaseResponse<BinhLuanResponse>();
            try
            {
                var item = _context.BinhLuans.Find(request.Id);
                if (item != null)
                {
                    var result = _mapper.Map<BinhLuanResponse>(item);
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

        public BaseResponse<bool> IsAllow(Guid UserId, Guid SanPhamId)
        {
            var res = new BaseResponse<bool>();
            try
            {
                var item = from h in _context.HoaDons join 
                           c in _context.ChiTietDonHangs
                           on h.Id equals c.HoaDonId
                           where h.UserId == UserId && c.SanPhamId == SanPhamId
                           select new
                           {
                               UserId = h.UserId,
                               HoaDonId = h.Id,
                               SanPhamId = c.SanPhamId,
                           }
                           ;
                if (item.Any())
                {
                    res.Data = true;
                }
                else
                {
                    res.Data = false;
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
                var data = new List<MODELBinhLuan>();
                if (!string.IsNullOrEmpty(request.TextSearch)) //Tìm theo tên sản phẩm
                {
                    var product = _context.SanPhams.FirstOrDefault(x => x.Name == request.TextSearch);
                    if (product != null)
                    { 
                        var result = _context.BinhLuans.Where(x => x.SanPhamId == product.Id).Skip((request.PageIndex - 1) * request.RowsPerPage).Take(request.RowsPerPage).ToList();
                        data = _mapper.Map<List<MODELBinhLuan>>(result);
                    }
                }
                else
                {
                    var result = _context.BinhLuans.Skip((request.PageIndex - 1) * request.RowsPerPage).Take(request.RowsPerPage).ToList();
                    data = _mapper.Map<List<MODELBinhLuan>>(result);
                }
                var page = new GetListPagingResponse();
                page.PageIndex = request.PageIndex;
                page.TotalRow = _context.BinhLuans.Count();
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

        public BaseResponse<MODELBinhLuan> Update(BinhLuanRequests request)
        {
            var userId = _contextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Name)?.Value;
            var NhatKiDTO = new NhatKiDTO();
            var response = new BaseResponse<MODELBinhLuan>();
            try
            {
                var add = _mapper.Map<BinhLuan>(request);
                add.DateCreate = DateTime.Now;
                //Lưu vào nhật kí
                NhatKiDTO.Name = "Bình luận";
                NhatKiDTO.Id = Guid.NewGuid();
                NhatKiDTO.Event = "Cập nhật";
                NhatKiDTO.Date = DateTime.Now;
                NhatKiDTO.UserId = Guid.Parse(userId);
                NhatKiDTO.UserId = Guid.Parse(userId);
                // Lưu vào Database
                _context.NhatKis.Add(_mapper.Map<NhatKi>(NhatKiDTO));
                _context.BinhLuans.Update(add);
                _context.SaveChanges();
                response.Data = _mapper.Map<MODELBinhLuan>(add);
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
