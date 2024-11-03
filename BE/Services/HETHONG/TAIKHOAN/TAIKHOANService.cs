using AutoMapper;
using BE.Helper;
using BE.Services.HETHONG.MAIL;
using ENTITIES.DbContent;
using MimeKit;
using MODELS.Base;
using MODELS.HETHONG.MAIL.Requests;
using MODELS.HETHONG.PHANQUYEN.Dtos;
using MODELS.HETHONG.ROLE.Dtos;
using MODELS.HETHONG.TAIKHOAN.Dtos;
using MODELS.HETHONG.TAIKHOAN.Requests;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BE.Services.HETHONG.TAIKHOAN
{
    public class TAIKHOANService : ITAIKHOANService
    {
        private readonly IMapper _mapper;
        private readonly EShoppingContext _context;
        private readonly IConfiguration _config;
        private readonly IMAILService _mailService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public TAIKHOANService(IMapper mapper, EShoppingContext context, IConfiguration config, IMAILService mailService, IHttpContextAccessor httpContextAccessor, IWebHostEnvironment webHostEnvironment)
        {
            _mapper = mapper;
            _context = context;
            _config = config;
            _mailService = mailService;
            _httpContextAccessor = httpContextAccessor;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<BaseResponse<MODELTaiKhoan>> Login(PostLoginRequest request)
        {
            var response = new BaseResponse<MODELTaiKhoan>();
            try
            {
                var data = new MODELTaiKhoan();
                var taiKhoan = _context.ApplicationUsers.FirstOrDefault(x => x.Username == request.Username);
                if (taiKhoan == null)
                {
                    throw new Exception("Tên đăng nhập hoặc mật khẩu không đúng");
                }
                else
                {
                    // Kiểm tra mật khẩu
                    var pass = Encrypt_Decrypt.EncodePassword(request.Password, taiKhoan.PasswordSalt);
                    if (!pass.Equals(taiKhoan.Password))
                    {
                        throw new Exception("Tên đăng nhập hoặc mật khẩu không đúng");
                    }

                    if (!taiKhoan.Vertify)
                    {
                        await SendEmailConfirm(taiKhoan.Email, taiKhoan.FullName, taiKhoan.Id);
                        throw new Exception("Tài khoản chưa được xác thực. Vui lòng kiểm tra email để xác thực tài khoản");
                    }

                    data = _mapper.Map<MODELTaiKhoan>(taiKhoan);
                    // Lấy role và phân quyền
                    var roles = await _context.Roles.FindAsync(data.RoleId);
                    data.Role = _mapper.Map<MODELRole>(roles);
                    var phanQuens = _context.PhanQuyens.Where(x => x.RoleId == data.RoleId).ToList();
                    data.ListPhanQuyen = _mapper.Map<List<MODELPhanQuyen>>(phanQuens);

                    var token = Encrypt_Decrypt.GenerateJwtToken(data, _config);
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
                add.DateCreate = DateTime.Now;
                add.Status = true;

                _context.ApplicationUsers.Add(add);
                _context.SaveChanges();

                // Gửi email xác thực
                await SendEmailConfirm(add.Email, add.FullName, add.Id);
            }
            catch (Exception ex)
            {
                response.Error = true;
                response.Message = ex.Message;
            }
            return response;
        }

        private async Task SendEmailConfirm(string Email, string Fullname, Guid UserId)
        {
            try
            {
                // Mã hóa UserId
                var UserIdEncode = Encrypt_Decrypt.Encrypt(UserId.ToString(), _config);

                // Url xác thực
                string callBackUrl = _config["FEUrl"] + "/taikhoan/ConfirmEmail?request=" + UserIdEncode;
                Console.WriteLine(callBackUrl);

                // Đường dẫn Template
                string templateFullPath = Path.Combine(_webHostEnvironment.WebRootPath, @"EmailTemplate//ConfirmEmail.html");
                var builder = new BodyBuilder();
                using (StreamReader SourceReader = System.IO.File.OpenText(templateFullPath))
                {
                    builder.HtmlBody = SourceReader.ReadToEnd();
                }

                // Replace các giá trị trong template
                string messageBody = builder.HtmlBody.Replace("{0}", Fullname)
                                  .Replace("{1}", callBackUrl);

                await _mailService.SendEmailAsync(new PostMailRequest
                {
                    ToEmail = Email,
                    Subject = "Xác thực tài khoản",
                    Body = messageBody
                });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<BaseResponse<MODELTaiKhoan>> ConfirmEmail(string UserIdEncode)
        {
            var response = new BaseResponse<MODELTaiKhoan>();
            try
            {
                // Giải mã UserId

                var UserId = Guid.Parse(Encrypt_Decrypt.Decrypt(UserIdEncode, _config));

                var user = await _context.ApplicationUsers.FindAsync(UserId);
                if (user == null)
                {
                    throw new Exception("Xác thực không thành công");
                }
                user.Vertify = true;
                _context.SaveChanges();
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
