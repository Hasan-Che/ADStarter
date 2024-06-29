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
            var child = _unitOfWork.Child.GetFirstOrDefault(c => c.parent_ID == parent.parent_ID);
            string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    string c_photo = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string c_photoPath = Path.Combine(wwwRootPath, @"images\child");

                    using (var fileStream = new FileStream(Path.Combine(c_photoPath, c_photo), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    obj.c_photo = @"\images\product\" + c_photo;
                }
                var parent_ID = (int)TempData["parent_ID"];
                obj.parent_ID = parent_ID;

                if (action == "Skip")
                {
                    return RedirectToAction("Index", "Dashboard");
                }

                _unitOfWork.Child.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Child Detail created successfully";
                TempData["c_myKid"] = obj.c_myKid;

                return RedirectToAction("TreatmentHistoryForm");
            

            TempData["error"] = "Parent ID not found. Please try again.";
            return RedirectToAction("Index");
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
            ViewBag.UserId = userId;
            ViewBag.Parent = parent;
            return View(viewModel); // Pass the view model to the view
        }


        [HttpPost]
        public IActionResult UserEdit(ParentChildViewModel model, string action, IFormFile? file, string userId)
        {
            userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var parent = _unitOfWork.Parent.GetFirstOrDefault(p => p.UserId == userId);
            var child = _unitOfWork.Child.GetFirstOrDefault(c => c.parent_ID == parent.parent_ID);
            var treatmenthistory = _unitOfWork.TreatmentHistory.GetFirstOrDefault(th => th.c_myKid == child.c_myKid);

            // Save Parent information
            parent.UserId = userId;
            _unitOfWork.Parent.Update(parent);
            _unitOfWork.Save();
            TempData["success"] = "Parent Detail created successfully";

            // Save Child information
            string wwwRootPath = _webHostEnvironment.WebRootPath;
            if (file != null)
            {
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
                    child.c_photo = @"\images\child\" + c_photo;
                }
                catch (Exception ex)
                {
                    // Handle the error
                    ModelState.AddModelError("", "File upload failed: " + ex.Message);
                    return View(model);
                }
            }

            child.parent_ID = parent.parent_ID; // Assuming parent_ID is set after save
            _unitOfWork.Child.Update(child);
            _unitOfWork.Save();
            TempData["success"] = "Child Detail created successfully";

            // Save Treatment History information
            treatmenthistory.c_myKid = child.c_myKid; // Assuming c_myKid is set after save
            _unitOfWork.TreatmentHistory.Update(treatmenthistory);
            _unitOfWork.Save();
            TempData["success"] = "Treatment History Detail created successfully";

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
