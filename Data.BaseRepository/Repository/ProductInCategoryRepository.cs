using Data.BaseRepository;
using Data.Entity;
using Data.Entity.Models;

namespace Data.Repository
{
    public class ProductInCategoryRepository : GenericRepository<ProductInCategory>
    {
        public ProductInCategoryRepository(EcommerceDbContext dbContext) : base(dbContext) { }
    }
}
