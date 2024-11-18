using FE.MODELS;
using Microsoft.AspNetCore.Mvc;
using MODELS.Base;
using MODELS.COMMON;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net;
using Microsoft.Extensions.Options;

namespace FE.Helpers
{
    public class BaseController<T> : Controller
    {
        string environment = Environment.CurrentDirectory;
        private readonly IConfiguration configuration = new ConfigurationBuilder()
                                                        .SetBasePath(Directory.GetCurrentDirectory())
                                                        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                                                        .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true)
                                                        .AddEnvironmentVariables();

        // Cấu hình ConfigurationBuilder để load các file cấu hình

        public BaseController()
        {

        }

        // Function
        public ResponseData ExcuteAPI(string action, object? model, HttpAction method)
        {
            ResponseData response = new ResponseData();
            try
            {
                using (var client = new HttpClient())
                {
                    // Lấy ra địa chỉ BE
                    client.BaseAddress = new Uri(GetBEUrl());
                    // Thời gian chờ tối đa
                    client.Timeout = TimeSpan.FromMinutes(5);
                    // Set header cho request
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.User.Claims.Where(x => x.Type == "Token").FirstOrDefault().Value.ToString());

                    client.DefaultRequestHeaders.Accept.Clear();

                    Task<HttpResponseMessage> responseTask;
                    switch (method)
                    {
                        case HttpAction.Get:
                            responseTask = client.GetAsync(action);
                            break;
                        case HttpAction.Post:
                            responseTask = client.PostAsJsonAsync(action, model);
                            break;
                        case HttpAction.Put:
                            responseTask = client.PutAsJsonAsync(action, model);
                            break;
                        case HttpAction.Delete:
                            responseTask = client.DeleteAsync(action);
                            break;
                        default:
                            responseTask = client.PostAsJsonAsync(action, model);
                            break;
                    }

                    responseTask.Wait();
                    response = ExecuteAPIResponse(responseTask);
                }
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = "Lỗi hệ thống: " + ex.Message;
            }
            return response;
        }
        public ResponseData ExcuteAPIWithoutToken(string action, object? model, HttpAction method)
        {
            ResponseData response = new ResponseData();
            try
            {
                using (var client = new HttpClient())
                {
                    // Lấy ra địa chỉ BE
                    client.BaseAddress = new Uri(GetBEUrl());
                    // Thời gian chờ tối đa
                    client.Timeout = TimeSpan.FromMinutes(5);
                    // Set header cho request
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Accept.Clear();

                    Task<HttpResponseMessage> responseTask;
                    switch (method)
                    {
                        case HttpAction.Get:
                            responseTask = client.GetAsync(action);
                            break;
                        case HttpAction.Post:
                            responseTask = client.PostAsJsonAsync(action, model);
                            break;
                        case HttpAction.Put:
                            responseTask = client.PutAsJsonAsync(action, model);
                            break;
                        case HttpAction.Delete:
                            responseTask = client.DeleteAsync(action);
                            break;
                        default:
                            responseTask = client.PostAsJsonAsync(action, model);
                            break;
                    }

                    responseTask.Wait();
                    response = ExecuteAPIResponse(responseTask);
                }
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = "Lỗi hệ thống: " + ex.Message;
            }
            return response;
        }
        public string GetBEUrl()
        {
            return new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("BEUrl").Value.ToString();
        }
        public ResponseData ExecuteAPIResponse(Task<HttpResponseMessage> responseTask)
        {
            ResponseData response = new ResponseData();

            //To store result of web api response.   
            var result = responseTask.Result;

            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsStringAsync();
                readTask.Wait();

                if (readTask == null)
                {
                    response.Status = false;
                    response.Message = "Lỗi hệ thống";
                }
                else
                {
                    string json = readTask.Result;
                    var resultData = JsonConvert.DeserializeObject<MODELAPIBasic>(json);

                    response.Message = resultData.Message;
                    if (!resultData.Success || resultData.StatusCode != Convert.ToInt32(HttpStatusCode.OK))
                    {
                        response.Status = false;
                    }
                    else
                    {
                        response.Data = resultData.Result;
                    }
                }
            }
            else
            {
                response.Status = false;
                response.Message = "Lỗi hệ thống";
            }

            return response;

        }
    }
}
