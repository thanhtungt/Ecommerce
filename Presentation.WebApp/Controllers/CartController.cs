using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Presentation.WebApp.ApiServices;
using Presentation.WebApp.Models;
using System.Security.Claims;
using Utilities.Constants;

namespace Presentation.WebApp.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly ICartApiClient _cartApiClient;
        private readonly IOrderApiClient _orderApiClient;
        private readonly IConfiguration _configuration;

        public CartController(ICartApiClient cartApiClient, IOrderApiClient orderApiClient, IConfiguration configuration)
        {
            _cartApiClient = cartApiClient;
            _orderApiClient = orderApiClient;
            _configuration = configuration;
        }
        public async Task<IActionResult> Index()
        {
            ViewBag.BaseAddress = new Uri(_configuration[SystemConstants.AppSettings.BaseAddress]);
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var num = await _cartApiClient.GetNumberProductInCart(userId);

            ViewBag.CartItemCount = num;

            var productList = await _cartApiClient.GetProductInCarts(userId);
            var userOrderInfo = await _orderApiClient.GetListUserOrderInfo(new Guid(userId));
            ViewBag.UserOrderInfo = userOrderInfo;
            ViewBag.UserId = userId;
            return View(productList);
        }

        [HttpGet("Cart/AddAddress")]
        public IActionResult AddAddress()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            ViewBag.UserId = userId;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddAddress(CreateUserOrderInfoViewModel model)
        {
            var result = await _orderApiClient.CreateUserOrderInfo(model);

            if (result) return RedirectToAction("Index", "Cart");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Checkout(CheckoutSubmitViewModel model)
        {
            if(model.UserId != null & model.SelectedAddress != 0)
            {
                var result = await _orderApiClient.CreateOrder(model.UserId, model.SelectedAddress);
                if(result) return RedirectToAction("Index", "Home");
                return RedirectToAction("Index", "Cart");
            }
            return RedirectToAction("Index", "Cart");
        }

        public async Task<IActionResult> ChangeQuantity(int quantity, int productId)
        {
            var model = new ChangeQuantityViewModel()
            {
                UserId = new Guid(User.FindFirst(ClaimTypes.NameIdentifier)?.Value),
                ProductId = productId,
                Quantity = quantity,
            };

            await _cartApiClient.ChangeQuantity(model);

            return RedirectToAction("Index");
        }
    }
}
