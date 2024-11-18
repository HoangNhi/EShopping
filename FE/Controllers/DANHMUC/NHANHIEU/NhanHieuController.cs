using FE.Constants;
using FE.MODELS;
using FE.Services.ConsumeAPI;
using Microsoft.AspNetCore.Mvc;
using MODELS.Base;
using MODELS.BASE;
using MODELS.COMMON;
using MODELS.DANHMUC.NHANHIEU.Dtos;
using MODELS.DANHMUC.NHANHIEU.Requests;
using Newtonsoft.Json;

namespace FE.Controllers.DANHMUC.NHANHIEU
{
    public class NhanHieuController : Controller
    {
        private readonly IConsumeAPIService _consumeAPI;

        public NhanHieuController(IConsumeAPIService consumeAPI)
        {
            _consumeAPI = consumeAPI;
        }

        public IActionResult Index()
        {
            return View("~/Views/DanhMuc/NhanHieu/Index.cshtml");
        }

        public IActionResult GetListPaging(GetListPagingRequest request)
        {
            try
            {
                var result = new GetListPagingResponse();

                GetListPagingRequest param = new GetListPagingRequest();
                param.PageIndex = request.PageIndex;
                param.RowsPerPage = request.RowsPerPage;
                param.TextSearch = request.TextSearch == null ? string.Empty : request.TextSearch.Trim();

                ResponseData response = _consumeAPI.ExcuteAPI(URL_API.NHANHIEU_GETLISTPAGING, param, HttpAction.Post);
                if (response.Status)
                {
                    result = JsonConvert.DeserializeObject<GetListPagingResponse>(response.Data.ToString());
                    result.Data = JsonConvert.DeserializeObject<List<MODELNhanHieu>>(result.Data.ToString());
                    return PartialView("~/Views/DanhMuc/NhanHieu/PartialViewDanhSach.cshtml", result);
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
                NhanHieuRequests obj = new NhanHieuRequests();

                ResponseData response = _consumeAPI.ExcuteAPI(URL_API.NHANHIEU_GETBYPOST, new { Id = Guid.Empty }, HttpAction.Post);

                if (response.Status)
                {
                    obj = JsonConvert.DeserializeObject<NhanHieuRequests>(response.Data.ToString());
                }

                ViewBag.BeUrl = _consumeAPI.GetBEUrl();
                return PartialView("~/Views/DanhMuc/NhanHieu/PopupDetail.cshtml", obj);
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
                NhanHieuRequests obj = new NhanHieuRequests();

                ResponseData response = _consumeAPI.ExcuteAPI(URL_API.NHANHIEU_GETBYPOST, new { Id = id.ToString() }, HttpAction.Post);

                if (response.Status)
                {
                    obj = JsonConvert.DeserializeObject<NhanHieuRequests>(response.Data.ToString());
                }

                ViewBag.BeUrl = _consumeAPI.GetBEUrl();
                return PartialView("~/Views/DanhMuc/NhanHieu/PopupDetail.cshtml", obj);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return PartialView("~/Views/Shared/ErrorPartial.cshtml");
            }
        }

        [HttpPost]
        public JsonResult Post(NhanHieuRequests request)
        {
            try
            {

                if (request != null && ModelState.IsValid)
                {
                    ResponseData response;
                    if (request.IsEdit)
                    {
                        response = _consumeAPI.ExcuteAPI(URL_API.NHANHIEU_UPDATE, request, HttpAction.Post);
                    }
                    else
                    {
                        response = _consumeAPI.ExcuteAPI(URL_API.NHANHIEU_CREATE, request, HttpAction.Post);
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
                ResponseData response = _consumeAPI.ExcuteAPI(URL_API.NHANHIEU_DELETE, request, HttpAction.Post);
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
