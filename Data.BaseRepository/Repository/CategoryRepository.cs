using Data.BaseRepository;
using Data.Entity;
using Data.Entity.Models;

namespace Data.Repository
{
    public class CategoryRepository : GenericRepository<Category>
    {
        public CategoryRepository(EcommerceDbContext dbContext) : base(dbContext) { }
    }
}
