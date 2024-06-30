using Microsoft.AspNetCore.Mvc;
using ADStarter.Models.ViewModels;
using ADStarter.DataAccess.Data;
using ADStarter.Models;
using System.Security.Claims;
using ADStarter.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Identity;
using ADStarter.DataAccess.Repository.IRepository;
using ADStarter.Models;
using ADStarter.Models.ViewModels;
using ADStarter.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ADStarterWeb.Areas.Parent.Controllers
{
    [Area("Parent")]
    public class RegistrationController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly UserManager<IdentityUser> _userManager;

        public RegistrationController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment, UserManager<IdentityUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
            _userManager = userManager;
        }
        public IActionResult RegistrationFlow()
        {
            var model = new ParentChildViewModel
            {
                Parent = new ADStarter.Models.Parent(),
                Child = new ADStarter.Models.Child(),
                TreatmentHistory = new ADStarter.Models.TreatmentHistory()

            };

            ViewBag.ParentID = model.Parent.parent_ID; // Assuming ParentID is a property of Parent

            return View(model);
        }

        [HttpPost]
        public IActionResult RegistrationFlow(ParentChildViewModel model, ADStarter.Models.Parent parent, ADStarter.Models.Child child, ADStarter.Models.TreatmentHistory history, string action, IFormFile? file)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (ModelState.IsValid)
            {
                // Save Parent information
                parent.UserId = userId;
                _unitOfWork.Parent.Add(parent);
                _unitOfWork.Save();
                TempData["success"] = "Parent Detail created successfully";

                // Save Child information with photo upload
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    string c_photo = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string c_photoPath = Path.Combine(wwwRootPath, @"images\child");

                    using (var fileStream = new FileStream(Path.Combine(c_photoPath, c_photo), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    child.c_photo = @"\images\child\" + c_photo;
                }
                if (child.c_nationality == "Other")
                {
                    child.c_nationality = Request.Form["c_otherNationality"]; // Assign directly from form input
                }

                child.parent_ID = parent.parent_ID; // Assuming parent_ID is set after save
                _unitOfWork.Child.Add(child);
                _unitOfWork.Save();
                TempData["success"] = "Child Detail created successfully";

                // Save Treatment History information
                history.c_myKid = child.c_myKid; // Assuming c_myKid is set after save
                _unitOfWork.TreatmentHistory.Add(history);
                _unitOfWork.Save();
                TempData["success"] = "Treatment History Detail created successfully";

                return RedirectToAction("Index", "Dashboard");
            }

            // If any part fails, handle errors here
            TempData["error"] = "Error in Registration Flow. Please try again.";
            return RedirectToAction("Index");
        }




        //CHILD FORM
        public IActionResult AddNewChild()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddNewChild(ADStarter.Models.Child obj, string action, IFormFile? file, string userId)
        {
            userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var parent = _unitOfWork.Parent.GetFirstOrDefault(p => p.UserId == userId);
            string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    string c_photo = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string c_photoPath = Path.Combine(wwwRootPath, @"images\child");

                    using (var fileStream = new FileStream(Path.Combine(c_photoPath, c_photo), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    obj.c_photo = @"\images\child\" + c_photo;
                }
                if (obj.c_nationality == "Other")
                {
                    obj.c_nationality = Request.Form["c_otherNationality"]; // Assign directly from form input
                }
            obj.parent_ID = parent.parent_ID;
                _unitOfWork.Child.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Child Detail created successfully";
                TempData["c_myKid"] = obj.c_myKid;

                return RedirectToAction("Index","Dashboard");
        }

        // TREATMENT HISTORY FORM
        public IActionResult AddNewTreatmentHistoryForm()
        {
            if (TempData["c_myKid"] != null)
            {
                ViewBag.cmyKid = TempData["c_myKid"];
                TempData.Keep("c_myKid");
            }
            return View();
        }

        [HttpPost]
        public IActionResult AddNewTreatmentHistoryForm(ADStarter.Models.TreatmentHistory obj, string submit)
        {
            if (TempData["c_myKid"] != null)
            {
                var c_myKid = (string)TempData["c_myKid"];
                obj.c_myKid = c_myKid;
                _unitOfWork.TreatmentHistory.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Treatment History Detail created successfully";

                if (submit == "Skip")
                {
                    return RedirectToAction("Index", "Dashboard");
                }
                return RedirectToAction("Index", "Dashboard");
            }

            TempData["error"] = "Child MyKid not found. Please try again.";
            return RedirectToAction("Index");
        }


        public IActionResult UserEditCopy()
        {
            var userId = _userManager.GetUserId(User);

            // Retrieve the parent record for the current user
            var parent = _unitOfWork.Parent.GetFirstOrDefault(p => p.UserId == userId);
            if (parent == null)
            {
                return NotFound(); // Parent not found, return 404
            }

            // Retrieve child records associated with the parent
            var child = _unitOfWork.Child.GetFirstOrDefault(c => c.parent_ID == parent.parent_ID);

            if (child == null)
            {
                return NotFound(); // Child not found for the parent, return 404
            }

            var treatmenthistory = _unitOfWork.TreatmentHistory.GetFirstOrDefault(th => th.c_myKid == child.c_myKid);

            // Prepare the view model
            var viewModel = new ParentChildViewModel
            {
                Parent = parent,
                Child = child,
                TreatmentHistory = treatmenthistory
            };

            return View(viewModel); // Pass the view model to the view
        }

        // EDIT
        public IActionResult UserEdit()
        {
            var userId = _userManager.GetUserId(User);

            // Retrieve the parent record for the current user
            var parent = _unitOfWork.Parent.GetFirstOrDefault(p => p.UserId == userId);
            if (parent == null)
            {
                return NotFound(); // Parent not found, return 404
            }

            // Retrieve child records associated with the parent
            var children = _unitOfWork.Child.GetAll().Where(c => c.parent_ID == parent.parent_ID).ToList();
            if (!children.Any())
            {
                return NotFound(); // No children found for the parent, return 404
            }

            // Retrieve treatment history records for each child
            var treatmentHistories = children.Select(child => new ParentChildViewModel
            {
                Child = child,
                TreatmentHistory = _unitOfWork.TreatmentHistory.GetFirstOrDefault(th => th.c_myKid == child.c_myKid)
            }).ToList();

            // Prepare the view model
            var viewModel = new ParentChildViewModel
            {
                Parent = parent,
                Children = treatmentHistories
            };

            ViewBag.UserId = userId;
            ViewBag.Parent = parent;
            return View(viewModel); // Pass the view model to the view
        }




        [HttpPost]
        public IActionResult UserEdit(ADStarter.Models.ParentChildViewModel obj, string action, IFormFile? file)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Retrieve parent, child, and treatment history entities
            var Parent = _unitOfWork.Parent.GetFirstOrDefault(p => p.UserId == userId);
            var Child = Parent != null ? _unitOfWork.Child.GetFirstOrDefault(c => c.parent_ID == Parent.parent_ID) : null;
            var History = Child != null ? _unitOfWork.TreatmentHistory.GetFirstOrDefault(th => th.c_myKid == Child.c_myKid) : null;

            // Update Parent information if Parent is not null
            if (obj.Parent != null)
            {
                Parent.f_ID = obj.Parent.f_ID;
                Parent.m_ID = obj.Parent.m_ID;
                Parent.f_name = obj.Parent.f_name;
                Parent.f_phoneNum = obj.Parent.f_phoneNum;
                Parent.f_race = obj.Parent.f_race;
                Parent.f_address = obj.Parent.f_address;
                Parent.f_Waddress = obj.Parent.f_Waddress;
                Parent.f_email = obj.Parent.f_email;
                Parent.f_occupation = obj.Parent.f_occupation;
                Parent.f_status = obj.Parent.f_status;
                Parent.m_name = obj.Parent.m_name;
                Parent.m_phoneNum = obj.Parent.m_phoneNum;
                Parent.m_race = obj.Parent.m_race;
                Parent.m_address = obj.Parent.m_address;
                Parent.m_Waddress = obj.Parent.m_Waddress;
                Parent.m_email = obj.Parent.m_email;
                Parent.m_status = obj.Parent.m_status;
                Parent.fm_income = obj.Parent.fm_income;
                _unitOfWork.Parent.Update(Parent);
                _unitOfWork.Save();
                TempData["successParent"] = "Parent Detail updated successfully";
            }
            else
            {
                TempData["errorParent"] = "Parent not found.";
            }

            // Update Child information if Child is not null
            if (obj.Child != null)
            {
                Child.parent_ID = Parent.parent_ID; // Assuming parent_ID is correctly set after parent update
                Child.c_name = obj.Child.c_name;
                Child.c_age = obj.Child.c_age;
                Child.c_gender = obj.Child.c_gender;
                Child.c_dob = obj.Child.c_dob;
                Child.c_nationality = obj.Child.c_nationality;
                Child.c_religion = obj.Child.c_religion;
                Child.c_race = obj.Child.c_race;
                Child.c_step = obj.Child.c_step;
                _unitOfWork.Child.Update(Child);
                _unitOfWork.Save();
                TempData["successChild"] = "Child Detail updated successfully";
            }
            else
            {
                TempData["errorChild"] = "Child not found.";
            }

            // Update Treatment History information if History is not null
            if (obj.TreatmentHistory != null)
            {
                History.c_myKid = Child.c_myKid; // Assuming c_myKid is correctly set after child update
                History.th_deadline = obj.TreatmentHistory.th_deadline;
                History.th_recommendation = obj.TreatmentHistory.th_recommendation;
                History.th_diagnosis = obj.TreatmentHistory.th_diagnosis;
                History.th_pediatrician = obj.TreatmentHistory.th_pediatrician;
                History.th_prevTherapy = obj.TreatmentHistory.th_prevTherapy;
                _unitOfWork.TreatmentHistory.Update(History);
                _unitOfWork.Save();
                TempData["successTreatmentHistory"] = "Treatment History Detail updated successfully";
            }
            else
            {
                TempData["errorTreatmentHistory"] = "Treatment History not found.";
            }

            // Handle file upload if applicable (for c_photo)
            if (file != null && Child != null)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                string c_photo = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                string c_photoPath = Path.Combine(wwwRootPath, @"images\child");

                // Ensure the directory exists
                if (!Directory.Exists(c_photoPath))
                {
                    Directory.CreateDirectory(c_photoPath);
                }

                try
                {
                    using (var fileStream = new FileStream(Path.Combine(c_photoPath, c_photo), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    Child.c_photo = @"\images\child\" + c_photo; // Update c_photo path
                    _unitOfWork.Child.Update(Child);
                    _unitOfWork.Save();
                }
                catch (Exception ex)
                {
                    // Handle file upload error
                    ModelState.AddModelError("", "File upload failed: " + ex.Message);
                    return RedirectToAction("Index", "Dashboard");
                }
            }

            return RedirectToAction("Index", "Dashboard");
        }



        // DELETE PARENT
        public IActionResult Delete(int? parent_ID)
        {
            if (parent_ID == null || parent_ID == 0)
            {
                return NotFound();
            }
            ADStarter.Models.Parent? parentFromDb = _unitOfWork.Parent.Get(u => u.parent_ID == parent_ID);
            if (parentFromDb == null)
            {
                return NotFound();
            }
            return View(parentFromDb);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? parent_ID)
        {
            ADStarter.Models.Parent? obj = _unitOfWork.Parent.Get(u => u.parent_ID == parent_ID);
            if (obj == null)
            {
                return NotFound();
            }
            _unitOfWork.Parent.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "ParentDetail deleted successfully";
            return RedirectToAction("Index");
        }
    }
}


//// TREATMENT HISTORY FORM
//public IActionResult TreatmentHistoryForm()
//{
//    if (TempData["c_myKid"] != null)
//    {
//        ViewBag.cmyKid = TempData["c_myKid"];
//        TempData.Keep("c_myKid");
//    }
//    return View();
//}

//[HttpPost]
//public IActionResult TreatmentHistoryForm(ADStarter.Models.TreatmentHistory obj, string submit)
//{
//    if (TempData["c_myKid"] != null)
//    {
//        var c_myKid = (string)TempData["c_myKid"];
//        obj.c_myKid = c_myKid;
//        _unitOfWork.TreatmentHistory.Add(obj);
//        _unitOfWork.Save();
//        TempData["success"] = "Treatment History Detail created successfully";

//        if (submit == "Skip")
//        {
//            return RedirectToAction("Index", "Dashboard");
//        }
//        return RedirectToAction("Index", "Dashboard");
//    }

//    TempData["error"] = "Child MyKid not found. Please try again.";
//    return RedirectToAction("Index");
//}

// PARENT FORM
//public IActionResult ParentForm()
//{
//    return View();
//}

//[HttpPost]
//public IActionResult ParentForm(ADStarter.Models.Parent obj)
//{
//    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
//    obj.UserId = userId;
//    _unitOfWork.Parent.Add(obj);
//    _unitOfWork.Save();
//    TempData["success"] = "Parent Detail created successfully";
//    TempData["parent_ID"] = obj.parent_ID;
//    return RedirectToAction("ChildForm");
//}