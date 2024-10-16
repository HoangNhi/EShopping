using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MODELS.COMMON;
using MODELS.HETHONG.PHANQUYEN.Dtos;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;

namespace BE.Attributes
{
    public class CustomAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        private readonly Permission _permission;

        public CustomAuthorizeAttribute(Permission permission)
        {
            _permission = permission;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            try
            {
                var userId = context.HttpContext.User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Name)?.Value;

                if (string.IsNullOrEmpty(userId))
                {
                    context.Result = new UnauthorizedResult();
                    return;
                }

                // Lấy tên controller từ context
                var controllerName = context.ActionDescriptor.RouteValues["controller"];

                // Lấy ListPhanQuyen từ context
                var ListPhanQuyen = context.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "ListPhanQuyen");
                if (ListPhanQuyen == null)
                {
                    throw new Exception("Không có quyền truy cập");
                }

                var roleGroup = JsonConvert.DeserializeObject<List<MODELPhanQuyen>>(ListPhanQuyen.Value)
                                .FirstOrDefault(x => x.ControllerName == controllerName);

                if (roleGroup == null || !CheckPermission(roleGroup, _permission))
                {
                    context.Result = new ForbidResult();
                }
            }
            catch (Exception ex)
            {
                context.Result = new UnauthorizedResult();
            }

        }

        private bool CheckPermission(MODELPhanQuyen roleGroup, Permission permission)
        {
            return permission switch
            {
                Permission.Watch => roleGroup.IsXem,
                Permission.Add => roleGroup.IsThem,
                Permission.Update => roleGroup.IsCapNhat,
                Permission.Delete => roleGroup.IsXoa,
                _ => false
            };
        }
    }
}
