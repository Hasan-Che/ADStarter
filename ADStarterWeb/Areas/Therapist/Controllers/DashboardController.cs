using ADStarter.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ADStarterWeb.Areas.Therapist.Controllers
{
    [Area("Therapist")]
    [Authorize(Roles = SD.Role_Therapist)]
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
