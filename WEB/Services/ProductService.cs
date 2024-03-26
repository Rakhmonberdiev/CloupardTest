
using WEB.Models.DTOs;
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

        public Task<APIResponse> CreateAsync(ProductCreateDto dto)
        {
            return SendAsync<APIResponse>(new APIRequest()
            {
                ApiType = ApiTypeEnum.ApiType.POST,
                Data = dto,
                Url = apiUrl+"/api/Product"
            });

        }

        public Task<APIResponse> DeleteAsync(Guid id)
        {
            return SendAsync<APIResponse>(new APIRequest()
            {
                ApiType = ApiTypeEnum.ApiType.DELETE,
                Url = apiUrl + "/api/Product/" + id
            });
        }

        public Task<APIResponse> GetAllAsync(string filter)
        {
            return SendAsync<APIResponse>(new APIRequest()
            {
                ApiType = ApiTypeEnum.ApiType.GET,
                Url = apiUrl + "/api/Product/?filter=" + filter
            });
        }

        public Task<APIResponse> GetAsync(Guid id)
        {
            return SendAsync<APIResponse>(new APIRequest()
            {
                ApiType = ApiTypeEnum.ApiType.GET,
                Url = apiUrl + "/api/Product/" + id
            });
        }

        public Task<APIResponse> UpdateAsync(ProductDto dto)
        {
            return SendAsync<APIResponse>(new APIRequest()
            {
                ApiType = ApiTypeEnum.ApiType.PUT,
                Data = dto,
                Url = apiUrl + "/api/Product/" + dto.Id
            });
        }
    }
}
