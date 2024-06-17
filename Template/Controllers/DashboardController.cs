using Microsoft.AspNetCore.Mvc;

namespace Cuba.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Ecommerce()
        {
            return View();
        }
        public IActionResult OnlineCourse()
        {
            return View();
        }
        public IActionResult Crypto()
        {
            return View();
        }
        public IActionResult Social()
        {
            return View();
        }
    }
}
