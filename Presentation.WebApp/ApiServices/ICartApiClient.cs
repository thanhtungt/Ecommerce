using Presentation.Models;
using Presentation.WebApp.Models;

namespace Presentation.WebApp.ApiServices
{
    public interface ICartApiClient
    {
        Task<string> GetNumberProductInCart(string userId);
        Task<bool> AddToCart(string userId, int productId);
        Task<IEnumerable<ProductInCartModel>> GetProductInCarts(string userId);
    }
}
