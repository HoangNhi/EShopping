using AutoMapper;
using ENTITIES.DbContent;
using MODELS.Base;
using MODELS.BASE;
using MODELS.DANHMUC.NHANHIEU.Dtos;
using MODELS.DANHMUC.NHANHIEU.Requests;
using MODELS.DANHMUC.SANPHAM.Dtos;
using MODELS.DANHMUC.SANPHAM.Requests;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BE.Services.DANHMUC.SANPHAM
{
    public class SANPHAMService : ISANPHAMService
    {
        private readonly EShoppingContext _context;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;
        public SANPHAMService(EShoppingContext context, IMapper mapper, IHttpContextAccessor contextAccessor)
        {
            _context = context;
            _mapper = mapper;
            _contextAccessor = contextAccessor;
        }
        public BaseResponse<MODELSanPham> Create(SanPhamRequests request)
        {
            var userId = _contextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Name)?.Value;
            
            var response = new BaseResponse<MODELSanPham>();
            try
            {
                var checkData = _context.SanPhams.Where(
                    x => x.Name == request.Name
                    && x.Status != -1
                ).ToList();

                if (checkData.Count > 0)
                    throw new Exception("Sản phẩm đã tồn tại");

                var add = _mapper.Map<SanPham>(request);
                add.Id = request.Id == Guid.Empty ? Guid.NewGuid() : request.Id;
                add.DateCreate = DateTime.Now;

                //Lưu vào nhật kí
                var log = new NhatKi();
                log.Name = "Sản phẩm";
                log.Id = new Guid();
                log.Event = "Thêm";
                log.Date = DateOnly.FromDateTime(DateTime.Now);
                log.UserId = Guid.Parse(userId);
                // Lưu vào Database
                _context.SanPhams.Add(add);
                _context.SaveChanges();

                // Trả về dữ liệu
                response.Data = _mapper.Map<MODELSanPham>(add);
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
            var response = new BaseResponse<string>();
            try
            {
                foreach (var id in request.Ids)
                {
                    var delete = _context.SanPhams.Find(id);
                    if (delete != null)
                    {
                        delete.Status = -1;
                        delete.DateCreate = DateTime.Now;

                        _context.SanPhams.Update(delete);
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

        public BaseResponse<MODELSanPham> GetById(GetByIdRequest request)
        {
            var res = new BaseResponse<MODELSanPham>();
            try
            {
                var item = _context.SanPhams.Find(request.Id);
                if (item != null)
                {
                    var result = _mapper.Map<MODELSanPham>(item);
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

        public BaseResponse<SanPhamRequests> GetByPost(GetByIdRequest request)
        {
            var response = new BaseResponse<SanPhamRequests>();
            try
            {
                var result = new SanPhamRequests();
                var data = _context.SanPhams.Find(request.Id);
                if (data == null)
                {
                    result.Id = Guid.NewGuid();
                    result.IsEdit = false;
                }
                else
                {
                    result = _mapper.Map<SanPhamRequests>(data);
                    result.IsEdit = true;
                }
                response.Data = result;
            }
            catch (Exception ex)
            {
                response.Error = true;
                response.Message = ex.Message;
            }
            return response;
        }

        public BaseResponse<GetListPagingResponse> GetListPaging(GetListPagingRequest request)
        {
            var res = new BaseResponse<GetListPagingResponse>();
            try
            {
                var data = new List<MODELSanPham>();
                if (!string.IsNullOrEmpty(request.TextSearch))
                {
                    var result = _context.SanPhams.Where(x => x.Name == request.TextSearch).Skip((request.PageIndex - 1) * request.RowsPerPage).Take(request.RowsPerPage).ToList();
                    data = _mapper.Map<List<MODELSanPham>>(result);
                }
                else
                {
                    var result = _context.SanPhams.Skip((request.PageIndex - 1) * request.RowsPerPage).Take(request.RowsPerPage).ToList();
                    data = _mapper.Map<List<MODELSanPham>>(result);
                }
                var page = new GetListPagingResponse();
                page.PageIndex = request.PageIndex;
                page.TotalRow = _context.SanPhams.Count();
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

        public BaseResponse<MODELSanPham> Update(SanPhamRequests request)
        {
            var response = new BaseResponse<MODELSanPham>();
            try
            {
                var add = _mapper.Map<SanPham>(request);
                add.Id = request.Id == Guid.Empty ? Guid.NewGuid() : request.Id;
                add.DateCreate = DateTime.Now;

                // Lưu vào Database
                _context.SanPhams.Update(add);
                _context.SaveChanges();
                response.Data = _mapper.Map<MODELSanPham>(add);
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
