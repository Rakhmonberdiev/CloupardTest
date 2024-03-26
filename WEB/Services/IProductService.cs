using WEB.Models.DTOs;
using WEB.Utilities;

namespace WEB.Services
{
    public interface IProductService
    {
        Task<APIResponse> GetAsync(Guid id);
        Task<APIResponse> GetAllAsync(string filter);
        Task<APIResponse> CreateAsync(ProductCreateDto dto);
        Task<APIResponse> UpdateAsync(ProductDto dto);
        Task<APIResponse> DeleteAsync(Guid id);
    }
}
