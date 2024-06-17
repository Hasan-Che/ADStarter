using Microsoft.AspNetCore.Mvc;

namespace Cuba.Controllers
{
    public class CalenderController : Controller
    {
        public IActionResult Calender()
        {
            return View();
        }
    }
}
