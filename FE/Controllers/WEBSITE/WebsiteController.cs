using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FE.Controllers.Website
{
    [AllowAnonymous]
    public class WebsiteController : Controller
    {
        public IActionResult Index()
        {
            return View("~/Views/Website/Index.cshtml");
        }
    }
}
