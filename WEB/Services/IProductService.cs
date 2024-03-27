using WEB.Models.DTOs;
using WEB.Utilities;

namespace WEB.Services
{
    public interface IProductService
    {
        Task<ProductDto> GetAsync(Guid id);
        Task<List<ProductDto>> GetAllAsync(string filter);
        Task<APIResponse> CreateAsync(ProductCreateDto dto);
        Task<APIResponse> UpdateAsync(ProductDto dto);
        Task<APIResponse> DeleteAsync(Guid id);
    }
}
