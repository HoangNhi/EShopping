using AutoMapper;
using BE.Helper;
using ENTITIES.DbContent;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.SqlClient;
using MODELS.Base;
using MODELS.BASE;
using MODELS.DANHMUC.THELOAI.Dtos;
using MODELS.DANHMUC.THELOAI.Requests;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace BE.Services.DANHMUC.THELOAI
{
    public class THELOAIService : ITHELOAIService
    {
        private readonly EShoppingContext _context;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public THELOAIService(EShoppingContext context, IMapper mapper, IHttpContextAccessor contextAccessor, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _mapper = mapper;
            _contextAccessor = contextAccessor;
            _webHostEnvironment = webHostEnvironment;
        }

        public BaseResponse<GetListPagingResponse> GetListPaging(GetListPagingRequest request)
        {
            var response = new BaseResponse<GetListPagingResponse>();
            try
            {
                SqlParameter iTotalRow = new SqlParameter()
                {
                    ParameterName = "@oTotalRow",
                    SqlDbType = System.Data.SqlDbType.BigInt,
                    Direction = System.Data.ParameterDirection.Output
                };

                var parameters = new[]
                {
                    new SqlParameter("@iPageIndex", request.PageIndex),
                    new SqlParameter("@iRowsPerPage", request.RowsPerPage),
                    new SqlParameter("@iTextSearch", request.TextSearch),
                    iTotalRow
                };

                var result = _context.ExcuteStoredProcedure<MODELTheLoai>("sp_DANHMUC_THELOAI_GetListPaging", parameters).ToList();
                GetListPagingResponse resposeData = new GetListPagingResponse();
                resposeData.PageIndex = request.PageIndex;
                resposeData.Data = result;
                resposeData.TotalRow = Convert.ToInt32(iTotalRow.Value);
                response.Data = resposeData;
            }
            catch (Exception ex)
            {
                response.Error = true;
                response.Message = ex.Message;
            }
            return response;
        }
        public BaseResponse<MODELTheLoai> GetById(GetByIdRequest request)
        {
            var response = new BaseResponse<MODELTheLoai>();
            try
            {
                var result = new MODELTheLoai();
                var data = _context.TheLoais.Find(request.Id);
                if (data == null)
                    throw new Exception("Không tìm thấy thông tin");
                else
                {
                    result = _mapper.Map<MODELTheLoai>(data);
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
        public BaseResponse<TheLoaiRequest> GetByPost(GetByIdRequest request)
        {
            var response = new BaseResponse<TheLoaiRequest>();
            try
            {
                var result = new TheLoaiRequest();
                var data = _context.TheLoais.Find(request.Id.ToString());
                if (data == null)
                {
                    result.Id = Guid.NewGuid().ToString();
                    result.IsEdit = false;
                }
                else
                {
                    result = _mapper.Map<TheLoaiRequest>(data);
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
        public BaseResponse<MODELTheLoai> Create(TheLoaiRequest request)
        {
            var response = new BaseResponse<MODELTheLoai>();
            try
            {
                var checkData = _context.TheLoais.Where(
                    x => x.Name == request.Name
                    && x.Status != -1
                ).ToList();

                if (checkData.Count > 0)
                    throw new Exception("Tên thể loại đã tồn tại");

                var add = _mapper.Map<TheLoai>(request);
                add.Id = request.Id == Guid.Empty.ToString() ? Guid.NewGuid().ToString() : request.Id;

                string _pathAnhBia = UploadAnhBia(request.Id, add.ImageUrl);
                add.ImageUrl = _pathAnhBia == "" ? add.ImageUrl : _pathAnhBia;

                add.DateCreate = DateTime.Now;

                // Lưu vào Database
                _context.TheLoais.Add(add);
                _context.SaveChanges();

                // Trả về dữ liệu
                response.Data = _mapper.Map<MODELTheLoai>(add);
            }
            catch (Exception ex)
            {
                response.Error = true;
                response.Message = ex.Message;
            }
            return response;
        }
        public BaseResponse<MODELTheLoai> Update(TheLoaiRequest request)
        {
            var response = new BaseResponse<MODELTheLoai>();
            try
            {
                var checkData = _context.TheLoais.Where(
                    x => x.Name == request.Name
                    && x.Status != -1
                    && x.Id != request.Id
                ).ToList();

                if (checkData.Count > 0)
                    throw new Exception("Tên thể loại đã tồn tại");

                var update = _mapper.Map<TheLoai>(request);
                if (update != null)
                {
                    string _pathAnhBia = UploadAnhBia(request.Id, update.ImageUrl);
                    update.ImageUrl = _pathAnhBia == "" ? update.ImageUrl : _pathAnhBia;
                    update.DateCreate = DateTime.Now;

                    // Lưu vào Database
                    _context.TheLoais.Update(update);
                    _context.SaveChanges();
                }
                else
                {
                    throw new Exception("Không tìm thấy dữ liệu");
                }

                // Trả về dữ liệu
                response.Data = _mapper.Map<MODELTheLoai>(update);
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
                    var delete = _context.TheLoais.Find(id.ToString());
                    if (delete != null)
                    {
                        delete.Status = -1;
                        delete.DateCreate = DateTime.Now;

                        _context.TheLoais.Update(delete);
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
                    string avataPath = Path.Combine(_webHostEnvironment.WebRootPath, "Files\\TheLoai");
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
                    path = "Files\\TheLoai\\" + fileName;
                }
            }

            return path;
        }
    }
}
