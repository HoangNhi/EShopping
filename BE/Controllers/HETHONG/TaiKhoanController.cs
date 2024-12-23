﻿using BE.Helper;
using BE.Services.HETHONG.TAIKHOAN;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MODELS.HETHONG.TAIKHOAN.Requests;
using MODELS.COMMON;
using MODELS.BASE;
using MODELS.Base;

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
        public async Task<IActionResult> getListPaging(GetListPagingRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Ok(new ApiResponse(false, StatusCodes.Status400BadRequest, CommonFunc.GetModelStateAPI(ModelState)));
                }
                var result = await _service.GetListPaging(request);
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
        public async Task<IActionResult> GetById(GetByIdRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Ok(new ApiResponse(false, StatusCodes.Status400BadRequest, CommonFunc.GetModelStateAPI(ModelState)));
                }
                var result = await _service.GetById(request);
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

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> GoogleRegister(GoogleRegisterRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Ok(new ApiResponse(false, StatusCodes.Status400BadRequest, CommonFunc.GetModelStateAPI(ModelState)));
                }
                var result = await _service.GoogleRegister(request);
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
        public async Task<IActionResult> ChangePassword(ChangePasswordRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Ok(new ApiResponse(false, StatusCodes.Status400BadRequest, CommonFunc.GetModelStateAPI(ModelState)));
                }
                var result = await _service.ChangePassword(request);
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

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult>Delete(GetByIdRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Ok(new ApiResponse(false, StatusCodes.Status400BadRequest, CommonFunc.GetModelStateAPI(ModelState)));
                }
                var result = await _service.Delete(request);
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
