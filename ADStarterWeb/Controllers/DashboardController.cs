using Microsoft.AspNetCore.Mvc;

namespace ADStarterWeb.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
