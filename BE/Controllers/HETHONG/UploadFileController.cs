using BE.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace BE.Controllers.HETHONG
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class UploadFileController : ControllerBase
    {
        IWebHostEnvironment _webHostEnvironment;

        public UploadFileController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpPost]
        [RequestSizeLimit(100_000_000)]
        public async Task<IActionResult> Post(List<IFormFile> files, [FromForm] string FolderName)
        {
            try
            {
                var folderPath = Path.Combine(_webHostEnvironment.WebRootPath, "Files/Temp/UploadFile/" + FolderName);
                if (!Directory.Exists(folderPath))
                    Directory.CreateDirectory(folderPath);

                foreach (var file in files)
                {
                    if (file.Length > 0)
                    {
                        using (var stream = new FileStream(folderPath + "/" + file.FileName, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }
                    }
                }

                return Ok(new ApiResponse(true, 200, null));
            }

            catch (Exception ex)
            {
                return Ok(new ApiResponse(false, StatusCodes.Status500InternalServerError, ex.Message));
            }
        }
    }
}
