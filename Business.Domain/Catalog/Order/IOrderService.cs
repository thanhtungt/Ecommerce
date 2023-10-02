using Business.Models;
using Data.Entity.Enums;
using Utilities.ServiceResult;

namespace Business.Domain.Catalog.OrderNP
{
    public interface IOrderService
    {
        Task<ServiceResult<bool>> CreateUserOrderInfoAsync(UserOrderInformationEntity model);
        Task<ServiceResult<IEnumerable<UserOrderInformationEntity>>> GetListUserOrderInfoAsync(Guid userId);
        Task<ServiceResult<bool>> DeleteUserOrderInfoAsync(int id);

        Task<ServiceResult<bool>> CreateOrderAsync(Guid userId, int addressId);
        Task<ServiceResult<IEnumerable<OrderResponseEntity>>> GetOrderByUserIdAsync(Guid userId);
        Task<ServiceResult<IEnumerable<OrderResponseEntity>>> GetOrderAsync();
        Task<ServiceResult<bool>> SetOrderStatusAsync(int orderId,OrderStatus type);
        Task<ServiceResult<bool>> DeleteOrderAsync(int orderId);
    }
}
