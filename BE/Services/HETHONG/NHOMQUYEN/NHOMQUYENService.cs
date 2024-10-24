using AutoMapper;
using BE.Helper;
using ENTITIES.DbContent;
using Microsoft.Data.SqlClient;
using MODELS.Base;
using MODELS.BASE;
using MODELS.HETHONG.NHOMQUYEN.Dtos;
using MODELS.HETHONG.NHOMQUYEN.Requests;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace BE.Services.HETHONG.NHOMQUYEN
{
    public class NHOMQUYENService : INHOMQUYENService
    {
        private readonly EShoppingContext _context;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;

        public NHOMQUYENService(EShoppingContext context, IMapper mapper, IHttpContextAccessor contextAccessor)
        {
            _context = context;
            _mapper = mapper;
            _contextAccessor = contextAccessor;
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
                    new SqlParameter("@iTextSearch", request.TextSearch),
                    new SqlParameter("@iPageIndex", request.PageIndex),
                    new SqlParameter("@iRowsPerPage", request.RowsPerPage),
                    iTotalRow
                };

                var result = _context.ExcuteStoredProcedure<MODELNhomQuyen>("sp_HETHONG_NHOMQUYEN_GetListPaging", parameters).ToList();
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
        public BaseResponse<MODELNhomQuyen> GetById(GetByIdRequest request)
        {
            var response = new BaseResponse<MODELNhomQuyen>();
            try
            {
                var result = new MODELNhomQuyen();
                var data = _context.PHANQUYEN_NHOMQUYENs.Find(request.Id);
                if (data == null)
                    throw new Exception("Không tìm thấy thông tin");
                else
                {
                    result = _mapper.Map<MODELNhomQuyen>(data);
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
        public BaseResponse<NhomQuyenRequest> GetByPost(GetByIdRequest request)
        {
            var response = new BaseResponse<NhomQuyenRequest>();
            try
            {
                var result = new NhomQuyenRequest();
                var data = _context.PHANQUYEN_NHOMQUYENs.Find(request.Id);
                if (data == null)
                {
                    result.Id = Guid.NewGuid();
                    result.IsEdit = false;
                }
                else
                {
                    result = _mapper.Map<NhomQuyenRequest>(data);
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
        public BaseResponse<MODELNhomQuyen> Create(NhomQuyenRequest request)
        {
            var response = new BaseResponse<MODELNhomQuyen>();
            try
            {
                var checkData = _context.PHANQUYEN_NHOMQUYENs.Where(
                    x => x.TenGoi == request.TenGoi
                    && x.Status != -1
                ).ToList();

                if (checkData.Count > 0)
                    throw new Exception("Tên nhóm quyền đã tồn tại");

                var add = _mapper.Map<PHANQUYEN_NHOMQUYEN>(request);
                add.Id = request.Id == Guid.Empty ? Guid.NewGuid() : request.Id;
                add.DateCreate = DateTime.Now;
                add.CreateBy = _contextAccessor.HttpContext.User.Identity.Name;

                // Lưu vào Database
                _context.PHANQUYEN_NHOMQUYENs.Add(add);
                _context.SaveChanges();

                // Trả về dữ liệu
                response.Data = _mapper.Map<MODELNhomQuyen>(add);
            }
            catch (Exception ex)
            {
                response.Error = true;
                response.Message = ex.Message;
            }
            return response;
        }
        public BaseResponse<MODELNhomQuyen> Update(NhomQuyenRequest request)
        {
            var response = new BaseResponse<MODELNhomQuyen>();
            try
            {
                var checkData = _context.PHANQUYEN_NHOMQUYENs.Where(
                    x => x.TenGoi == request.TenGoi
                    && x.Status != -1
                    && x.Id != request.Id
                ).ToList();

                if (checkData.Count > 0)
                    throw new Exception("Tên nhóm quyền đã tồn tại");

                var update = _mapper.Map<PHANQUYEN_NHOMQUYEN>(request);
                if(update != null)
                {
                    update.DateCreate = DateTime.Now;
                    update.CreateBy = _contextAccessor.HttpContext.User.Identity.Name;

                    // Lưu vào Database
                    _context.PHANQUYEN_NHOMQUYENs.Add(update);
                    _context.SaveChanges();
                }
                else
                {
                    throw new Exception("Không tìm thấy dữ liệu");
                }

                // Trả về dữ liệu
                response.Data = _mapper.Map<MODELNhomQuyen>(update);
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
                    var delete = _context.PHANQUYEN_NHOMQUYENs.Find(id);
                    if (delete != null)
                    {
                        delete.Status = -1;
                        delete.CreateBy = _contextAccessor.HttpContext.User.Identity.Name;
                        delete.DateCreate = DateTime.Now;

                        _context.PHANQUYEN_NHOMQUYENs.Add(delete);
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
    }
}
