using Newtonsoft.Json;
using Presentation.Models;
using Presentation.WebApp.Models;
using System.Net.Http.Headers;
using System.Text;
using Utilities.Constants;

namespace Presentation.WebApp.ApiServices
{
    public class OrderApiClient : BaseApiClient, IOrderApiClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public OrderApiClient(IHttpClientFactory httpClientFactory, IConfiguration configuration, IHttpContextAccessor httpContextAccessor) : base(httpClientFactory, configuration, httpContextAccessor)
        {
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            _httpClientFactory = httpClientFactory;
        }
        
        public async Task<bool> CreateOrder(string userId, int addressId)
        {
            var sessions = _httpContextAccessor
               .HttpContext
               .Session
               .GetString(SystemConstants.AppSettings.Token);
            var san = new CreateOrderViewModel() { AddressId = addressId };
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration[SystemConstants.AppSettings.BaseAddress]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);
            var json = JsonConvert.SerializeObject(san);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync($"api/Order/{userId}/{addressId}", httpContent);
            var result = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode) return true;
            return false;
        }

        public async Task<bool> CreateUserOrderInfo(CreateUserOrderInfoViewModel model)
        {
            var castmodel = new CreateUserOrderInfoModel()
            {
                UserId = model.UserId,
                Name = model.Name,
                PhoneNumber = model.PhoneNumber,
                Province = model.Province,
                District = model.District,
                Ward = model.Ward,
                Address = model.Address,
                IsDefault = model.IsDefault,
                AddressType = model.AddressType.ToString(),
            };

            var sessions = _httpContextAccessor
               .HttpContext
               .Session
               .GetString(SystemConstants.AppSettings.Token);
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration[SystemConstants.AppSettings.BaseAddress]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);
            var json = JsonConvert.SerializeObject(castmodel);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("api/Order/info", httpContent);
            var result = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode) return true;
            return false;

        }

        public async Task<IEnumerable<UserOrderInformationModel>> GetListUserOrderInfo(Guid userId)
        {
            var sessions = _httpContextAccessor
               .HttpContext
               .Session
               .GetString(SystemConstants.AppSettings.Token);
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration[SystemConstants.AppSettings.BaseAddress]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);
            var response = await client.GetAsync($"api/Order/info/{userId}");
            var body = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                var myDeserializedObjList = (IEnumerable<UserOrderInformationModel>)JsonConvert.DeserializeObject(body,
                    typeof(IEnumerable<UserOrderInformationModel>));

                return myDeserializedObjList;
            }

            return null;
        }

        public async Task<IEnumerable<OrderResponseModel>> GetOrderByUserId(Guid userId)
        {
            var sessions = _httpContextAccessor
               .HttpContext
               .Session
               .GetString(SystemConstants.AppSettings.Token);
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration[SystemConstants.AppSettings.BaseAddress]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);
            var response = await client.GetAsync($"api/Order/{userId}");
            var body = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                var myDeserializedObjList = (IEnumerable<OrderResponseModel>)JsonConvert.DeserializeObject(body,
                    typeof(IEnumerable<OrderResponseModel>));

                return myDeserializedObjList;
            }

            return null;
        }
    }
}
