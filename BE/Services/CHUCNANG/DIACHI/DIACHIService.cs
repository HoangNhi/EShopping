using AutoMapper;
using ENTITIES.DbContent;
using MODELS.Base;
using MODELS.CHUCNANG.DIACHI.Dtos;
using MODELS.CHUCNANG.DIACHI.Request;
using MODELS.DANHMUC.BINHLUAN.Dtos;

namespace BE.Services.CHUCNANG.DIACHI
{
    public class DIACHIService : IDIACHIService
    {
        private readonly EShoppingContext _context;
        private readonly IMapper _mapper;
        public DIACHIService(EShoppingContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public BaseResponse<MODELDiaChi> Create(DiaChiRequests request)
        {
            
            var response = new BaseResponse<MODELDiaChi>();
            try
            {
                var add = _mapper.Map<DiaChi>(request);
                add.Id = Guid.NewGuid();
                add.DateCreate = DateOnly.FromDateTime(DateTime.Now);
                add.Status = true;
                // Lưu vào Database
                _context.DiaChis.Add(add);
                _context.SaveChanges();

                // Trả về dữ liệu
                response.Data = _mapper.Map<MODELDiaChi>(add);
            }
            catch (Exception ex)
            {
                response.Error = true;
                response.Message = ex.Message;
            }
            return response;
        }

        public BaseResponse<string> Delete(GetByIdRequest request)
        {
            var response = new BaseResponse<string>();
            try
            {
                var delete = _context.DiaChis.Find(request.Id);
                if(delete == null)
                {
                    response.Error = true;
                    response.Message = "Not Found!";
                }
                else 
                {
                    delete.Status = false;
                    _context.DiaChis.Update(delete);
                    _context.SaveChanges();
                    response.Data = "Đã xoá" + request;
                }
                // Lưu vào Database


                // Trả về dữ liệu
            }
            catch (Exception ex)
            {
                response.Error = true;
                response.Message = ex.Message;
            }
            return response;
        }

        public BaseResponse<List<MODELDiaChi>> GetById(GetByIdRequest request)
        {
            var res = new BaseResponse<List<MODELDiaChi>>();
            try
            {
                var item = _context.DiaChis.Where(x => x.UserId == request.Id && x.Status == true).ToList();
                if (item != null)
                {
                    var result = _mapper.Map<List<MODELDiaChi>>(item);
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

        public BaseResponse<MODELDiaChi> IsDefault(GetByIdRequest request)
        {
            var response = new BaseResponse<MODELDiaChi>();
            try
            {
                //Xoá địa chỉ mặc định cũ
                var diachicu = _context.DiaChis.FirstOrDefault(x => x.IsDefault == true);
                if(diachicu != null)
                {
                    diachicu.IsDefault = false;
                    _context.DiaChis.Update(diachicu);
                }    
                var diachi = _context.DiaChis.Find(request.Id);
                if(diachi != null)
                {
                    //Đặt làm địa chỉ mặc định mới
                    diachi.IsDefault = true;
                    var user = _context.ApplicationUsers.Find(diachi.UserId);
                    user.Address = diachi.Address;
                    user.PhoneNumber = diachi.PhoneNumber;
                    _context.ApplicationUsers.Update(user);

                    // Lưu vào Database
                    _context.DiaChis.Update(diachi);
                    _context.SaveChanges();
                }
                

                // Trả về dữ liệu
                response.Data = _mapper.Map<MODELDiaChi>(diachi);
            }
            catch (Exception ex)
            {
                response.Error = true;
                response.Message = ex.Message;
            }
            return response;
        }

        public BaseResponse<MODELDiaChi> Update(DiaChiRequests request)
        {
            var response = new BaseResponse<MODELDiaChi>();
            try
            {
                var add = _mapper.Map<DiaChi>(request);
                add.DateCreate = DateOnly.FromDateTime(DateTime.Now);
                add.Status = true;
                // Lưu vào Database
                _context.DiaChis.Update(add);
                _context.SaveChanges();

                // Trả về dữ liệu
                response.Data = _mapper.Map<MODELDiaChi>(add);
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
