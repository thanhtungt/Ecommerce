using Data.Entity;
using Data.Entity.Models;
using Data.Repository;
using Microsoft.AspNetCore.Identity;

namespace Data.BaseRepository
{
    public class UnitOfWork : IUnitOfWork
    {
        #region Readonlys
        private readonly EcommerceDbContext _dbContext;

        #endregion

        #region Properties
        /*public CartRepository CartRepository { get; private set; }
        public CategoryRepository CategoryRepository { get; private set; }
        public OrderRepository OrderRepository { get; private set; }
        public OrderDetailRepository OrderDetailRepository { get; private set; }
        public ProductRepository ProductRepository { get; private set; }
        public ProductImageRepository ProductImageRepository { get; private set; }
        public ProductInCategoryRepository ProductInCategoryRepository { get; private set; }
        public UserOrderInformationRepository UserOrderInformationRepository { get; private set; }*/

        private CartRepository _cartRepository;
        private CategoryRepository _categoryRepository;
        private OrderRepository _orderRepository;
        private OrderDetailRepository _orderDetailRepository;
        private ProductRepository _productRepository;
        private ProductInCategoryRepository _productInCategoryRepository;
        private UserOrderInformationRepository _userOrderInformationRepository;

        public CartRepository CartRepository => _cartRepository ?? (_cartRepository = new CartRepository(_dbContext));
        public CategoryRepository CategoryRepository => _categoryRepository ?? (_categoryRepository = new CategoryRepository(_dbContext));
        public OrderRepository OrderRepository => _orderRepository ?? (_orderRepository = new OrderRepository(_dbContext));
        public OrderDetailRepository OrderDetailRepository => _orderDetailRepository ?? (_orderDetailRepository = new OrderDetailRepository(_dbContext));
        public ProductRepository ProductRepository => _productRepository ?? (_productRepository = new ProductRepository(_dbContext));
        public ProductInCategoryRepository ProductInCategoryRepository => _productInCategoryRepository ?? (_productInCategoryRepository = new ProductInCategoryRepository(_dbContext));
        public UserOrderInformationRepository UserOrderInformationRepository => _userOrderInformationRepository ?? (_userOrderInformationRepository = new UserOrderInformationRepository(_dbContext));


        #endregion

        #region Private Dispose Fields

        private bool _disposed;

        #endregion

        public UnitOfWork(EcommerceDbContext dbContext)
        {
            _dbContext = dbContext;

            /*CartRepository = new CartRepository();
            CategoryRepository = new CategoryRepository();
            OrderRepository = new OrderRepository();
            OrderDetailRepository = new OrderDetailRepository();
            ProductRepository = new ProductRepository();    
            ProductImageRepository = new ProductImageRepository();
            ProductInCategoryRepository = new ProductInCategoryRepository();
            UserOrderInformationRepository = new UserOrderInformationRepository();*/
        }
        public async ValueTask DisposeAsync()
        {
            await DisposeAsync(true);
            GC.SuppressFinalize(this);
        }

        protected virtual async ValueTask DisposeAsync(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    await _dbContext.DisposeAsync();
                }

                _disposed = true;
            }
        }

        public Task<int> SaveChanageAsync()
        {
            return _dbContext.SaveChangesAsync();
        }

        public async Task BeginTransactionAsync()
        {
            await _dbContext.Database.BeginTransactionAsync();
        }

        public async Task CommitAsync()
        {
            await _dbContext.Database.CommitTransactionAsync();
        }

        public async Task RollbackAsync()
        {
            await _dbContext.Database.RollbackTransactionAsync();
        }
    }
}
