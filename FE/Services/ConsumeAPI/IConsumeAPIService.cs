using FE.MODELS;
using MODELS.COMMON;

namespace FE.Services.ConsumeAPI
{
    public interface IConsumeAPIService
    {
        ResponseData ExcuteAPI(string action, object? model, HttpAction method);
        ResponseData ExcuteAPIWithoutToken(string action, object? model, HttpAction method);
    }
}
