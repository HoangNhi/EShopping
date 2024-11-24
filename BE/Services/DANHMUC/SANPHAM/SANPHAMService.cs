using AutoMapper;
using ENTITIES.DbContent;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using MODELS.Base;
using MODELS.BASE;
using MODELS.DANHMUC.ANHSAN_HAM.Dtos;
using MODELS.DANHMUC.BINHLUAN.Dtos;
using MODELS.DANHMUC.CAUHINHSANPHAM.Dtos;
using MODELS.DANHMUC.NHANHIEU.Dtos;
using MODELS.DANHMUC.NHANHIEU.Requests;
using MODELS.DANHMUC.NHOMPHANLOAI1.Dtos;
using MODELS.DANHMUC.NHOMPHANLOAI2.Dtos;
using MODELS.DANHMUC.SANPHAM.Dtos;
using MODELS.DANHMUC.SANPHAM.Requests;
using MODELS.DANHMUC.THELOAI.Dtos;
using MODELS.DANHMUC.TRALOIBINHLUAN.Dtos;
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
                add.DateCreate = DateTime.Now;

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

        public BaseResponse<GetListPagingResponse> GetCustom(SanPhamCustomRequest request)
        {
            var res = new BaseResponse<GetListPagingResponse>();
            try
            {
                var data = new List<MODELSanPham>();
                var query = _context.SanPhams.AsQueryable();
                if(!string.IsNullOrEmpty(request.TheLoaiId))
                {
                    query = query.Where(i => i.TheLoaiId == request.TheLoaiId);
                }
                if(request.NhanHieuId != null)
                {
                    query = query.Where(i => i.NhanHieuId == request.NhanHieuId);
                }
                if (request.IsNew == true) 
                {
                    query = query.Where(i => i.IsNew == request.IsNew);
                }
                if(request.IsBestSelling == true)
                {
                    query = query.Where(i => i.IsBestSelling == request.IsBestSelling);
                }
                if(request.IsSale == true)
                {
                    query = query.Where(i => i.IsSale == request.IsSale);
                }
                var result = query.Where(x => x.Status != -1).Skip((request.PageIndex -  1) * request.RowsPerPage).Take(request.RowsPerPage).ToList();
                data = _mapper.Map<List<MODELSanPham>>(result);
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

        public BaseResponse<SanPhamResponse> GetProduct(GetByIdRequest request)
        {
            var res = new BaseResponse<SanPhamResponse>();
            var result = new SanPhamResponse();
            try
            {
                var item = _context.SanPhams.Find(request.Id);
                if (item != null)
                {
                    result = _mapper.Map<SanPhamResponse>(item);
                    result.anhSanPham = _mapper.Map<List<MODELSanPhamAnh>>(_context.SanPham_Anhs.Where(x => x.SanPhamId == item.Id).ToList());
                    result.theLoai = _mapper.Map<MODELTheLoai>(_context.TheLoais.FirstOrDefault(x => x.Id == item.TheLoaiId));
                    result.nhanHieu = _mapper.Map<MODELNhanHieu>(_context.NhanHieus.FirstOrDefault(x => x.Id == item.NhanHieuId));
                    result.cauHinhSanPham = _mapper.Map<List<MODELCauHinhSanPham>>(_context.CauHinhSanPhams.Where(x => x.SanPhamId == item.Id).ToList());
                    result.nhomPhanLoai = _mapper.Map<List<NhomPhanLoai>>(_context.NhomPhanLoai1s.Where(x => x.SanPhamId == item.Id).ToList());
                    foreach(var i in result.nhomPhanLoai)
                    {
                        i.NhomPhanLoai2 = _mapper.Map<List<MODELNhomPhanLoai2>>(_context.NhomPhanLoai2s.Where(x => x.NhomPhanLoai1Id == i.Id).ToList());

                    }
                    result.BinhLuan = _mapper.Map<List<BinhLuanResponse>>(_context.BinhLuans.Where(x => x.SanPhamId == item.Id)).ToList();
                    foreach (var i in result.BinhLuan)
                    {
                        i.TraLoiBinhLuan = _mapper.Map<List<MODELTraLoiBinhLuan>>(_context.TraLoiBinhLuans.Where(x => x.BinhLuanId == i.Id).ToList());

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
                res.StatusCode=500;
            }
            return res;
        }

        public BaseResponse<SanPhamResponse> PostProduct(SanPhamRequestAll request)
        {
            var userId = _contextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Name)?.Value;
            var NhatKiDTO = new NhatKiDTO();
            var res = new BaseResponse<SanPhamResponse>();
            var result = new SanPhamResponse();
            try
            {
                var item = _mapper.Map<SanPham>(request);
                item.Id = Guid.NewGuid();
                item.DateCreate = DateTime.Now;
                _context.SanPhams.Add(item);
                result = _mapper.Map<SanPhamResponse>(request);
                result.theLoai = _mapper.Map<MODELTheLoai>(_context.TheLoais.Find(request.TheLoaiId));
                result.nhanHieu = _mapper.Map<MODELNhanHieu>(_context.NhanHieus.Find(request.NhanHieuId));
                foreach(var i in request.anhSanPham)
                {
                    var anh = _mapper.Map<SanPham_Anh>(i);
                    anh.Id = Guid.NewGuid();
                    anh.DateCreate = DateTime.Now;
                    _context.SanPham_Anhs.Add(anh);
                }   
                foreach(var c in request.cauHinhSanPham)
                {
                    var cauhinh = _mapper.Map<CauHinhSanPham>(c);
                    cauhinh.Id = Guid.NewGuid();
                    cauhinh.SanPhamId = item.Id;
                    _context.CauHinhSanPhams.Add(cauhinh);
                }
                foreach(var i in request.nhomPhanLoai)
                {
                    var npl  = _mapper.Map<NhomPhanLoai1>(i);
                    npl.Id = Guid.NewGuid();
                    npl.SanPhamId = item.Id;
                    _context.NhomPhanLoai1s.Add(npl);
                    foreach(var n in npl.NhomPhanLoai2s)
                    {
                        var npl2 = _mapper.Map<NhomPhanLoai2>(n);
                        npl2.Id = Guid.NewGuid();
                        npl2.NhomPhanLoai1Id = npl.Id;
                        _context.NhomPhanLoai2s.Add(npl2);
                    }
                }
                result.Id = item.Id;
                res.Data = result;
                NhatKiDTO.Name = "Sản phẩm";
                NhatKiDTO.Id = Guid.NewGuid();
                NhatKiDTO.Event = "Thêm";
                NhatKiDTO.Date = DateTime.Now;
                NhatKiDTO.UserId = Guid.Parse(userId);
                NhatKiDTO.TargetId = item.Id;
                _context.NhatKis.Add(_mapper.Map<NhatKi>(NhatKiDTO));
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                res.Error = true;
                res.Message = ex.Message;
                res.StatusCode = 500;
            }
            return res;
        }

        public BaseResponse<SanPhamResponse> PutProduct(SanPhamRequestAll request)
        {
            var userId = _contextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Name)?.Value;
            var NhatKiDTO = new NhatKiDTO();
            var res = new BaseResponse<SanPhamResponse>();
            var result = new SanPhamResponse();
            try
            {
                var item = _mapper.Map<SanPham>(request);
                item.DateCreate = DateTime.Now;
                _context.SanPhams.Update(item);
                result = _mapper.Map<SanPhamResponse>(request);
                result.theLoai = _mapper.Map<MODELTheLoai>(_context.TheLoais.Find(request.TheLoaiId));
                result.nhanHieu = _mapper.Map<MODELNhanHieu>(_context.NhanHieus.Find(request.NhanHieuId));
                foreach (var i in request.anhSanPham)
                {
                    var anh = _mapper.Map<SanPham_Anh>(i);
                    anh.DateCreate = DateTime.Now;
                    _context.SanPham_Anhs.Update(anh);
                }
                foreach (var c in request.cauHinhSanPham)
                {
                    var cauhinh = _mapper.Map<CauHinhSanPham>(c);
                    _context.CauHinhSanPhams.Update(cauhinh);
                }
                foreach (var i in request.nhomPhanLoai)
                {
                    var npl = _mapper.Map<NhomPhanLoai1>(i);
                    _context.NhomPhanLoai1s.Update(npl);
                    foreach (var n in npl.NhomPhanLoai2s)
                    {
                        var npl2 = _mapper.Map<NhomPhanLoai2>(n);
                        _context.NhomPhanLoai2s.Update(npl2);
                    }
                }
                result.Id = item.Id;
                res.Data = result;
                NhatKiDTO.Name = "Sản phẩm";
                NhatKiDTO.Id = Guid.NewGuid();
                NhatKiDTO.Event = "Cập nhật";
                NhatKiDTO.Date = DateTime.Now;
                NhatKiDTO.UserId = Guid.Parse(userId);
                NhatKiDTO.TargetId = item.Id;
                _context.NhatKis.Add(_mapper.Map<NhatKi>(NhatKiDTO));
                _context.SaveChanges();
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
