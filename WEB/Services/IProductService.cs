using WEB.Models.DTOs;

namespace WEB.Services
{
    public interface IProductService
    {
        Task<T> GetAsync<T>(Guid id);
        Task<T> GetAllAsync<T>(string filter);
        Task<T> CreateAsync<T>(ProductCreateDto dto);
        Task<T> UpdateAsync<T>(ProductDto dto);
        Task<T> DeleteAsync<T>(Guid id);
    }
}
