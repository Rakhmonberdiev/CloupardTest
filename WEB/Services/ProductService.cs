
using Newtonsoft.Json;
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

        public async Task<List<ProductDto>> GetAllAsync(string filter)
        {
 
            var response = await SendAsync<APIResponse>(new APIRequest()
            {
                ApiType = ApiTypeEnum.ApiType.GET,
                Url = apiUrl + "/api/Product/?filter=" + filter
            });
            if (response != null && response.IsSuccess)
            {
                var products = JsonConvert.DeserializeObject<List<ProductDto>>(Convert.ToString(response.Result));
                return products;
            }

            return null;
        }

        public async Task<ProductDto> GetAsync(Guid id)
        {
            var response = await SendAsync<APIResponse>(new APIRequest()
            {
                ApiType = ApiTypeEnum.ApiType.GET,
                Url = apiUrl + "/api/Product/" + id
            });
            if (response != null && response.IsSuccess)
            {
                var productDto = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(response.Result));
                return productDto;
            }
            return null;
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
