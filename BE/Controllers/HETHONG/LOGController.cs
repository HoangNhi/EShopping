using BE.Helper;
using BE.Services.HETHONG.LOG;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MODELS.BASE;

namespace BE.Controllers.HETHONG
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class LOGController : ControllerBase
    {
        private readonly ILOGService _service;

        public LOGController(ILOGService service) 
        {
            _service = service;
        }
        [HttpPost]
        public IActionResult GetListPaging(GetListPagingRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    throw new Exception(MODELS.COMMON.CommonFunc.GetModelStateAPI(ModelState));
                }
                var result = _service.GetAllListPaging(request);
                if (result.Error)
                {
                    throw new Exception(result.Message);
                }
                else
                {
                    return Ok(new ApiOkResponse(result.Data));
                }
            }
            catch (Exception ex)
            {
                return Ok(new ApiResponse(false, 500, ex.Message));
            }
        }
    }
}
