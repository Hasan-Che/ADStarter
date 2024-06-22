using Microsoft.AspNetCore.Mvc;

namespace ADStarterWeb.Areas.Parent.Controllers
{
    [Area("Parent")]
    public class landingPageController : Controller
    {
        public IActionResult landingPage()
        {
            return View();
        }
    }
}

