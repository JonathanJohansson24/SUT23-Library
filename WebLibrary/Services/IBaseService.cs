using Microsoft.Identity.Client;
using Newtonsoft.Json;
using System.Text;
using WebLibrary.Models;

namespace WebLibrary.Services
{
    public interface IBaseService : IDisposable
    {
        ResponseDTO responseModel { get; set; }

        Task<T> SendAsync<T>(ApiRequest apiRequest);
    }

    public class BaseService : IBaseService
    {
        public ResponseDTO responseModel { get; set; }

        public IHttpClientFactory _httpClient { get; set; }

        public BaseService(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient;
            responseModel = new ResponseDTO();
        }
        public async Task<T> SendAsync<T>(ApiRequest apiRequest)
        {
            try
            {
                var client = _httpClient.CreateClient("LibraryMinimalApi");
                HttpRequestMessage message = new HttpRequestMessage();
                message.Headers.Add("Accept", "application/json");
                message.RequestUri = new Uri(apiRequest.Url);
                client.DefaultRequestHeaders.Clear();

                if (apiRequest.Data != null)
                {
                    message.Content = new StringContent(JsonConvert.SerializeObject(apiRequest.Data),
                    Encoding.UTF8, "application/json");
                }

                HttpResponseMessage apiRespo = null;

                switch (apiRequest.Type)
                {
                    case StaticDetails.ApiType.GET:
                        message.Method = HttpMethod.Get;
                        break;
                    case StaticDetails.ApiType.POST:
                        message.Method = HttpMethod.Post;
                        break;
                    case StaticDetails.ApiType.PUT:
                        message.Method = HttpMethod.Put;
                        break;
                    case StaticDetails.ApiType.DELETE:
                        message.Method = HttpMethod.Delete;
                        break;
                }

                apiRespo = await client.SendAsync(message);

                var apiContent = await apiRespo.Content.ReadAsStringAsync();
                var apiResponsDto = JsonConvert.DeserializeObject<T>(apiContent);

                return apiResponsDto;
            }
            catch (Exception e)
            {
                var dto = new ResponseDTO()
                {
                    DisplayMessage = "Error",
                    ErrorMessages = new List<string> { Convert.ToString(e.Message) },
                    IsSuccess = false
                };
                var result = JsonConvert.SerializeObject(dto);
                var apiResponseDto = JsonConvert.DeserializeObject<T>(result);
                return apiResponseDto;
            }

            
        }
        public void Dispose()
        {
            GC.SuppressFinalize(true);
        }
    }
}
