using FE.Constants;
using FE.MODELS;
using FE.Services.ConsumeAPI;
using Microsoft.AspNetCore.Mvc;
using MODELS.Base;
using MODELS.BASE;
using MODELS.COMMON;
using MODELS.DANHMUC.THELOAI.Dtos;
using MODELS.DANHMUC.THELOAI.Requests;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace FE.Controllers.DANHMUC.THELOAI
{
    public class TheLoaiController : Controller
    {
        private readonly IConsumeAPIService _consumeAPI;

        public TheLoaiController(IConsumeAPIService consumeAPI)
        {
            _consumeAPI = consumeAPI;
        }

        public IActionResult Index()
        {
            return View("~/Views/DanhMuc/TheLoai/Index.cshtml");
        }

        public IActionResult GetListPaging(GetListPagingRequest request)
        {
            try
            {
                var result = new GetListPagingResponse();

                GetListPagingRequest param = new GetListPagingRequest();
                param.PageIndex = request.PageIndex - 1;
                param.RowsPerPage = request.RowsPerPage;
                param.TextSearch = request.TextSearch == null ? string.Empty : request.TextSearch.Trim();

                ResponseData response = _consumeAPI.ExcuteAPI(URL_API.THELOAI_GETLISTPAGING, request, HttpAction.Post);
                if (response.Status)
                {
                    result = JsonConvert.DeserializeObject<GetListPagingResponse>(response.Data.ToString());
                    result.Data = JsonConvert.DeserializeObject<List<MODELTheLoai>>(result.Data.ToString());
                    return PartialView("~/Views/DanhMuc/TheLoai/PartialViewDanhSach.cshtml", result);
                }
                else
                {
                    throw new Exception(response.Message);
                }
            }
            catch (Exception ex)
            {
                string message = "Lỗi lấy danh sách: " + ex.Message;
                return Json(new { IsSuccess = false, Message = message, Data = "" });
            }
        }

        public ActionResult ShowInsertPopup()
        {
            try
            {
                TheLoaiRequest obj = new TheLoaiRequest();

                ResponseData response = _consumeAPI.ExcuteAPI(URL_API.THELOAI_GETBYPOST, new { Id = Guid.Empty }, HttpAction.Post);

                if (response.Status)
                {
                    obj = JsonConvert.DeserializeObject<TheLoaiRequest>(response.Data.ToString());
                }

                ViewBag.BeUrl = _consumeAPI.GetBEUrl();
                return PartialView("~/Views/DanhMuc/TheLoai/PopupDetail.cshtml", obj);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return PartialView("~/Views/Shared/ErrorPartial.cshtml");
            }
        }

        public ActionResult ShowUpdatePopup(Guid id)
        {
            try
            {
                TheLoaiRequest obj = new TheLoaiRequest();

                ResponseData response = _consumeAPI.ExcuteAPI(URL_API.THELOAI_GETBYPOST, new { Id = id.ToString() }, HttpAction.Post);

                if (response.Status)
                {
                    obj = JsonConvert.DeserializeObject<TheLoaiRequest>(response.Data.ToString());
                }

                ViewBag.BeUrl = _consumeAPI.GetBEUrl();
                return PartialView("~/Views/DanhMuc/TheLoai/PopupDetail.cshtml", obj);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return PartialView("~/Views/Shared/ErrorPartial.cshtml");
            }
        }

        [HttpPost]
        public JsonResult Post(TheLoaiRequest request)
        {
            try
            {

                if (request != null && ModelState.IsValid)
                {
                    ResponseData response;
                    if (request.IsEdit)
                    {
                        response = _consumeAPI.ExcuteAPI(URL_API.THELOAI_UPDATE, request, HttpAction.Post);
                    }
                    else
                    {
                        response = _consumeAPI.ExcuteAPI(URL_API.THELOAI_CREATE, request, HttpAction.Post);
                    }


                    if (!response.Status)
                    {
                        return Json(new { IsSuccess = false, Message = response.Message, Data = "" });
                    }
                }
                else
                {
                    return Json(new { IsSuccess = false, Message = CommonFunc.GetModelState(this.ModelState), Data = "" });
                }
                return Json(new { IsSuccess = true, Message = "", Data = request.IsEdit });
            }
            catch (Exception ex)
            {
                string message = "Lỗi sửa thông tin: " + ex.Message;
                return Json(new { IsSuccess = false, Message = message, Data = "" });
            }
        }

        [HttpPost]
        public JsonResult Delete(DeleteListRequest request)
        {
            try
            {
                ResponseData response = _consumeAPI.ExcuteAPI(URL_API.THELOAI_DELETE, request, HttpAction.Post);
                if (!response.Status)
                {
                    return Json(new { IsSuccess = false, Message = response.Message, Data = "" });
                }
                return Json(new { IsSuccess = true, Message = "", Data = "" });
            }
            catch (Exception ex)
            {
                string message = "Lỗi xóa thông tin: " + ex.Message;
                return Json(new { IsSuccess = false, Message = message, Data = "" });
            }
        }
    }
}
