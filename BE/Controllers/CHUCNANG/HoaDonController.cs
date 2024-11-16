﻿using BE.Helper;
using BE.Services.CHUCNANG.HOADON;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MODELS.BASE;
using MODELS.CHUCNANG.HOADON.Requests;

namespace BE.Controllers.CHUCNANG
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class HoaDonController : ControllerBase
    {
        private readonly IHOADONService _service;

        public HoaDonController(IHOADONService service) 
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
        public IActionResult Create(HoaDonRequests request)
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
    }
}