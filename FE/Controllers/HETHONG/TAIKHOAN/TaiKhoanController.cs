using FE.Constants;
using FE.MODELS;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
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

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromForm] PostLoginRequest request)
        {

            try
            {
                if (request != null && ModelState.IsValid)
                {
                    ResponseData response = this.LoginAPI(URL_API.TAIKHOAN_LOGIN, request);
                    if (response.Status)
                    {
                        var taikhoanData = JsonConvert.DeserializeObject<MODELTaiKhoan>(response.Data.ToString());

                        var claims = new List<Claim>();

                        claims.Add(new Claim(ClaimTypes.Name, taikhoanData.Username));

                        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                        await HttpContext.SignInAsync(claimsPrincipal, 
                            new AuthenticationProperties 
                            {
                                // Lưu cookie 
                                IsPersistent = request.RememberMe,
                            }
                        );


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

                    if (param.Fullname == "" || param.PhoneNumber == "" || param.Email == "" || param.Password == "" || param.RePassword == "")
                    {
                        throw new Exception("Vui lòng nhập đầy đủ thông tin đăng nhập");
                    }

                    if (param.Password != param.RePassword)
                    {
                        throw new Exception("Nhập lại mật khẩu không đúng");
                    }

                    ResponseData response;
                    // Tên đăng nhập mặc định là email
                    param.Username = param.Email;
                    response = this.LoginAPI(URL_API.TAIKHOAN_REGISTER, param);
                    if (response.Status)
                    {
                        throw new Exception("Đăng ký tài khoản thành công");
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
                if (ex.Message == "Đăng ký tài khoản thành công")
                {
                    ViewBag.RegisterIsError = false;
                    ViewBag.RegisterIsSuccess = true;
                }
                else
                {
                    ViewBag.RegisterIsSuccess = false;
                    ViewBag.RegisterIsError = true;
                }
                ViewBag.Message = ex.Message;
                return View("~/Views/Account/Register.cshtml", param);
            }
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "TaiKhoan");
        }

        // Function
        private ResponseData LoginAPI(string action, object model)
        {
            ResponseData response = new ResponseData();
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(GetBEUrl());
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Accept.Clear();
                    var responseTask = client.PostAsJsonAsync(action, model);
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
            return new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("BEUrl").Value.ToString();
        }
        private ResponseData ExecuteAPIResponse(Task<HttpResponseMessage> responseTask)
        {
            ResponseData response = new ResponseData();

            //To store result of web api response.   
            var result = responseTask.Result;

            //CHECK 401
            //if (result.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            //{
            //    HttpContext.SignOutAsync().Wait();
            //    RedirectToAction("Index", "Login").ExecuteResultAsync(this.ControllerContext).Wait();
            //}

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
