using Data.BaseRepository;
using Data.Entity;
using Data.Entity.Models;

namespace Data.Repository
{
    public class UserOrderInformationRepository : GenericRepository<UserOrderInformation>
    {
        public UserOrderInformationRepository(EcommerceDbContext dbContext) : base(dbContext) { }
    }
}
