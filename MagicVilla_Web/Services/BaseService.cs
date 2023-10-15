using MagicVilla_Web.Models;
using MagicVilla_Web.Services.IServices;
using Newtonsoft.Json;
using System.Text;
using static MagicVilla_Web.Utils.StaticDetails;

namespace MagicVilla_Web.Services
{
    public class BaseService : IBaseService
    {
        public APIResponse responseModel { get; set; }
        public IHttpClientFactory httpClient { get; set; }

        public BaseService(IHttpClientFactory httpClient)
        {
            responseModel = new();
            this.httpClient = httpClient;
        }

        public async Task<T> SendAsync<T>(APIRequest apiRequest)
        {
            try
            {
                var client = httpClient.CreateClient("MagicAPI");
                HttpRequestMessage message = new();
                message.Headers.Add("Accept", "application/json");
                message.RequestUri = new Uri(apiRequest.Url);

                if (apiRequest.Data != null)
                {
                    message.Content = new StringContent(JsonConvert.SerializeObject(apiRequest.Data),
                        Encoding.UTF8, "application/json");
                }

                switch (apiRequest.ApiType)
                {
                    case ApiType.POST:
                        message.Method = HttpMethod.Post;
                        break;
                    case ApiType.PUT:
                        message.Method = HttpMethod.Put;
                        break;
                    case ApiType.DELETE:
                        message.Method = HttpMethod.Delete;
                        break;
                    default:
                        message.Method = HttpMethod.Get;
                        break;
                }

                HttpResponseMessage apiResponse = null;

                apiResponse = await client.SendAsync(message);

                var apiContent = await apiResponse.Content.ReadAsStringAsync();
                try
                {
                    APIResponse apiReturn = JsonConvert.DeserializeObject<APIResponse>(apiContent);

                    if ((int)apiResponse.StatusCode >= 400)
                    {
                        apiReturn.IsSuccess = false;
                        apiReturn.StatusCode = apiResponse.StatusCode;

                        var res = JsonConvert.SerializeObject(apiReturn);
                        var returnObj = JsonConvert.DeserializeObject<T>(res);

                        return returnObj;
                    }
                    else
                    {
                        var APIResponse = JsonConvert.DeserializeObject<T>(apiContent);
                        return APIResponse;
                    }
                }
                catch (Exception e)
                {
                    var apiReturn = JsonConvert.DeserializeObject<T>(apiContent);
                    return apiReturn;
                }
            }
            catch (Exception e)
            {
                var dto = new APIResponse
                {
                    ErrorMessages = new List<string>
                    {
                        Convert.ToString(e.Message)
                    },
                    IsSuccess = false
                };
                var res = JsonConvert.SerializeObject(dto);
                var apiReturn = JsonConvert.DeserializeObject<T>(res);

                return apiReturn;
            }
        }
    }
}
