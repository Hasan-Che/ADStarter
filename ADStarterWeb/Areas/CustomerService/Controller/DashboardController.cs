using ADStarter.DataAccess.Repository;
using ADStarter.DataAccess.Repository.IRepository;
using ADStarter.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ADStarterWeb.Areas.CustomerService.Controllers
{
    [Area("CustomerService")]
    [Authorize(Roles = SD.Role_Customer_service)]
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

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> UpdateProfile()
        {
            var user = await _userManager.GetUserAsync(User);
            var adminDetails = _unitOfWork.CustomerService.Get(filter: a => a.User.Id == user.Id);
            var roles = await _userManager.GetRolesAsync(user);
            var model = new ADStarter.Models.CustomerService
            {
                cs_name = adminDetails.cs_name,
                cs_phoneNum = adminDetails.cs_phoneNum,
                cs_street = adminDetails.cs_street,
                cs_zip = adminDetails.cs_zip,
                cs_city = adminDetails.cs_city,
                cs_state = adminDetails.cs_state
                // Add other properties as needed
            };
            ViewBag.UserEmail = user.Email;
            ViewBag.UserId = user.Id;
            ViewBag.UserRoles = roles;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProfile(ADStarter.Models.CustomerService obj, string userId)
        {
            if (ModelState.IsValid)
            {
                // Retrieve the IdentityUser object corresponding to the userId
                var user = await _userManager.FindByIdAsync(userId);

                if (user == null)
                {
                    return NotFound("User not found."); // Handle the case where user is not found
                }

                var existingCS = _unitOfWork.CustomerService.Get(filter: a => a.User.Id == userId);

                if (existingCS == null)
                {
                    // Assign the retrieved IdentityUser object to the CustomerService.User property
                    obj.User = user;

                    _unitOfWork.CustomerService.Add(obj);
                }
                else
                {
                    // Update existing CustomerService details
                    existingCS.cs_name = obj.cs_name;
                    existingCS.cs_phoneNum = obj.cs_phoneNum;
                    existingCS.cs_street = obj.cs_street;
                    existingCS.cs_zip = obj.cs_zip;
                    existingCS.cs_city = obj.cs_city;
                    existingCS.cs_state = obj.cs_state;

                    // Update other properties as needed
                    _unitOfWork.CustomerService.Update(existingCS);
                }

                _unitOfWork.Save();
                TempData["success"] = "Customer Service details updated successfully";
                return RedirectToAction("Index");
            }

            ViewBag.UserId = userId;
            return View(obj);
        }

        [HttpPost]
        public async Task<IActionResult> EditProfile(string newEmail)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            if (ModelState.IsValid)
            {
                user.Email = newEmail;
                user.UserName = newEmail;
                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Dashboard");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }

            var userRoles = await _userManager.GetRolesAsync(user);
            ViewBag.UserId = user.Id;
            ViewBag.UserRoles = userRoles;
            ViewBag.UserEmail = user.Email;

            return View();
        }
    }
}
