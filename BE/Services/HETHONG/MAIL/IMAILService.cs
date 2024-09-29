using AutoMapper.Internal;
using MODELS.HETHONG.MAIL.Requests;

namespace BE.Services.HETHONG.MAIL
{
    public interface IMAILService
    {
        Task SendEmailAsync(PostMailRequest mailRequest);
    }
}
