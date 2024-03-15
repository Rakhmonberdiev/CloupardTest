
using WEB.Services.BaseServices;
using WEB.Utilities;

namespace WEB.Services
{
    public class ProductService :BaseService, IProductService
    {
        private readonly IHttpClientFactory _httpClient;
        private string apiUrl;
        public ProductService(IHttpClientFactory httpClient, IConfiguration configuration) : base(httpClient)
        {
            _httpClient = httpClient;
            apiUrl = configuration.GetValue<string>("ServiceUrls:API");
        }

        public Task<T> GetAllAsync<T>(string filter)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = ApiTypeEnum.ApiType.GET,
                Url = apiUrl + "/api/Product/?filter=" + filter
            });
        }

        public Task<T> GetAsync<T>(Guid id)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = ApiTypeEnum.ApiType.GET,
                Url = apiUrl + "/api/Product/" + id
            });
        }
    }
}
