using ADStarter.Models.ViewModels;
using ADStarter.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ADStarterWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class UserListController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserListController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> List()
        {
            var users = _userManager.Users.ToList();
            var userRolesViewModel = new List<UserRolesViewModel>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                if (roles.Contains("Customer Service") || roles.Contains("Therapist"))
                {
                    var thisViewModel = new UserRolesViewModel
                    {
                        UserId = user.Id,
                        Email = user.Email,
                        Roles = roles
                    };
                    userRolesViewModel.Add(thisViewModel);
                }
            }

            return View(userRolesViewModel);
        }
        public IActionResult AddDetails()
        {
            return View();
        }
    }
}
