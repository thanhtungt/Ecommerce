using Presentation.Models;
using Utilities.ServiceResult;

namespace Presentation.AdminApp.ApiServices
{
    public interface IUserApiClient
    {
        public Task<ServiceResult<string>> Authenticate(LoginModel model);
        public Task<string> Register(RegisterModel model);
    }
}
