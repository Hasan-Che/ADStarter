using Microsoft.AspNetCore.Mvc;

namespace Cuba.Controllers
{
    public class EmailController : Controller
    {
        public IActionResult EmailApp()
        {
            return View();
        }
        public IActionResult EmailCompose()
        {
            return View();
        }
    }
}
