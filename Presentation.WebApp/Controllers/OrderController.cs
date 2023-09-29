using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.WebApp.ApiServices;
using System.Security.Claims;

namespace Presentation.WebApp.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly ICartApiClient _cartApiClient;
        private readonly IOrderApiClient _orderApiClient;

        public OrderController(ICartApiClient cartApiClient, IOrderApiClient orderApiClient)
        {
            _cartApiClient = cartApiClient;
            _orderApiClient = orderApiClient;
        }
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var num = await _cartApiClient.GetNumberProductInCart(userId);

            ViewBag.CartItemCount = num;

            var dw = await _orderApiClient.GetOrderByUserId(new Guid(userId));



            return View(dw);
        }
    }
}
