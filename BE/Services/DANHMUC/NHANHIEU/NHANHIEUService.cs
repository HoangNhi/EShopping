using AutoMapper;
using ENTITIES.DbContent;
using Microsoft.AspNetCore.Hosting;
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
        private readonly IWebHostEnvironment _webHostEnvironment;
        public NHANHIEUService(EShoppingContext context, IMapper mapper, IHttpContextAccessor contextAccessor, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _mapper = mapper;
            _contextAccessor = contextAccessor;
            _webHostEnvironment = webHostEnvironment;
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
                string _pathAnhBia = UploadAnhBia(request.Id.ToString(), add.ImageUrl);
                add.ImageUrl = _pathAnhBia == "" ? add.ImageUrl : _pathAnhBia;

                add.Id = Guid.NewGuid();
                add.DateCreate = DateTime.Now;
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
                        var nhatky = _mapper.Map<NhatKi>(log);
                        nhatky.User = null;
                        _context.NhatKis.Add(nhatky);
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
                    res.Message = "Không tìm thấy thông tin";
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
                int totalData = 0;
                if (!string.IsNullOrEmpty(request.TextSearch) && !string.IsNullOrWhiteSpace(request.TextSearch))
                {
                    var result = _context.NhanHieus.Where(x => x.Name.ToLower().Contains(request.TextSearch.Trim().ToLower()) && x.Status != -1).OrderByDescending(x => x.DateCreate).Skip((request.PageIndex - 1) * request.RowsPerPage).Take(request.RowsPerPage).ToList();
                    data = _mapper.Map<List<MODELNhanHieu>>(result);
                    totalData = _context.NhanHieus.Where(x => x.Name.ToLower().Contains(request.TextSearch.Trim().ToLower()) && x.Status != -1).Count();
                }
                else
                {
                    var result = _context.NhanHieus.Where(x => x.Status != -1).OrderByDescending(x => x.DateCreate).Skip((request.PageIndex - 1) * request.RowsPerPage).Take(request.RowsPerPage).ToList();
                    data = _mapper.Map<List<MODELNhanHieu>>(result);
                    totalData = _context.NhanHieus.Where(x => x.Status != -1).Count();
                }
                var page = new GetListPagingResponse();
                page.PageIndex = request.PageIndex;
                page.TotalRow = totalData;
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
                var checkData = _context.NhanHieus.Where(
                    x => x.Name == request.Name
                    && x.Status != -1
                    && x.Id != request.Id
                ).ToList();

                if (checkData.Count > 0)
                    throw new Exception("Tên nhãn hiệu đã tồn tại");

                var update = _mapper.Map<NhanHieu>(request);
                update.DateCreate = DateTime.Now;
                string _pathAnhBia = UploadAnhBia(request.Id.ToString(), update.ImageUrl);
                update.ImageUrl = _pathAnhBia == "" ? update.ImageUrl : _pathAnhBia;

                //Lưu vào nhật kí
                log.Name = "Nhãn hiệu";
                log.Id = Guid.NewGuid();
                log.Event = "Cập nhật";
                log.Date = DateTime.UtcNow;
                log.UserId = Guid.Parse(userId);
                log.TargetId = update.Id;
                var addNhatKi = _mapper.Map<NhatKi>(log);
                addNhatKi.User = null;
                _context.NhatKis.Add(addNhatKi);
                // Lưu vào Database
                _context.NhanHieus.Update(update);
                _context.SaveChanges();
                response.Data = _mapper.Map<MODELNhanHieu>(update);
            }
            catch (Exception ex)
            {
                response.Error = true;
                response.Message = ex.Message;
            }
            // Trả về dữ liệu

            return response;
        }

        // Update 
        private string UploadAnhBia(string folderUpload, string oldImage)
        {
            string path = "";
            string folderUploadPath = Path.Combine(_webHostEnvironment.WebRootPath, "Files\\Temp\\UploadFile\\" + folderUpload);
            if (Directory.Exists(folderUploadPath))
            {
                string[] arrFiles = Directory.GetFiles(folderUploadPath);
                string[] imageExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".tiff", ".svg" };
                List<string> imgFiles = new List<string>();
                foreach (var file in arrFiles)
                {
                    string fileExtension = Path.GetExtension(file);

                    if (imageExtensions.Contains(fileExtension.ToLower()))
                    {
                        imgFiles.Add(file);
                    }
                }
                if (imgFiles.Count() > 0) //có đính kèm
                {
                    FileInfo info = new FileInfo(imgFiles[0]);
                    string fileName = Guid.NewGuid().ToString() + info.Extension;
                    string avataPath = Path.Combine(_webHostEnvironment.WebRootPath, "Files\\NhanHieu");
                    //Kiểm tra nếu thư mục chưa tồn tại thì tạo mới.
                    if (!Directory.Exists(avataPath))
                    {
                        Directory.CreateDirectory(avataPath);
                    }

                    //Xóa ảnh cũ nếu tồn tại
                    if (File.Exists(_webHostEnvironment.WebRootPath + "\\" + oldImage))
                    {
                        File.Delete(_webHostEnvironment.WebRootPath + "\\" + oldImage);
                    }

                    //Copy ảnh mới
                    File.Move(info.FullName, avataPath + "\\" + fileName, true);
                    path = "Files\\NhanHieu\\" + fileName;
                }
            }

            return path;
        }
    }
}
