using FE.Constants;
using FE.Helpers;
using FE.MODELS;
using Microsoft.AspNetCore.Mvc;
using MODELS.Base;
using MODELS.BASE;
using MODELS.COMMON;
using MODELS.HETHONG.NHOMQUYEN.Dtos;
using MODELS.HETHONG.NHOMQUYEN.Requests;
using Newtonsoft.Json;

namespace FE.Controllers.HETHONG.NHOMQUYEN
{
    public class NhomQuyenController : BaseController<NhomQuyenController>
    {
        public IActionResult Index()
        {
            return View("~/Views/HeThong/NhomQuyen/Index.cshtml");
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

                ResponseData response = this.ExcuteAPI(URL_API.NHOMQUYEN_GETLISTPAGING, request, HttpAction.Post);
                if (response.Status)
                {
                    result = JsonConvert.DeserializeObject<GetListPagingResponse>(response.Data.ToString());
                    result.Data = JsonConvert.DeserializeObject<List<MODELNhomQuyen>>(result.Data.ToString());
                    return PartialView("~/Views/HeThong/NhomQuyen/PartialViewDanhSach.cshtml" , result);
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
                NhomQuyenRequest obj = new NhomQuyenRequest();

                ResponseData response = this.ExcuteAPI(URL_API.NHOMQUYEN_GETBYPOST, new { Id = Guid.Empty }, HttpAction.Post);

                if (response.Status)
                {
                    obj = JsonConvert.DeserializeObject<NhomQuyenRequest>(response.Data.ToString());
                }

                return PartialView("~/Views/HeThong/NhomQuyen/PopupDetail.cshtml", obj);
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
                NhomQuyenRequest obj = new NhomQuyenRequest();

                ResponseData response = this.ExcuteAPI(URL_API.NHOMQUYEN_GETBYPOST, new { Id = id }, HttpAction.Post);

                if (response.Status)
                {
                    obj = JsonConvert.DeserializeObject<NhomQuyenRequest>(response.Data.ToString());
                }

                return PartialView("~/Views/HeThong/NhomQuyen/PartialViewDanhSach.cshtml", obj);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return PartialView("~/Views/Shared/ErrorPartial.cshtml");
            }
        }

        [HttpPost]
        public JsonResult Post(NhomQuyenRequest request)
        {
            try
            {

                if (request != null && ModelState.IsValid)
                {
                    ResponseData response;
                    if (request.IsEdit)
                    {
                        response = this.ExcuteAPI(URL_API.NHOMQUYEN_UPDATE, request, HttpAction.Post);
                    }
                    else
                    {
                        response = this.ExcuteAPI(URL_API.NHOMQUYEN_CREATE, request, HttpAction.Post);
                    }


                    if (!response.Status)
                    {
                        return Json(new { IsSuccess = false, Message = response.Message, Data = ""});
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
                ResponseData response = this.ExcuteAPI(URL_API.NHOMQUYEN_DELETE, request, HttpAction.Post);
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
