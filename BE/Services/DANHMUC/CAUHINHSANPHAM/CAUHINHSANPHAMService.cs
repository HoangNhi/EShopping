using AutoMapper;
using ENTITIES.DbContent;
using Microsoft.EntityFrameworkCore;
using MODELS.Base;
using MODELS.BASE;
using MODELS.DANHMUC.CAUHINHSANPHAM.Dtos;
using MODELS.DANHMUC.CAUHINHSANPHAM.Request;
using MODELS.DANHMUC.SANPHAM.Dtos;
using MODELS.DANHMUC.SANPHAM.Requests;
using MODELS.HETHONG.LOG;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;

namespace BE.Services.DANHMUC.CAUHINHSANPHAM
{
    public class CAUHINHSANPHAMService : ICAUHINHSANPHAMService
    {
        private readonly EShoppingContext _context;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;
        public CAUHINHSANPHAMService(EShoppingContext context, IMapper mapper, IHttpContextAccessor contextAccessor) 
        {
            _context = context;
            _mapper = mapper;
            _contextAccessor = contextAccessor;
        }
        public BaseResponse<List<MODELCauHinhSanPham>> Create(List<CauHinhSanPhamRequests> request)
        {
            var userId = _contextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Name)?.Value;
            var NhatKiDTO = new NhatKiDTO();
            var response = new BaseResponse<List<MODELCauHinhSanPham>>();
            try
            {
                foreach(var item in request)
                {
                    var checkData = _context.CauHinhSanPhams.Where(
                    x => x.SanPhamId == item.SanPhamId && x.Name == item.Name
                ).ToList();
                    if (checkData.Count > 0)
                        throw new Exception("Cấu hình này của Sản phẩm đã tồn tại");

                    var add = _mapper.Map<CauHinhSanPham>(item);
                    add.Id = Guid.NewGuid();
                    _context.CauHinhSanPhams.Add(add);
                }
                // Lưu vào Database          
                _context.SaveChanges();

                // Trả về dữ liệu
                response.Data = _mapper.Map<List<MODELCauHinhSanPham>>(request);
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
                    var delete = _context.CauHinhSanPhams.Find(id);
                    if (delete != null)
                    {
                        _context.CauHinhSanPhams.Remove(delete);
                       
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

        public BaseResponse<List<MODELCauHinhSanPham>> GetById(GetByIdRequest request)
        {
            var res = new BaseResponse<List<MODELCauHinhSanPham>>();
            try
            {
                var item = _context.CauHinhSanPhams.Where(x => x.SanPhamId == request.Id).ToList();
                if (item != null)
                {
                    var result = _mapper.Map<List<MODELCauHinhSanPham>>(item);
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

        public BaseResponse<CauHinhSanPhamRequests> GetByPost(GetByIdRequest request)
        {
            throw new NotImplementedException();
        }

        public BaseResponse<GetListPagingResponse> GetListPaging(GetListPagingRequest request)
        {
            var res = new BaseResponse<GetListPagingResponse>();
            try
            {
                var data = new List<MODELCauHinhSanPham>();
                    var result = _context.CauHinhSanPhams.Skip((request.PageIndex - 1) * request.RowsPerPage).Take(request.RowsPerPage).ToList();
                    data = _mapper.Map<List<MODELCauHinhSanPham>>(result);
                
                var page = new GetListPagingResponse();
                page.PageIndex = request.PageIndex;
                page.TotalRow = _context.CauHinhSanPhams.Count();
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

        public BaseResponse<List<MODELCauHinhSanPham>> Update(List<CauHinhSanPhamRequests> request)
        {
            var response = new BaseResponse<List<MODELCauHinhSanPham>>();
            try
            {      
                foreach (var item in request)
                {
                    var add = _mapper.Map<CauHinhSanPham>(item);
                    _context.CauHinhSanPhams.Update(add);
                }
                // Lưu vào Database          
                _context.SaveChanges();

                // Trả về dữ liệu
                response.Data = _mapper.Map<List<MODELCauHinhSanPham>>(request);
            }
            catch (Exception ex)
            {
                response.Error = true;
                response.Message = ex.Message;
            }
            return response;
        }
    }
}
