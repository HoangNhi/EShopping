using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MODELS.HETHONG.TAIKHOAN.Requests;

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
            ViewBag.IsError = false;
            ViewBag.Message = "";
            return View("~/Views/HETHONG/TAIKHOAN/Login.cshtml", new PostLoginRequest());
        }

        public IActionResult Register()
        {
            ViewBag.RegisterIsError = false;
            ViewBag.RegisterIsSuccess = false;
            ViewBag.Message = "";
            return View("~/Views/HETHONG/TAIKHOAN/Register.cshtml", new PostRegisterRequest());
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "TaiKhoan");
        }
    }
}
