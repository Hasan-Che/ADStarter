using Microsoft.AspNetCore.Mvc;

namespace Cuba.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
