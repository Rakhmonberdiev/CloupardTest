using API.Data;
using API.Models;
using API.Repositories.Interface;

namespace API.Repositories.Implementation
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext db) : base(db)
        {
        }
    }
}
