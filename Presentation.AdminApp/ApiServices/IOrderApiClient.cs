using Presentation.Models;

namespace Presentation.AdminApp.ApiServices
{
    public interface IOrderApiClient
    {
        Task<IEnumerable<OrderResponseModel>> GetOrder();
        Task<bool> ChangeStatusOrder(int productId, int statusCode);
        Task<bool> DeleteOrder(int orderId);
    }
}
