using Microsoft.AspNetCore.Mvc;
using Presentation.AdminApp.ApiServices;
using Utilities.Constants;

namespace Presentation.AdminApp.Controllers
{
    public class OrderController : BaseController
    {
        private readonly IConfiguration _configuration;
        private readonly IOrderApiClient _orderApiClient;
        public OrderController(IOrderApiClient orderApiClient, IConfiguration configuration)
        {
            _orderApiClient = orderApiClient;
            _configuration = configuration;
        }
        public async Task<IActionResult> Index()
        {
            var dw = await _orderApiClient.GetOrder();
            ViewBag.BaseAddress = new Uri(_configuration[SystemConstants.AppSettings.BaseAddress]);
            return View(dw);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeStatus(int orderId, int statusCode, int currentStatus)
        {
            if(currentStatus != statusCode)
            {
                var de = await _orderApiClient.ChangeStatusOrder(orderId, statusCode);
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteOrder(int orderId)
        {
            await _orderApiClient.DeleteOrder(orderId);

            return RedirectToAction("Index");
        }
    }
}
