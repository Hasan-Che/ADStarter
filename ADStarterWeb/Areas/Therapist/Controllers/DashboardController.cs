using Microsoft.AspNetCore.Mvc;

namespace ADStarterWeb.Areas.Therapist.Controllers
{
    [Area("Therapist")]
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
