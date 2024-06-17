using Microsoft.AspNetCore.Mvc;

namespace Cuba.Controllers
{
    public class widgetsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult chart_widget()
        {
            return View();
        }
    }
}
