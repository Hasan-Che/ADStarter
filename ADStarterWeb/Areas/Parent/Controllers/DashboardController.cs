using ADStarter.DataAccess.Repository.IRepository;
using ADStarter.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ADStarterWeb.Areas.Parent.Controllers
{
    [Area("Parent")]
    [Authorize(Roles = SD.Role_Parent)]
    public class DashboardController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DashboardController(IUnitOfWork unitOfWork, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var parentDetails = _unitOfWork.Parent.Get(filter: p => p.UserId == user.Id); // Replace with actual method to fetch admin detail
            if (parentDetails != null)
            {
                // Admin details exist, show dashboard
                return View();
            }
            else
            {
                // Admin details don't exist, redirect to AddAdminDetails
                return RedirectToAction("RegistrationFlow", "Registration", new { userId = user.Id });
            }
        }
    }
}
