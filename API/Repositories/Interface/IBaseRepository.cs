using System.Linq.Expressions;

namespace API.Repositories.Interface
{
    public interface IBaseRepository<T> where T : class
    {
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? filter=null);
        Task<T> GetAsync(Expression<Func<T, bool>> filter);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task CreateAsync(T entity);
        Task SaveAsync();
    }
}
