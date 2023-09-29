using Data.BaseRepository;
using Data.Entity;
using Data.Entity.Models;

namespace Data.Repository
{
    public class ProductRepository : GenericRepository<Product>
    {
        public ProductRepository(EcommerceDbContext dbContext) : base(dbContext) { }
    }
}
