using Business.Models;
using Utilities.ServiceResult;

namespace Business.Domain.System.User
{
    public interface IUserService
    {
        Task<ServiceResult<string>> AuthenticateAsync(LoginRequestEntity request);
        Task<ServiceResult<bool>> RegisterAsync(RegisterRequestEntity request);
        Task<ServiceResult<bool>> RoleAssignAsync(Guid Id, IEnumerable<string> roleList);
        Task<ServiceResult<IEnumerable<string>>> GetAllRolesAsync();
        Task<ServiceResult<bool>> CreateUserOrderInformationAsync(Guid userId, UserOrderInformationEntity request);
        Task<ServiceResult<bool>> RemoveUserOrderInformationAsync(Guid userId, int userOrderInfoId);
        Task<ServiceResult<IEnumerable<UserOrderInformationEntity>>> GetUserOrderInformationAsync(Guid userId);

        Task<ServiceResult<IEnumerable<UserResponseEntity>>> GetAllUserInformationAsync();
        Task<ServiceResult<bool>> DeleteUserAsync(Guid id);
        Task<ServiceResult<bool>> SetAdmin(Guid id, bool isAdmin);
    }
}
