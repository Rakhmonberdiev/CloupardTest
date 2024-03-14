namespace API.Repositories.Interface
{
    public interface IBaseRepository<T> where T : class
    {
        Task CreateAsync(T entity);
        Task SaveAsync();
    }
}
