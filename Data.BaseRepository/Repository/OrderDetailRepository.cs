using Data.BaseRepository;
using Data.Entity;
using Data.Entity.Models;

namespace Data.Repository
{
    public class OrderDetailRepository : GenericRepository<OrderDetail>
    {
        public OrderDetailRepository(EcommerceDbContext dbContext) : base(dbContext) { }
    }
}
