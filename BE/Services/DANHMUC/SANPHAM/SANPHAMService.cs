using AutoMapper;
using ENTITIES.DbContent;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using MODELS.Base;
using MODELS.BASE;
using MODELS.DANHMUC.NHANHIEU.Dtos;
using MODELS.DANHMUC.NHANHIEU.Requests;
using MODELS.DANHMUC.SANPHAM.Dtos;
using MODELS.DANHMUC.SANPHAM.Requests;
using MODELS.HETHONG.LOG;
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
            var NhatKiDTO = new NhatKiDTO();
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
                add.Id = Guid.NewGuid();
                add.DateCreate = DateTime.UtcNow;

                //Lưu vào nhật kí
                NhatKiDTO.Name = "Sản phẩm";
                NhatKiDTO.Id = Guid.NewGuid();
                NhatKiDTO.Event = "Thêm";
                NhatKiDTO.Date = DateTime.Now;
                NhatKiDTO.UserId = Guid.Parse(userId);
                NhatKiDTO.TargetId = add.Id;
                _context.NhatKis.Add(_mapper.Map<NhatKi>(NhatKiDTO));
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
            var userId = _contextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Name)?.Value;
            var NhatKiDTO = new NhatKiDTO();
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

        public BaseResponse<GetListPagingResponse> GetCustom(int PageIndex, int RowPerPage, string? TheLoaiId, Guid? NhanHieuId, bool? IsNew, bool? IsBestSelling, bool? IsSale)
        {
            var res = new BaseResponse<GetListPagingResponse>();
            try
            {
                var data = new List<MODELSanPham>();
                var query = _context.SanPhams.AsQueryable();
                if(!string.IsNullOrEmpty(TheLoaiId))
                {
                    query = query.Where(i => i.TheLoaiId == TheLoaiId);
                }
                if(NhanHieuId != null)
                {
                    query = query.Where(i => i.NhanHieuId == NhanHieuId);
                }
                if (IsNew == true) 
                {
                    query = query.Where(i => i.IsNew == IsNew);
                }
                if(IsBestSelling == true)
                {
                    query = query.Where(i => i.IsBestSelling == IsBestSelling);
                }
                if(IsSale == true)
                {
                    query = query.Where(i => i.IsSale == IsSale);
                }
                var result = query.Skip((PageIndex -  1) * RowPerPage).Take(RowPerPage).ToList();
                data = _mapper.Map<List<MODELSanPham>>(result);
                var page = new GetListPagingResponse();
                page.PageIndex = PageIndex;
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

        public BaseResponse<GetListPagingResponse> GetListPaging(GetListPagingRequest request)
        {
            var res = new BaseResponse<GetListPagingResponse>();
            try
            {
                var data = new List<MODELSanPham>();
                if (!string.IsNullOrEmpty(request.TextSearch))
                {
                    var result = _context.SanPhams.Where(x => x.Name == request.TextSearch && x.Status != -1).Skip((request.PageIndex - 1) * request.RowsPerPage).Take(request.RowsPerPage).ToList();
                    data = _mapper.Map<List<MODELSanPham>>(result);
                }
                else
                {
                    var result = _context.SanPhams.Where(x => x.Status != -1).Skip((request.PageIndex - 1) * request.RowsPerPage).Take(request.RowsPerPage).ToList();
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
            var userId = _contextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Name)?.Value;
            var NhatKiDTO = new NhatKiDTO();
            var response = new BaseResponse<MODELSanPham>();
            try
            {
                var add = _mapper.Map<SanPham>(request);
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
