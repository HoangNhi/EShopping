using FE.Constants;
using FE.MODELS;
using FE.Services.ConsumeAPI;
using Microsoft.AspNetCore.Mvc;

namespace FE.Controllers.HETHONG.Base
{
    public class UploadFileController : Controller
    {
        private readonly IConsumeAPIService _consumeAPI;

        public UploadFileController(IConsumeAPIService consumeAPI)
        {
            _consumeAPI = consumeAPI;
        }

        [HttpPost]
        public IActionResult UploadFile(IFormCollection data)
        {
            try
            {
                var multiForm = new System.Net.Http.MultipartFormDataContent();

                // add API method parameters
                foreach (var file in data.Files)
                {
                    multiForm.Add(new StreamContent(file.OpenReadStream()), "files", file.FileName);
                }

                multiForm.Add(new StringContent(data["FolderName"]), "FolderName");

                ResponseData response = _consumeAPI.PostFormDataAPI(URL_API.UPLOADFILE, multiForm);

                if (!response.Status)
                {
                    return Json(new { IsSuccess = false, Message = response.Message, Data = "" });
                }

                return Json(new { IsSuccess = true, Message = "", Data = "" });
            }
            catch (Exception ex)
            {
                string message = "Lỗi upload file: " + ex.Message;
                return Json(new { IsSuccess = false, Message = message, Data = "" });
            }
        }
    }
}
