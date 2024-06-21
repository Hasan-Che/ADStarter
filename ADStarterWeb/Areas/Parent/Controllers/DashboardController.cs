using Microsoft.AspNetCore.Mvc;

namespace ADStarterWeb.Areas.Parent.Controllers
{
    [Area("Parent")]
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
