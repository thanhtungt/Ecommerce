using Microsoft.AspNetCore.Mvc;
using Presentation.AdminApp.ApiServices;

namespace Presentation.AdminApp.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderApiClient _orderApiClient;
        public OrderController(IOrderApiClient orderApiClient)
        {
            _orderApiClient = orderApiClient;
        }
        public async Task<IActionResult> Index()
        {
            var dw = await _orderApiClient.GetOrder();
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
    }
}
