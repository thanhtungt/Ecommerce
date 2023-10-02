using Newtonsoft.Json;
using Presentation.Models;
using System.Net.Http.Headers;
using System.Net.Http;
using Utilities.Constants;
using System.Text;
using Presentation.AdminApp.Models;
using Data.Entity.Models;
using Microsoft.AspNetCore.Http;

namespace Presentation.AdminApp.ApiServices
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

        public async Task<bool> ChangeStatusOrder(int productId, int statusCode)
        {
            var sessions = _httpContextAccessor
            .HttpContext
            .Session
               .GetString(SystemConstants.AppSettings.Token);

            var model = new ChangeStatusOrderViewModel() { OrderStatus = statusCode };
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration[SystemConstants.AppSettings.BaseAddress]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);
            var json = JsonConvert.SerializeObject(model);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PutAsync($"api/Order/{productId}/{statusCode}", httpContent);
            var result = await response.Content.ReadAsStringAsync();

            if(response.IsSuccessStatusCode) { return true; }
            return false;
        }

        public async Task<bool> DeleteOrder(int orderId)
        {
            var sessions = _httpContextAccessor
            .HttpContext
            .Session
            .GetString(SystemConstants.AppSettings.Token);

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration[SystemConstants.AppSettings.BaseAddress]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);
            var response = await client.DeleteAsync($"api/Order/{orderId}");
            var result = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode) { return true; }
            return false;
        }

        public async Task<IEnumerable<OrderResponseModel>> GetOrder()
        {
            var sessions = _httpContextAccessor
            .HttpContext
            .Session
               .GetString(SystemConstants.AppSettings.Token);
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration[SystemConstants.AppSettings.BaseAddress]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);
            var response = await client.GetAsync($"api/Order");
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
