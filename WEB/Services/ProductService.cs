
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

        public Task<T> CreateAsync<T>(ProductCreateDto dto)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = ApiTypeEnum.ApiType.POST,
                Data = dto,
                Url = apiUrl+"/api/Product"
            });

        }

        public Task<T> DeleteAsync<T>(Guid id)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = ApiTypeEnum.ApiType.DELETE,
                Url = apiUrl + "/api/Product/" + id
            });
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

        public Task<T> UpdateAsync<T>(ProductUpdateDto dto)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = ApiTypeEnum.ApiType.PUT,
                Data = dto,
                Url = apiUrl + "/api/Product" + dto.Id
            });
        }
    }
}
