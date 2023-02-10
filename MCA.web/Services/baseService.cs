using MSA.web;
using MSA.web.Models;
using MSA.web.Services.IServices;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace MSA.web.Services
{
    public class BaseService : IBaseService
    {
        public ResponseDto ResponseModel { get; set; }
        public IHttpClientFactory HttpClientFactory { get; set; }
        //ResponseDto IBaseService.ResponseModel { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public BaseService(IHttpClientFactory httpClientFactory)
        {
            ResponseModel = new ResponseDto();
            HttpClientFactory = httpClientFactory;
        }

        public async Task<T> SendAsync<T>(ApiRequest apiRequest)
        {
            try
            {
                var client = HttpClientFactory.CreateClient("MangoAPI");
                HttpRequestMessage message = new HttpRequestMessage();
                //message.Headers.Add("Content-Type", "application/json");
                message.Headers.Add("Accept", "application/json");
#pragma warning disable CS8604 // Possible null reference argument.
                message.RequestUri = new Uri(apiRequest.Url);
#pragma warning restore CS8604 // Possible null reference argument.
                client.DefaultRequestHeaders.Clear();
                if (apiRequest.Data != null)
                {
                    message.Content = new StringContent(JsonConvert.SerializeObject(apiRequest.Data),
                        Encoding.UTF8, "application/json");

                }

                if (!string.IsNullOrEmpty(apiRequest.AccessToken))
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiRequest.AccessToken);
                }

                HttpResponseMessage apiResponse = null;
                switch (apiRequest.ApiType)
                {
                    case SD.ApiType.POST:
                        message.Method = HttpMethod.Post;
                        break;
                    case SD.ApiType.PUT:
                        message.Method = HttpMethod.Put;
                        break;
                    case SD.ApiType.DELETE:
                        message.Method = HttpMethod.Delete;
                        break;
                    default:
                        message.Method = HttpMethod.Get;
                        break;
                }
                apiResponse = await client.SendAsync(message);
                var apiContent = await apiResponse.Content.ReadAsStringAsync();
                var apiResponseDto = JsonConvert.DeserializeObject<T>(apiContent);
                return apiResponseDto;
            }
            catch (Exception e)
            {
                var dto = new ResponseDto
                {
                    Message = "Error",
                    Errors = new List<string> { Convert.ToString(e.Message) },
                    Success = false
                };
                var res = JsonConvert.SerializeObject(dto);
                var apiResponseDto = JsonConvert.DeserializeObject<T>(res);
                return apiResponseDto;
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(true);
        }
    }
}
