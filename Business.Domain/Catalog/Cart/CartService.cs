using Business.Models;
using Data.BaseRepository;
using Data.Entity.Models;
using Utilities.ServiceResult;

namespace Business.Domain.Catalog.CartNP
{
    public class CartService : ICartService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CartService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ServiceResult<bool>> AddToCartAsync(Guid userId, int productId, int quantity)
        {
            try
            {
                var cart = await _unitOfWork.CartRepository.FindAsync(x => x.ProductId == productId && x.UserId == userId);
                if (cart != null)
                {
                    cart.Quantity += quantity;
                    await _unitOfWork.SaveChanageAsync();
                    return new ServiceSuccessResult<bool>();
                };

                cart = new Cart()
                {
                    ProductId = productId,
                    Quantity = quantity,
                    UserId = userId,
                };

                await _unitOfWork.CartRepository.AddAsync(cart);
                await _unitOfWork.SaveChanageAsync();
                return new ServiceSuccessResult<bool>();
            }
            catch (Exception ex)
            {
                return new ServiceErrorResult<bool>(ex.Message);
            }
        }

        public async Task<ServiceResult<bool>> ChangeQuantityAsync(Guid userId, int productId, int amount)
        {
            try
            {
                var cart = await _unitOfWork.CartRepository.FindAsync(x => x.ProductId == productId && x.UserId == userId);
                
                if (cart == null)
                {
                    return new ServiceErrorResult<bool>("Không tìm thấy sản phẩm");
                };


                var quantity = cart.Quantity;
                if (quantity + amount > 0)
                {
                    cart.Quantity += amount;
                    await _unitOfWork.SaveChanageAsync();
                    return new ServiceSuccessResult<bool>();
                }

                _unitOfWork.CartRepository.Remove(cart);
                await _unitOfWork.SaveChanageAsync();
                return new ServiceSuccessResult<bool>();
            }
            catch (Exception ex)
            {
                return new ServiceErrorResult<bool>(ex.Message);
            }
        }

        public async Task<ServiceResult<IEnumerable<ProductInCartResponseEntity>>> GetCartAsync(Guid userId)
        {
            try
            {
                var query = from c in await _unitOfWork.CartRepository.GetAllAsync()
                            join p in await _unitOfWork.ProductRepository.GetAllAsync() on c.ProductId equals p.Id
                            where c.UserId == userId
                            select new ProductInCartResponseEntity()
                            {
                                ProductId = c.ProductId,
                                Name = p.Name,
                                Price = p.Price,
                                DiscountPercent = p.DiscountPercent,
                                ThumbnailImage = p.ProductImagePath,
                                Quantity = c.Quantity,
                            };
                return new ServiceSuccessResult<IEnumerable<ProductInCartResponseEntity>>(query);
            }
            catch (Exception ex)
            {
                return new ServiceErrorResult<IEnumerable<ProductInCartResponseEntity>>(ex.Message);
            }

            throw new NotImplementedException();
        }

        public async Task<ServiceResult<int>> GetNumberOfProductInCart(Guid userId)
        {
            try
            {
                var carts = await _unitOfWork.CartRepository.GetManyAsync(x => x.UserId == userId);
                return new ServiceSuccessResult<int>(carts.Count());
            }catch(Exception ex)
            {
                return new ServiceErrorResult<int>(ex.Message);
            }
        }

        public async Task<ServiceResult<bool>> RemoveToCartAsync(Guid userId, int productId)
        {
            try
            {
                var cart = await _unitOfWork.CartRepository.FindAsync(x => x.ProductId == productId && x.UserId == userId);

                if (cart == null)
                {
                    return new ServiceErrorResult<bool>("Không tìm thấy sản phẩm");
                };

                _unitOfWork.CartRepository.Remove(cart);
                await _unitOfWork.SaveChanageAsync();
                return new ServiceSuccessResult<bool>();
            }
            catch(Exception ex)
            {
                return new ServiceErrorResult<bool>(ex.Message);
            }
        }
    }
}
