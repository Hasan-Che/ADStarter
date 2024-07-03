using ADStarter.DataAccess.Repository;
using ADStarter.DataAccess.Repository.IRepository;
using ADStarter.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ADStarterWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class AdminDashboardController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdminDashboardController(IUnitOfWork unitOfWork, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _roleManager = roleManager;
        }
       
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var adminDetails = _unitOfWork.Admin.Get(filter: a => a.User.Id == user.Id); // Replace with actual method to fetch admin detail
            if (adminDetails != null)
            {
                var numberOfChildren = _unitOfWork.Child.GetAll().Count();
                var numberOfParent = _unitOfWork.Parent.GetAll().Count();
                var numberOfTherapist = _unitOfWork.Therapist.GetAll().Count();
                var numberOfCustomerService = _unitOfWork.CustomerService.GetAll().Count();
                var totalInvoiceAmount = _unitOfWork.Invoice.GetTotalInvoiceAmount();
                var totalPaidAmount = _unitOfWork.Payment.GetTotalPaidAmount();
                var viewModel = new ADStarter.Models.ViewModels.DashboardVM
                {
                    NumberOfChildren = numberOfChildren,
                    NumberOfParent = numberOfParent,
                    NumberOfTherapist = numberOfTherapist,
                    NumberOfCustomerService = numberOfCustomerService,
                    TotalInvoiceAmount = totalInvoiceAmount,
                    TotalPaidAmount = totalPaidAmount
                };
                // Admin details exist, show dashboard with number of children
                return View(viewModel);
            }
            else
            {
                // Admin details don't exist, redirect to AddAdminDetails
                return RedirectToAction("AddAdminDetails", new {userId = user.Id});
            }
        }
        public IActionResult AddAdminDetails(string? userId)
        {
            ViewBag.UserId = userId;

            // Check if the user already has Customer Service details
            var adminDetails = _unitOfWork.Admin.Get(filter: a => a.User.Id == userId);

            if (adminDetails != null)
            {
                // Populate the form with existing details
                var model = new ADStarter.Models.Admin
                {
                    a_name = adminDetails.a_name,
                    a_phoneNum = adminDetails.a_phoneNum,
                    a_street = adminDetails.a_street,
                    a_zip = adminDetails.a_zip,
                    a_city = adminDetails.a_city,
                    a_state = adminDetails.a_state
                    // Add other properties as needed
                };

                return View(model);
            }

            // Return an empty form
            return View(new ADStarter.Models.Admin());
        }

        public async Task<IActionResult> UpdateProfile()
        {
            var user = await _userManager.GetUserAsync(User);
            var adminDetails = _unitOfWork.Admin.Get(filter: a => a.User.Id == user.Id);
            var roles = await _userManager.GetRolesAsync(user);
            var model = new ADStarter.Models.Admin
            {
                a_name = adminDetails.a_name,
                a_phoneNum = adminDetails.a_phoneNum,
                a_street = adminDetails.a_street,
                a_zip = adminDetails.a_zip,
                a_city = adminDetails.a_city,
                a_state = adminDetails.a_state
                // Add other properties as needed
            };
            ViewBag.UserEmail = user.Email;
            ViewBag.UserId = user.Id;
            ViewBag.UserRoles = roles;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddAdminDetailsAsync(ADStarter.Models.Admin obj, string userId)
        {
            if (ModelState.IsValid)
            {
                // Retrieve the IdentityUser object corresponding to the userId
                var user = await _userManager.FindByIdAsync(userId);

                if (user == null)
                {
                    return NotFound("User not found."); // Handle the case where user is not found
                }

                var existingAdmin = _unitOfWork.Admin.Get(filter: a => a.User.Id == userId);

                if (existingAdmin == null)
                {
                    // Assign the retrieved IdentityUser object to the CustomerService.User property
                    obj.User = user;

                    _unitOfWork.Admin.Add(obj);
                }
                else
                {
                    // Update existing CustomerService details
                    existingAdmin.a_name = obj.a_name;
                    existingAdmin.a_phoneNum = obj.a_phoneNum;
                    existingAdmin.a_street = obj.a_street;
                    existingAdmin.a_zip = obj.a_zip;
                    existingAdmin.a_city = obj.a_city;
                    existingAdmin.a_state = obj.a_state;

                    // Update other properties as needed
                    _unitOfWork.Admin.Update(existingAdmin);
                }

                _unitOfWork.Save();
                TempData["success"] = "Customer Service details updated successfully";
                return RedirectToAction("Index");
            }

            ViewBag.UserId = userId;
            return View(obj);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProfile(ADStarter.Models.Admin obj, string userId)
        {
            if (ModelState.IsValid)
            {
                // Retrieve the IdentityUser object corresponding to the userId
                var user = await _userManager.FindByIdAsync(userId);

                if (user == null)
                {
                    return NotFound("User not found."); // Handle the case where user is not found
                }

                var existingAdmin = _unitOfWork.Admin.Get(filter: a => a.User.Id == userId);

                if (existingAdmin == null)
                {
                    // Assign the retrieved IdentityUser object to the CustomerService.User property
                    obj.User = user;

                    _unitOfWork.Admin.Add(obj);
                }
                else
                {
                    // Update existing CustomerService details
                    existingAdmin.a_name = obj.a_name;
                    existingAdmin.a_phoneNum = obj.a_phoneNum;
                    existingAdmin.a_street = obj.a_street;
                    existingAdmin.a_zip = obj.a_zip;
                    existingAdmin.a_city = obj.a_city;
                    existingAdmin.a_state = obj.a_state;

                    // Update other properties as needed
                    _unitOfWork.Admin.Update(existingAdmin);
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
                    return RedirectToAction("Index", "AdminDashboard");
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
