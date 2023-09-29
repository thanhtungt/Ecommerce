using Presentation.Models;
using Presentation.WebApp.Models;

namespace Presentation.WebApp.ApiServices
{
    public interface IOrderApiClient
    {
        Task<bool> CreateUserOrderInfo(CreateUserOrderInfoViewModel model);
        Task<IEnumerable<UserOrderInformationModel>> GetListUserOrderInfo(Guid userId);
        Task<bool> CreateOrder(string userId, int addressId);
        Task<IEnumerable<OrderResponseModel>> GetOrderByUserId(Guid userId);
    }
}
