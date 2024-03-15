namespace WEB.Services
{
    public interface IProductService
    {
        Task<T> GetAsync<T>(Guid id);
        Task<T> GetAllAsync<T>(string filter);
    }
}
