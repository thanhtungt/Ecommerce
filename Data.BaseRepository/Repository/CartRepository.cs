using Data.BaseRepository;
using Data.Entity;
using Data.Entity.Models;

namespace Data.Repository
{
    public class CartRepository : GenericRepository<Cart>
    {
        public CartRepository(EcommerceDbContext dbContext) : base(dbContext) { }
    }
}
