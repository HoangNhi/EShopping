using Microsoft.AspNetCore.Http;

namespace MODELS.HETHONG.MAIL.Requests
{
    public class PostMailRequest
    {
        public string ToEmail { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public List<IFormFile> Attachments { get; set; }
    }
}
