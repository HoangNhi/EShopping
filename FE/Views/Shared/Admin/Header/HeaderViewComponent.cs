using Microsoft.AspNetCore.Mvc;
using MODELS.HETHONG.TAIKHOAN.Dtos;
using System.Security.Claims;

namespace FE.Views.Shared.Admin.Header
{
    public class HeaderViewComponent : ViewComponent
    {
        IHttpContextAccessor _contextAccessor;

        public HeaderViewComponent(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public IViewComponentResult Invoke()
        {
            MODELTaiKhoan model = new MODELTaiKhoan();

            foreach (var claim in _contextAccessor.HttpContext.User.Claims)
            {
                switch (claim.Type)
                {
                    case "FullName": { model.Fullname = claim.Value; }; break;
                }
            }
            ViewBag.UserInfo = model;
            return View("~/Views/Shared/Admin/Header/Default.cshtml");
        }
    }
}
