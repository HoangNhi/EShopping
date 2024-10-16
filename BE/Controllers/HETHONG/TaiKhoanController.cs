﻿using BE.Helper;
using BE.Services.HETHONG.TAIKHOAN;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MODELS.HETHONG.TAIKHOAN.Requests;
using MODELS.COMMON;

namespace BE.Controllers.HETHONG.TAIKHOAN
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class TaiKhoanController : BaseController<TaiKhoanController>
    {
        ITAIKHOANService _service;
        public TaiKhoanController(ITAIKHOANService service)
        {
            _service = service;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(PostLoginRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Ok(new ApiResponse(false, StatusCodes.Status400BadRequest, CommonFunc.GetModelStateAPI(ModelState)));
                }
                var result = await _service.Login(request);
                if (result.Error)
                {
                    throw new Exception(result.Message);
                }

                return Ok(new ApiOkResponse(result.Data));
            }
            catch (Exception ex)
            {
                return Ok(new ApiResponse(false, StatusCodes.Status500InternalServerError, ex.Message));
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(PostRegisterRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Ok(new ApiResponse(false, StatusCodes.Status400BadRequest, CommonFunc.GetModelStateAPI(ModelState)));
                }
                var result = await _service.Register(request);
                if (result.Error)
                {
                    throw new Exception(result.Message);
                }

                return Ok(new ApiOkResponse(result.Data));
            }
            catch (Exception ex)
            {
                return Ok(new ApiResponse(false, StatusCodes.Status500InternalServerError, ex.Message));
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Ok(new ApiResponse(false, StatusCodes.Status400BadRequest, CommonFunc.GetModelStateAPI(ModelState)));
                }
                var result = await _service.ConfirmEmail(request);
                if (result.Error)
                {
                    throw new Exception(result.Message);
                }

                return Ok(new ApiOkResponse(result.Data));
            }
            catch (Exception ex)
            {
                return Ok(new ApiResponse(false, StatusCodes.Status500InternalServerError, ex.Message));
            }
        }
    }
}
