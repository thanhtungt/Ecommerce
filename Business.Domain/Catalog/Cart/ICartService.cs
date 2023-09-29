using Business.Models;
using Utilities.ServiceResult;

namespace Business.Domain.Catalog.CartNP
{
    public interface ICartService
    {
        Task<ServiceResult<bool>> AddToCartAsync(Guid userId, int productId, int quantity);
        Task<ServiceResult<bool>> RemoveToCartAsync(Guid userId, int productId);
        Task<ServiceResult<bool>> ChangeQuantityAsync(Guid userId, int productId, int amount);
        Task<ServiceResult<IEnumerable<ProductInCartResponseEntity>>> GetCartAsync(Guid userId);
        Task<ServiceResult<int>> GetNumberOfProductInCart(Guid userId);

    }
}
