﻿using BE.Helper;
using BE.Services.CHUCNANG.CHITIETHOADON;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MODELS.Base;
using MODELS.CHUCNANG.CHITIETDONHANG.Requests;
using MODELS.CHUCNANG.HOADON.Requests;

namespace BE.Controllers.CHUCNANG
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class ChiTietDonHangController : ControllerBase
    {
        private readonly ICHITIETDONHANGService _service;

        public ChiTietDonHangController(ICHITIETDONHANGService service)
        {
            _service = service;
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
        public IActionResult Update(ChiTietDonHangRequests request)
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