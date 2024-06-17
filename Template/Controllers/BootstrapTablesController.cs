using Microsoft.AspNetCore.Mvc;

namespace Cuba.Controllers
{
    public class BootstrapTablesController : Controller
    {
        public IActionResult BasicTables()
        {
            return View();
        }
        public IActionResult TableComponents()
        {
            return View();
        }
    }
}
