using FE.MODELS;
using MODELS.COMMON;

namespace FE.Services.ConsumeAPI
{
    public interface IConsumeAPIService
    {
        ResponseData ExcuteAPI(string action, object? model, HttpAction method);
        ResponseData ExcuteAPIWithoutToken(string action, object? model, HttpAction method);
        ResponseData PostFormDataAPI(string action, System.Net.Http.MultipartFormDataContent content);
        string GetBEUrl();
    }
}
