using Microsoft.AspNetCore.Mvc;

namespace ADStarterWeb.Areas.CustomerService.Controllers
{
    [Area("CustomerService")]
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
