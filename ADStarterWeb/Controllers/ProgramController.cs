using Microsoft.AspNetCore.Mvc;

namespace ADStarterWeb.Controllers
{
    public class ProgramController : Controller
    {
        // GET: Program/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Program/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(string progName, string progDesc, string progSummary, double progPrice)
        {
            if (ModelState.IsValid)
            {
                // For now, we'll just return a success view without doing anything
                return RedirectToAction("Success");
            }
            return View();
        }

        // GET: Program/Success
        public IActionResult Success()
        {
            return View();
        }
    }
}
