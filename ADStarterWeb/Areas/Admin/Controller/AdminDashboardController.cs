using Microsoft.AspNetCore.Mvc;

namespace ADStarterWeb.Areas.Parent.Controllers
{
    [Area("Admin")]
    public class AdminDashboardController : Controller
    {
        public IActionResult Index(string id) 
        {
            var userId = HttpContext.Session.GetInt32("UserId");

            if (!userId.HasValue)
            {
                // Handle the case where user ID is not available
                return RedirectToAction("Login", "Account", new { area = "Identity" });
            }

            ViewData["UserId"] = userId.Value;

            // Your logic here

            return View();
        }
    }
}
