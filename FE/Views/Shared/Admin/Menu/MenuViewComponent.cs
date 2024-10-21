using Microsoft.AspNetCore.Mvc;
using MODELS.HETHONG.ROLE.Dtos;
using MODELS.HETHONG.TAIKHOAN.Dtos;
using Newtonsoft.Json;
using System.Security.Claims;

namespace FE.Views.Shared.Admin.Menu
{
    public class MenuViewComponent : ViewComponent
    {
        IHttpContextAccessor _contextAccessor;

        public MenuViewComponent(IHttpContextAccessor contextAccessor)
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
                    case "Role": { model.Role = JsonConvert.DeserializeObject<MODELRole>(claim.Value); }; break;
                }
            }
            ViewBag.UserInfo = model;
            return View("~/Views/Shared/Admin/Menu/Default.cshtml");
        }
    }
}
