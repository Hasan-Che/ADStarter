using Microsoft.AspNetCore.Mvc;

namespace ADStarterWeb.Areas.landingPage.Controllers
{
    public class landingPages : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
