using Newtonsoft.Json;
using System.Text;
using WEB.Utilities;

namespace WEB.Services.BaseService
{
    public class BaseService : IBaseService
    {
        public APIResponse responseMoodel { get; set; }
        public IHttpClientFactory httpClient { get; set; }
        public BaseService(IHttpClientFactory httpClient)
        {
            responseMoodel = new();
            this.httpClient = httpClient;
        }
        public async Task<T> SendAsync<T>(APIRequest aPIRequest)
        {
            try
            {
                var client = httpClient.CreateClient();
                HttpRequestMessage message = new HttpRequestMessage();
                message.Headers.Add("Accept", "application/json");
                message.RequestUri = new Uri(aPIRequest.Url);
                if (aPIRequest.Data != null)
                {
                    message.Content = new StringContent(JsonConvert.SerializeObject(aPIRequest.Data),
                        Encoding.UTF8, "application/json");
                }
                switch (aPIRequest.ApiType)
                {
                    case ApiTypeEnum.ApiType.POST:
                        message.Method = HttpMethod.Post;
                        break;
                    case ApiTypeEnum.ApiType.PUT:
                        message.Method = HttpMethod.Put;
                        break;
                    case ApiTypeEnum.ApiType.DELETE:
                        message.Method = HttpMethod.Delete;
                        break;
                    default:
                        message.Method = HttpMethod.Get;
                        break;
                }
                HttpResponseMessage apiRs = null;
                apiRs = await client.SendAsync(message);
                var apiContent = await apiRs.Content.ReadAsStringAsync();
                var APIrs = JsonConvert.DeserializeObject<T>(apiContent);
                return APIrs;
            }
            catch (Exception ex)
            {
                var utAPIresponse = new APIResponse
                {
                    ErrorMessage = new List<string> { Convert.ToString(ex.Message) },
                    IsSuccess = false
                };
                var res = JsonConvert.SerializeObject(utAPIresponse);
                var APIRes = JsonConvert.DeserializeObject<T>(res);
                return APIRes;
            }
        }
    }
}
