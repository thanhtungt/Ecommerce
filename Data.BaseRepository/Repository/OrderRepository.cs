using Data.BaseRepository;
using Data.Entity;
using Data.Entity.Models;

namespace Data.Repository
{
    public class OrderRepository : GenericRepository<Order>
    {
        public OrderRepository(EcommerceDbContext dbContext) : base(dbContext) { }
    }
}
