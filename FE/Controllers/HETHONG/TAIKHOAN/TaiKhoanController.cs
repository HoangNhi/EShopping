using FE.Constants;
using FE.MODELS;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MODELS.Base;
using MODELS.COMMON;
using MODELS.HETHONG.TAIKHOAN.Dtos;
using MODELS.HETHONG.TAIKHOAN.Requests;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace FE.Controllers.HETHONG.TAIKHOAN
{
    [AllowAnonymous]
    public class TaiKhoanController : Controller
    {
        public IActionResult Index()
        {
            return RedirectToAction("Login");
        }

        public IActionResult Login()
        {
            return View("~/Views/HETHONG/TAIKHOAN/Login.cshtml", new PostLoginRequest());
        }

        public IActionResult Register()
        {
            return View("~/Views/HETHONG/TAIKHOAN/Register.cshtml", new PostRegisterRequest());
        }

        public IActionResult ConfirmEmail(string request)
        {
            try
            {
                if (request != null)
                {
                    ResponseData response;
                    response = this.ExcuteAPIWithoutToken(URL_API.TAIKHOAN_CONFIRM_EMAIL + "?request=" + request, request, HttpAction.Get);
                    if (response.Status)
                    {
                        ViewBag.IsSuccess = true;
                        return View("~/Views/HETHONG/TAIKHOAN/ConfirmEmail.cshtml");
                    }
                    else
                    {
                        throw new Exception(response.Message);
                    }
                }
                else
                {
                    throw new Exception("Xác thực không thành công");
                }
            }
            catch (Exception ex)
            {
                ViewBag.IsSuccess = false;
                ViewBag.Message = "Lỗi xác thực: " + ex.Message;
                return View("~/Views/HETHONG/TAIKHOAN/ConfirmEmail.cshtml");
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromForm] PostLoginRequest request)
        {

            try
            {
                if (request != null && ModelState.IsValid)
                {
                    ResponseData response = this.ExcuteAPIWithoutToken(URL_API.TAIKHOAN_LOGIN, request, HttpAction.Post);
                    if (response.Status)
                    {
                        var taikhoanData = JsonConvert.DeserializeObject<MODELTaiKhoan>(response.Data.ToString());
                        var claims = new List<Claim>();

                        claims.Add(new Claim("Id", taikhoanData.Id.ToString()));
                        claims.Add(new Claim("Name", taikhoanData.Username));
                        claims.Add(new Claim("FullName", taikhoanData.Fullname));
                        claims.Add(new Claim("Email", taikhoanData.Email));
                        claims.Add(new Claim("ListPhanQuyen", JsonConvert.SerializeObject(taikhoanData.ListPhanQuyen)));
                        claims.Add(new Claim("Role", JsonConvert.SerializeObject(taikhoanData.Role)));
                        claims.Add(new Claim("Token", taikhoanData.Token));

                        // Create the identity from the user info
                        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        // Create the principal
                        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                        // Save token in cookie
                        await HttpContext.SignInAsync(claimsPrincipal, new AuthenticationProperties
                        {
                            IsPersistent = true, // Giữ cookie sau khi trình duyệt đóng
                            ExpiresUtc = DateTimeOffset.UtcNow.AddDays(CommonConst.ExpireTime) // Hạn sử dụng là 7 ngày
                        });

                        return Json(new { IsSuccess = true, Message = "Đăng nhập thành công", Data = "" });
                    }
                    else
                    {
                        throw new Exception(response.Message);
                    }
                }
                else
                {
                    throw new Exception(CommonFunc.GetModelState(this.ModelState));
                }
            }
            catch (Exception ex)
            {
                string message = "Lỗi đăng nhập: " + ex.Message;
                return Json(new { IsSuccess = false, Message = message, Data = "" });
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromForm] PostRegisterRequest param)
        {
            try
            {
                if (param != null && ModelState.IsValid)
                {
                    if (!CommonFunc.IsValidEmail(param.Email))
                    {
                        throw new Exception("Email chưa đúng định dạng");
                    }

                    if (param.Fullname == "" || param.Email == "" || param.Password == "" || param.RePassword == "")
                    {
                        throw new Exception("Vui lòng nhập đầy đủ thông tin đăng nhập");
                    }

                    if (param.Password != param.RePassword)
                    {
                        throw new Exception("Xác nhận mật khẩu không đúng");
                    }

                    ResponseData response;
                    response = this.ExcuteAPIWithoutToken(URL_API.TAIKHOAN_REGISTER, param, HttpAction.Post);
                    if (response.Status)
                    {
                        return Json(new { IsSuccess = true, Message = "Đăng ký thành công", Data = "" });
                    }
                    else
                    {
                        throw new Exception(response.Message);
                    }
                }
                else
                {
                    throw new Exception(CommonFunc.GetModelState(this.ModelState));
                }
            }
            catch (Exception ex)
            {
                string message = "Lỗi đăng ký: " + ex.Message;
                return Json(new { IsSuccess = false, Message = message, Data = "" });
            }
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "TaiKhoan");
        }

        // Function
        private ResponseData ExcuteAPIWithoutToken(string action, object model, HttpAction method)
        {
            ResponseData response = new ResponseData();
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(GetBEUrl());
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Accept.Clear();

                    Task<HttpResponseMessage> responseTask;
                    switch (method)
                    {
                        case HttpAction.Get:
                            responseTask = client.GetAsync(action);
                            break;
                        case HttpAction.Post:
                            responseTask = client.PostAsJsonAsync(action, model);
                            break;
                        case HttpAction.Put:
                            responseTask = client.PutAsJsonAsync(action, model);
                            break;
                        case HttpAction.Delete:
                            responseTask = client.DeleteAsync(action);
                            break;
                        default:
                            responseTask = client.PostAsJsonAsync(action, model);
                            break;
                    }

                    responseTask.Wait();
                    response = ExecuteAPIResponse(responseTask);
                }
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = "Lỗi hệ thống: " + ex.Message;
            }
            return response;
        }
        private string GetBEUrl()
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true)
                .Build();
            return configuration.GetSection("BEUrl").Value;
        }
        private ResponseData ExecuteAPIResponse(Task<HttpResponseMessage> responseTask)
        {
            ResponseData response = new ResponseData();

            //To store result of web api response.   
            var result = responseTask.Result;

            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsStringAsync();
                readTask.Wait();

                if (readTask == null)
                {
                    response.Status = false;
                    response.Message = "Lỗi hệ thống";
                }
                else
                {
                    string json = readTask.Result;
                    var resultData = JsonConvert.DeserializeObject<MODELAPIBasic>(json);

                    response.Message = resultData.Message;
                    if (!resultData.Success || resultData.StatusCode != Convert.ToInt32(HttpStatusCode.OK))
                    {
                        response.Status = false;
                    }
                    else
                    {
                        response.Data = resultData.Result;
                    }
                }
            }
            else
            {
                response.Status = false;
                response.Message = "Lỗi hệ thống";
            }

            return response;

        }
    }
}
