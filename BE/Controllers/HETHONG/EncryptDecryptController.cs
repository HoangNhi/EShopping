using BE.Attributes;
using BE.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MODELS.COMMON;
using MODELS.HETHONG.ENCRYPTDECRYPT.Reuqests;
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
        [CustomAuthorize(Permission.Add)]
        public async Task<IActionResult> Encrypt(EncryptDecryptRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Ok(new ApiResponse(false, StatusCodes.Status400BadRequest, CommonFunc.GetModelStateAPI(ModelState)));
                }
                var result = Encrypt_Decrypt.Encrypt(request.Request, _config);

                return Ok(new ApiOkResponse(result));
            }
            catch (Exception ex)
            {
                return Ok(new ApiResponse(false, StatusCodes.Status500InternalServerError, ex.Message));
            }
        }

        [HttpPost]
        [CustomAuthorize(Permission.Update)]
        public async Task<IActionResult> Decrypt(EncryptDecryptRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Ok(new ApiResponse(false, StatusCodes.Status400BadRequest, CommonFunc.GetModelStateAPI(ModelState)));
                }
                var result = Encrypt_Decrypt.Decrypt(request.Request, _config);

                return Ok(new ApiOkResponse(result));
            }
            catch (Exception ex)
            {
                return Ok(new ApiResponse(false, StatusCodes.Status500InternalServerError, ex.Message));
            }
        }
    }
}
