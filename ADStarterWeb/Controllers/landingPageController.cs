using Microsoft.AspNetCore.Mvc;

namespace Cuba.Controllers
{
    public class landingPageController : Controller
    {
        public IActionResult landingPages()
        {
            return View();
        }
    }
}
