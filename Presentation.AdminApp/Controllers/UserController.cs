using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Utilities.Constants;

namespace Presentation.AdminApp.Controllers
{
    public class UserController : BaseController
    {
        private readonly IConfiguration _configuration;

        public UserController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IActionResult Index()
        {
            var sessions = HttpContext.Session.GetString("Token");
            ViewBag.Token = sessions;
            ViewBag.BaseAddress = new Uri(_configuration[SystemConstants.AppSettings.BaseAddress]);
            return View();
        }
    }
}
