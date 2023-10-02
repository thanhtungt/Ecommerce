using Data.Entity.Configurations;
using Data.Repository;

namespace Data.BaseRepository
{
    public interface IUnitOfWork : IAsyncDisposable
    {

        #region Properties
        CartRepository CartRepository { get; }
        CategoryRepository CategoryRepository { get; }
        OrderRepository OrderRepository { get; }
        OrderDetailRepository OrderDetailRepository { get; }
        ProductRepository ProductRepository { get; }
        ProductInCategoryRepository ProductInCategoryRepository { get; }
        UserOrderInformationRepository UserOrderInformationRepository { get; }



        #endregion




        Task<int> SaveChanageAsync();
        Task BeginTransactionAsync();
        Task CommitAsync();
        Task RollbackAsync();
    }
}
