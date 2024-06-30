using ADStarter.DataAccess.Repository.IRepository;
using ADStarter.Models.ViewModels;
using ADStarter.Models;
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
    public class UserListController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUnitOfWork _unitOfWork;

        public UserListController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _unitOfWork = unitOfWork;
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

        public IActionResult AddTherapistDetails(string? userId)
        {
            ViewBag.UserId = userId;

            // Check if the user already has Customer Service details
            var addTherapistDetails = _unitOfWork.Therapist.Get(filter: t => t.User.Id == userId);

            if (addTherapistDetails != null)
            {
                // Populate the form with existing details
                var model = new ADStarter.Models.Therapist
                {
                    t_name = addTherapistDetails.t_name,
                    t_phoneNum = addTherapistDetails.t_phoneNum,
                    t_street = addTherapistDetails.t_street,
                    t_zip = addTherapistDetails.t_zip,
                    t_city = addTherapistDetails.t_city,
                    t_state = addTherapistDetails.t_state
                    // Add other properties as needed
                };

                return View(model);
            }

            // Return an empty form
            return View(new ADStarter.Models.Therapist());
        }

        public IActionResult AddCustomerServiceDetails(string? userId)
        {
            ViewBag.UserId = userId;

            // Check if the user already has Customer Service details
            var customerServiceDetails = _unitOfWork.CustomerService.Get(filter:cs => cs.User.Id == userId);
            
            if (customerServiceDetails != null)
            {
                // Populate the form with existing details
                var model = new ADStarter.Models.CustomerService
                {
                    cs_name = customerServiceDetails.cs_name,
                    cs_phoneNum = customerServiceDetails.cs_phoneNum,
                    cs_street = customerServiceDetails.cs_street,
                    cs_zip = customerServiceDetails.cs_zip,
                    cs_city = customerServiceDetails.cs_city,
                    cs_state = customerServiceDetails.cs_state
                    // Add other properties as needed
                };

                return View(model);
            }

            // Return an empty form
            return View(new ADStarter.Models.CustomerService());
        }

        [HttpPost]
        public async Task<IActionResult> AddTherapistDetailsAsync(ADStarter.Models.Therapist obj, string userId)
        {
            if (ModelState.IsValid)
            {
                // Retrieve the IdentityUser object corresponding to the userId
                var user = await _userManager.FindByIdAsync(userId);

                if (user == null)
                {
                    return NotFound("User not found."); // Handle the case where user is not found
                }

                var existingTherapist = _unitOfWork.Therapist.Get(filter: t => t.User.Id == userId);

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
                return RedirectToAction("List");
            }
            
            ViewBag.UserId = userId;
            return View(obj);
        }

        [HttpPost]
        public async Task<IActionResult> AddCustomerServiceDetailsAsync(ADStarter.Models.CustomerService obj, string userId)
        {
            if (ModelState.IsValid)
            {
                // Retrieve the IdentityUser object corresponding to the userId
                var user = await _userManager.FindByIdAsync(userId);

                if (user == null)
                {
                    return NotFound("User not found."); // Handle the case where user is not found
                }

                var existingCustomerService = _unitOfWork.CustomerService.Get(filter: cs => cs.User.Id == userId);

                if (existingCustomerService == null)
                {
                    // Assign the retrieved IdentityUser object to the CustomerService.User property
                    obj.User = user;

                    _unitOfWork.CustomerService.Add(obj);
                }
                else
                {
                    // Update existing CustomerService details
                    existingCustomerService.cs_name = obj.cs_name;
                    existingCustomerService.cs_phoneNum = obj.cs_phoneNum;
                    existingCustomerService.cs_street = obj.cs_street;
                    existingCustomerService.cs_city = obj.cs_city;
                    existingCustomerService.cs_zip = obj.cs_zip;
                    existingCustomerService.cs_state = obj.cs_state;


                    // Update other properties as needed
                    _unitOfWork.CustomerService.Update(existingCustomerService);
                }

                _unitOfWork.Save();
                TempData["success"] = "Customer Service details updated successfully";
                return RedirectToAction("List");
            }

            ViewBag.UserId = userId;
            return View(obj);
        }
        ////public async Task<IActionResult> AddCustomerServiceDetailsAsync(ADStarter.Models.CustomerService obj, string userId)
        ////{
        ////    try
        ////    {
        ////        if (ModelState.IsValid)
        ////        {
        ////            // Retrieve the IdentityUser object corresponding to the userId
        ////            var user = await _userManager.FindByIdAsync(userId);

        ////            if (user == null)
        ////            {
        ////                return NotFound("User not found."); // Handle the case where user is not found
        ////            }

        ////            var existingCustomerService = _unitOfWork.CustomerService.Get(filter: cs => cs.User.Id == userId);

        ////            if (existingCustomerService == null)
        ////            {
        ////                // Assign the retrieved IdentityUser object to the CustomerService.User property
        ////                obj.User = user;

        ////                _unitOfWork.CustomerService.Add(obj);
        ////            }
        ////            else
        ////            {
        ////                // Update existing CustomerService details
        ////                existingCustomerService.cs_name = obj.cs_name;
        ////                existingCustomerService.cs_phoneNum = obj.cs_phoneNum;
        ////                existingCustomerService.cs_street = obj.cs_street;
        ////                existingCustomerService.cs_city = obj.cs_city;
        ////                existingCustomerService.cs_zip = obj.cs_zip;
        ////                existingCustomerService.cs_state = obj.cs_state;

        ////                // Update other properties as needed
        ////                _unitOfWork.CustomerService.Update(existingCustomerService);
        ////            }

        ////            _unitOfWork.Save();
        ////            TempData["success"] = "Customer Service details updated successfully";
        ////            return RedirectToAction("List");
        ////        }

        ////        // ModelState is invalid, handle errors
        ////        foreach (var key in ModelState.Keys)
        ////        {
        ////            var errors = ModelState[key].Errors;
        ////            foreach (var error in errors)
        ////            {
        ////                // Log the error or process it as needed
        ////                var errorMessage = error.ErrorMessage;
        ////                // or
        ////                var exception = error.Exception;
        ////            }
        ////        }

        ////        ViewBag.UserId = userId;
        ////        return View(obj);
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        // Log the exception or handle it as needed
        ////        // For demonstration, we'll log to the console
        ////        Console.WriteLine($"Exception occurred: {ex.Message}");
        ////        TempData["error"] = "An error occurred while processing the request.";
        ////        return RedirectToAction("List"); // Redirect to a safe page or handle accordingly
        ////    }
        ////}


    }
}
