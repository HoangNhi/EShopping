using AutoMapper;
using ENTITIES.DbContent;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using MODELS.Base;
using MODELS.BASE;
using MODELS.DANHMUC.NHANHIEU.Dtos;
using MODELS.DANHMUC.NHANHIEU.Requests;
using MODELS.DANHMUC.THELOAI.Dtos;
using MODELS.DANHMUC.THELOAI.Requests;
using MODELS.HETHONG.LOG;
using System.IdentityModel.Tokens.Jwt;

namespace BE.Services.DANHMUC.NHANHIEU
{
    public class NHANHIEUService : INHANHIEUService
    {
        private readonly EShoppingContext _context;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;
        public NHANHIEUService(EShoppingContext context, IMapper mapper, IHttpContextAccessor contextAccessor)
        {
            _context = context;
            _mapper = mapper;
            _contextAccessor = contextAccessor;
        }
    
        public BaseResponse<MODELNhanHieu> Create(NhanHieuRequests request)
        {
            var userId = _contextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Name)?.Value;
            var log = new NhatKiDTO();
            var response = new BaseResponse<MODELNhanHieu>();
            try
            {
                var checkData = _context.NhanHieus.Where(
                    x => x.Name == request.Name
                    && x.Status != -1
                ).ToList();

                if (checkData.Count > 0)
                    throw new Exception("Nhãn hiệu đã tồn tại");

                var add = _mapper.Map<NhanHieu>(request);
                add.Id = Guid.NewGuid();
                add.DateCreate = DateTime.UtcNow;
                _context.NhanHieus.Add(add);
                //Lưu vào nhật kí
                log.Name = "Nhãn hiệu";
                log.Id = Guid.NewGuid();
                log.Event = "Thêm";
                log.Date = DateTime.Now;
                log.UserId = Guid.Parse(userId);
                log.TargetId = add.Id;
                _context.NhatKis.Add(_mapper.Map<NhatKi>(log));
                //Lưu vào Database
                _context.SaveChanges();

                // Trả về dữ liệu
                response.Data = _mapper.Map<MODELNhanHieu>(add);
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
            var log = new NhatKiRequest();
            var response = new BaseResponse<string>();
            try
            {
                foreach (var id in request.Ids)
                {
                    var delete = _context.NhanHieus.Find(id);
                    if (delete != null)
                    {
                        delete.Status = -1;
                        delete.DateCreate = DateTime.Now;
                        //Lưu vào nhật kí
                        log.Name = "Nhãn hiệu";
                        log.Id = Guid.NewGuid();
                        log.Event = "Xoá";
                        log.Date = DateTime.Now;
                        log.UserId = Guid.Parse(userId);
                        log.TargetId = delete.Id;
                        _context.NhatKis.Add(_mapper.Map<NhatKi>(log));
                        _context.NhanHieus.Update(delete);
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

        public BaseResponse<MODELNhanHieu> GetById(GetByIdRequest request)
        {
            var res = new BaseResponse<MODELNhanHieu>();
            try
            {
                var item = _context.NhanHieus.Find(request.Id);
                if (item != null)
                {
                    var result = _mapper.Map<MODELNhanHieu>(item);
                    res.Data = result;
                }
                else 
                {
                    res.Error = true;
                    res.Message = "Not Found!";
                    res.StatusCode = 404;
                }
            }
            catch (Exception ex) {
                res.Error = true;
                res.Message = ex.Message;
                res.StatusCode = 500;
            }
            return res;
        }

        public BaseResponse<NhanHieuRequests> GetByPost(GetByIdRequest request)
        {
            var response = new BaseResponse<NhanHieuRequests>();
            try
            {
                var result = new NhanHieuRequests();
                var data = _context.NhanHieus.Find(request.Id);
                if (data == null)
                {
                    result.Id = Guid.NewGuid();
                    result.IsEdit = false;
                }
                else
                {
                    result = _mapper.Map<NhanHieuRequests>(data);
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
                var data = new List<MODELNhanHieu>();
                if (!string.IsNullOrEmpty(request.TextSearch))
                {
                    var result = _context.NhanHieus.Where(x => x.Name == request.TextSearch && x.Status != -1).Skip((request.PageIndex - 1) * request.RowsPerPage).Take(request.RowsPerPage).ToList();
                    data = _mapper.Map<List<MODELNhanHieu>>(result);
                }
                else
                {
                    var result = _context.NhanHieus.Where(x => x.Status != -1).Skip((request.PageIndex - 1) * request.RowsPerPage).Take(request.RowsPerPage).ToList();
                    data = _mapper.Map<List<MODELNhanHieu>>(result);
                }
                var page = new GetListPagingResponse();
                page.PageIndex = request.PageIndex;
                page.TotalRow = _context.NhanHieus.Count();
                page.Data = data;
                res.Data = page;
            }
            catch(Exception ex) 
            {
                res.Error = true;
                res.Message = ex.Message;
                res.StatusCode = 500;
            }
            return res;
        }

        public BaseResponse<MODELNhanHieu> Update(NhanHieuRequests request)
        {
            var userId = _contextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Name)?.Value;
            var log = new NhatKiRequest();
            var response = new BaseResponse<MODELNhanHieu>();
            try
            {
                    var add = _mapper.Map<NhanHieu>(request);
                    add.Id = request.Id == Guid.Empty ? Guid.NewGuid() : request.Id;
                    add.DateCreate = DateTime.Now;
                //Lưu vào nhật kí
                log.Name = "Nhãn hiệu";
                log.Id = Guid.NewGuid();
                log.Event = "Cập nhật";
                log.Date = DateTime.UtcNow;
                log.UserId = Guid.Parse(userId);
                log.TargetId = add.Id;
                _context.NhatKis.Add(_mapper.Map<NhatKi>(log));
                // Lưu vào Database
                _context.NhanHieus.Update(add);
                    _context.SaveChanges();
                    response.Data = _mapper.Map<MODELNhanHieu>(add);
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
