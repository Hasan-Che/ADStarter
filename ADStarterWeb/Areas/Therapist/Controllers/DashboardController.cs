using ADStarter.DataAccess.Repository.IRepository;
using ADStarter.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ADStarterWeb.Areas.Therapist.Controllers
{
    [Area("Therapist")]
    [Authorize(Roles = SD.Role_Therapist)]

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
            var adminDetails = _unitOfWork.Therapist.Get(filter: a => a.User.Id == user.Id);
            var roles = await _userManager.GetRolesAsync(user);
            var model = new ADStarter.Models.Therapist
            {
                t_name = adminDetails.t_name,
                t_phoneNum = adminDetails.t_phoneNum,
                t_street = adminDetails.t_street,
                t_zip = adminDetails.t_zip,
                t_city = adminDetails.t_city,
                t_state = adminDetails.t_state
                // Add other properties as needed
            };
            ViewBag.UserEmail = user.Email;
            ViewBag.UserId = user.Id;
            ViewBag.UserRoles = roles;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProfile(ADStarter.Models.Therapist obj, string userId)
        {
            if (ModelState.IsValid)
            {
                // Retrieve the IdentityUser object corresponding to the userId
                var user = await _userManager.FindByIdAsync(userId);

                if (user == null)
                {
                    return NotFound("User not found."); // Handle the case where user is not found
                }

                var existingTherapist = _unitOfWork.Therapist.Get(filter: a => a.User.Id == userId);

                if (existingTherapist == null)
                {
                    // Assign the retrieved IdentityUser object to the CustomerService.User property
                    obj.User = user;

                    _unitOfWork.Therapist.Add(obj);
                }
                else
                {
                    // Update existing CustomerService details
                    existingTherapist.t_name = obj.t_name;
                    existingTherapist.t_phoneNum = obj.t_phoneNum;
                    existingTherapist.t_street = obj.t_street;
                    existingTherapist.t_zip = obj.t_zip;
                    existingTherapist.t_city = obj.t_city;
                    existingTherapist.t_state = obj.t_state;

                    // Update other properties as needed    
                    _unitOfWork.Therapist.Update(existingTherapist);
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
