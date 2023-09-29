using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using Utilities.Constants;
using Newtonsoft.Json;
using System.Reflection;
using System.Text;
using Presentation.Models;
using System.Reflection.Metadata.Ecma335;
using Presentation.WebApp.Models;

namespace Presentation.WebApp.ApiServices
{
    public class CartApiClient : BaseApiClient, ICartApiClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CartApiClient(IHttpClientFactory httpClientFactory, IConfiguration configuration, IHttpContextAccessor httpContextAccessor) : base(httpClientFactory, configuration, httpContextAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<bool> AddToCart(string userId, int productId)
        {
            var model = new AddToCartModel()
            {
                UserId = new Guid(userId),
                ProductId = productId,
                Quantity = 1
            };

            var sessions = _httpContextAccessor
                .HttpContext
                .Session
                .GetString(SystemConstants.AppSettings.Token);
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration[SystemConstants.AppSettings.BaseAddress]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);
            var json = JsonConvert.SerializeObject(model);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("api/Cart", httpContent);
            var result = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode) return true;
            return false;
        }


        public async Task<string> GetNumberProductInCart(string userId)
        {
            var sessions = _httpContextAccessor
               .HttpContext
               .Session
               .GetString(SystemConstants.AppSettings.Token);
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration[SystemConstants.AppSettings.BaseAddress]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);
            var response = await client.GetAsync($"/api/Cart/number/{userId}");
            var body = await response.Content.ReadAsStringAsync();

            return body;
        }

        public async Task<IEnumerable<ProductInCartModel>> GetProductInCarts(string userId)
        {
            var sessions = _httpContextAccessor
                .HttpContext
                .Session
                .GetString(SystemConstants.AppSettings.Token);
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration[SystemConstants.AppSettings.BaseAddress]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);
            var response = await client.GetAsync($"api/Cart/{userId}");
            var body = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                var myDeserializedObjList = (IEnumerable<ProductInCartModel>)JsonConvert.DeserializeObject(body,
                    typeof(IEnumerable<ProductInCartModel>));

                return myDeserializedObjList;
            }
            throw new Exception(body);

        }
    }
}
