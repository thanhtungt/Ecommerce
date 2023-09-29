using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.WebApp.ApiServices;
using Presentation.WebApp.Models;
using System.Diagnostics;
using System.Security.Claims;

namespace Presentation.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductApiClient _productApiClient;
        private readonly ICartApiClient _cartApiClient;

        public HomeController(ILogger<HomeController> logger, IProductApiClient productApiClient, ICartApiClient cartApiClient)
        {
            _logger = logger;
            _productApiClient = productApiClient;
            _cartApiClient = cartApiClient;
        }

        public async Task<IActionResult> Index(int pageIndex = 1, int pageSize = 8)
        {
            if (User.Identity.IsAuthenticated)
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (userId != null)
                {
                    var num = await _cartApiClient.GetNumberProductInCart(userId);

                    ViewBag.CartItemCount = num;
                }
            }
            var result = await _productApiClient.GetPagingProduct(pageIndex, pageSize);

            return View(result);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

       
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddToCart(int productId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if(userId != null)
            {
                var result = await _cartApiClient.AddToCart(userId, productId);
                if (result)
                {
                    ViewBag.ShowNotification = true;
                    ViewBag.NotificationMessage = "Add product to cart success!";
                }
                else
                {
                    ViewBag.ShowNotification = false;
                }
            }

            return RedirectToAction("Index", "Home");
        }
    }
}