using Presentation.Models;
using Utilities.ServiceResult;

namespace Presentation.WebApp.ApiServices
{
    public interface IUserApiClient
    {
        public Task<ServiceResult<string>> Authenticate(LoginModel model);
        public Task<string> Register(RegisterModel model);
    }
}
