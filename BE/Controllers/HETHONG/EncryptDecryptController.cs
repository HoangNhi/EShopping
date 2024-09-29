using BE.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MODELS.COMMON;
using MODELS.HETHONG.TAIKHOAN.Requests;

namespace BE.Controllers.HETHONG
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class EncryptDecryptController : BaseController<EncryptDecryptController>
    {
        IConfiguration _config;
        public EncryptDecryptController(IConfiguration config)
        {
            _config = config;
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Encrypt(string request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Ok(new ApiResponse(false, StatusCodes.Status400BadRequest, CommonFunc.GetModelStateAPI(ModelState)));
                }
                var result = Encrypt_Decrypt.Encrypt(request, _config);

                return Ok(new ApiOkResponse(result));
            }
            catch (Exception ex)
            {
                return Ok(new ApiResponse(false, StatusCodes.Status500InternalServerError, ex.Message));
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Decrypt(string request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Ok(new ApiResponse(false, StatusCodes.Status400BadRequest, CommonFunc.GetModelStateAPI(ModelState)));
                }
                var result = Encrypt_Decrypt.Decrypt(request, _config);

                return Ok(new ApiOkResponse(result));
            }
            catch (Exception ex)
            {
                return Ok(new ApiResponse(false, StatusCodes.Status500InternalServerError, ex.Message));
            }
        }
    }
}
