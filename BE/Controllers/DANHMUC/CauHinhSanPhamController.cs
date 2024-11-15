using BE.Helper;
using BE.Services.DANHMUC.CAUHINHSANPHAM;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MODELS.Base;
using MODELS.BASE;
using MODELS.DANHMUC.CAUHINHSANPHAM.Request;
using MODELS.DANHMUC.SANPHAM.Requests;

namespace BE.Controllers.DANHMUC
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class CauHinhSanPhamController : ControllerBase
    {
        private readonly ICAUHINHSANPHAMService _service;

        public CauHinhSanPhamController(ICAUHINHSANPHAMService service) 
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
                var result = _service.GetListPaging(request);
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
        [HttpPost]
        public IActionResult GetById(GetByIdRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    throw new Exception(MODELS.COMMON.CommonFunc.GetModelStateAPI(ModelState));
                }
                var result = _service.GetById(request);
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
                //SysLog             
                return Ok(new ApiResponse(false, 500, ex.Message));
            }
        }
        [HttpPost]
        public IActionResult Create(List<CauHinhSanPhamRequests> request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    throw new Exception(MODELS.COMMON.CommonFunc.GetModelStateAPI(ModelState));
                }
                var result = _service.Create(request);
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
                //SysLog             
                return Ok(new ApiResponse(false, 500, ex.Message));
            }
        }

        [HttpPost]
        public IActionResult Update(List<CauHinhSanPhamRequests> request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    throw new Exception(MODELS.COMMON.CommonFunc.GetModelStateAPI(ModelState));
                }
                var result = _service.Update(request);
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
                //SysLog             
                return Ok(new ApiResponse(false, 500, ex.Message));
            }
        }

        [HttpPost]
        public IActionResult Delete(DeleteListRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    throw new Exception(MODELS.COMMON.CommonFunc.GetModelStateAPI(ModelState));
                }
                var result = _service.Delete(request);
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
                //SysLog             
                return Ok(new ApiResponse(false, 500, ex.Message));
            }
        }
    }
}
