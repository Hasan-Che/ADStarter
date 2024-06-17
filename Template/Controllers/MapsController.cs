using Microsoft.AspNetCore.Mvc;

namespace Viho.Controllers
{
    public class MapsController : Controller
    {
        public IActionResult MapJS()
        {
            return View();
        }
        public IActionResult VectorMaps()
        {
            return View();
        }
    }
}
