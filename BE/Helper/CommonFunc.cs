using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BE.Helper
{
    public static class CommonFunc
    {
        public static string GetModelStateAPI(ModelStateDictionary modelState)
        {
            var errorMessages = modelState.Values
                               .SelectMany(v => v.Errors)
                               .Select(e => e.ErrorMessage)
                               .ToList();

            return errorMessages[0];
        }
    }
}
