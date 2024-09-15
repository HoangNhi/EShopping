using AutoMapper;
using BE.Helper;
using ENTITIES.DbContent;
using MODELS.Base;
using MODELS.HETHONG.TAIKHOAN.Dtos;
using MODELS.HETHONG.TAIKHOAN.Requests;

namespace BE.Services.HETHONG.TAIKHOAN
{
    public class TAIKHOANService : ITAIKHOANService
    {
        private readonly IMapper _mapper;
        private readonly EShoppingContext _context;
        private readonly IConfiguration _config;

        public TAIKHOANService(IMapper mapper, EShoppingContext context, IConfiguration config)
        {
            _mapper = mapper;
            _context = context;
            _config = config;
        }

        public async Task<BaseResponse<MODELTaiKhoan>> Login(PostLoginRequest request)
        {
            var response = new BaseResponse<MODELTaiKhoan>();
            try
            {
                var data = new MODELTaiKhoan();
                var taiKhoan = _context.ApplicationUsers.FirstOrDefault(x => x.Username == request.Username && !x.IsDeleted);
                if (taiKhoan == null)
                {
                    throw new Exception("Tên đăng nhập hoặc mật khẩu không đúng");
                }
                else
                {
                    if (!taiKhoan.IsActived)
                    {
                        throw new Exception("Tài khoản đang bị khóa");
                    }

                    // Kiểm tra mật khẩu
                    var pass = Encrypt_Decrypt.EncodePassword(request.Password, taiKhoan.PasswordSalt);
                    if (!pass.Equals(taiKhoan.Password))
                    {
                        throw new Exception("Tên đăng nhập hoặc mật khẩu không đúng");
                    }
                    var token = Encrypt_Decrypt.GenerateJwtToken(new MODELTaiKhoan { Id = taiKhoan.Id, Username = request.Username }, _config);
                    data = _mapper.Map<MODELTaiKhoan>(taiKhoan);
                    data.Token = token;

                    response.Data = data;
                }
            }
            catch (Exception ex)
            {
                response.Error = true;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<BaseResponse<MODELTaiKhoan>> Register(PostRegisterRequest request)
        {
            var response = new BaseResponse<MODELTaiKhoan>();
            try
            {
                var checkUsername = _context.ApplicationUsers.FirstOrDefault(x => x.Username == request.Username);
                if (checkUsername != null)
                {
                    throw new Exception("Email đã tồn tại");
                }

                var add = _mapper.Map<ApplicationUser>(request);
                var salt = Encrypt_Decrypt.GenerateSalt();
                add.PasswordSalt = salt;
                add.Password = Encrypt_Decrypt.EncodePassword(request.Password, salt);

                add.Id = Guid.NewGuid();
                // Vai trò mặc định là User
                add.RoleId = Guid.Parse("CB0A5375-10FD-4CD9-A659-00490896D6A7");

                add.NguoiTao = "Admin";
                add.NgayTao = DateTime.Now;

                _context.ApplicationUsers.Add(add);
                await _context.SaveChangesAsync();
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
