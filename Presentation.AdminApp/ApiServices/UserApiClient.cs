using Newtonsoft.Json;
using Presentation.Models;
using System.Net.Http;
using System.Text;
using System;
using Utilities.Constants;
using Microsoft.Extensions.Configuration;
using Utilities.ServiceResult;

namespace Presentation.AdminApp.ApiServices
{

    public class UserApiClient : BaseApiClient, IUserApiClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        public UserApiClient(IHttpClientFactory httpClientFactory, IConfiguration configuration, IHttpContextAccessor httpContextAccessor) : base(httpClientFactory, configuration, httpContextAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        public async Task<ServiceResult<string>> Authenticate(LoginModel model)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration[SystemConstants.AppSettings.BaseAddress]);
            var json = JsonConvert.SerializeObject(model);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("api/User/authenticate", httpContent);
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                return new ServiceSuccessResult<string>(result);
            }
            return new ServiceErrorResult<string> ( result );
        }

        public async Task<string> Register(RegisterModel model)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration[SystemConstants.AppSettings.BaseAddress]);
            var json = JsonConvert.SerializeObject(model);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("api/User/register", httpContent);
            var result = await response.Content.ReadAsStringAsync();

            return result.ToString();
        }
    }
}
