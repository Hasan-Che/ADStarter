using Microsoft.AspNetCore.Mvc;
using ADStarter.Models.ViewModels;
using ADStarter.DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using ADStarter.Models;
using ADStarter.Utility;
using Microsoft.AspNetCore.Authorization;

namespace ADStarterWeb.Areas.Parent.Controllers
{
    [Area("Parent")]
    [Authorize(Roles = SD.Role_Parent)]
    public class RegistrationController : Controller
    {
        private readonly ApplicationDBContext _db;

        private int _id = 11;

        public RegistrationController(ApplicationDBContext db)
        {
            _db = db; 
        }

        [HttpGet]
        public IActionResult ParentForm()
        {
            return View(new ParentViewModel());
        }

        [HttpPost]
        public IActionResult ParentForm(ParentViewModel model)
        {
            if (ModelState.IsValid)
            {
                var parent = new ADStarter.Models.Parent
                {
                    // Map properties from ParentViewModel to Parent entity
                    f_name = model.f_name,
                    f_phoneNum = model.f_phoneNum,
                    f_race = model.f_race,
                    f_address = model.f_address,
                    f_Waddress = model.f_Waddress,
                    f_email = model.f_email,
                    f_occupation = model.f_occupation,
                    f_status = model.f_status,
                    m_name = model.m_name,
                    m_phoneNum = model.m_phoneNum,
                    m_race = model.m_race,
                    m_address = model.m_address,
                    m_Waddress = model.m_Waddress,
                    m_email = model.m_email,
                    m_status = model.m_status,
                    fm_income = model.fm_income,
                };

                _db.Parents.Add(parent);
                _db.SaveChanges();

                TempData["ParentID"] = parent.parent_ID;
                TempData.Keep("ParentID");

                return RedirectToAction("ChildForm");
            }
            return View(model);
        }


        [HttpGet]
        public IActionResult ChildForm()
        {
            if (TempData["ParentID"] == null)
            {
                return RedirectToAction("ParentForm");
            }

            var parentID = (int)TempData["ParentID"];
            var childViewModel = new ChildViewModel
            {
                parent_ID = parentID,
                c_status = "Step One" // Set default value here
            };

            TempData.Keep("ParentID"); // Retain TempData for the next request
            return View(childViewModel);
        }

        [HttpPost]
        public IActionResult ChildForm(ChildViewModel model, string action)
        {
            if (action == "skip")
            {
                return RedirectToAction(nameof(Index), "Dashboard");
            }

            if (ModelState.IsValid)
            {
                var child = new Child
                {
                    // Map properties from ChildViewModel to Child entity
                    parent_ID = model.parent_ID,
                    c_myKid = model.c_myKid,
                    prog_ID = model.prog_ID,
                    c_name = model.c_name,
                    c_age = model.c_age,
                    c_gender = model.c_gender,
                    c_dob = model.c_dob,
                    c_nationality = model.c_nationality,
                    c_religion = model.c_religion,
                    c_race = model.c_race,
                    c_status = model.c_status
                };

                _db.Children.Add(child);
                _db.SaveChanges();

                TempData["ChildData"] = child;
                return RedirectToAction("TreatmentHistoryForm");
            }

            // Log ModelState errors for debugging
            foreach (var modelState in ModelState.Values)
            {
                foreach (var error in modelState.Errors)
                {
                    Console.WriteLine(error.ErrorMessage);
                }
            }

            TempData.Keep("ParentID"); // Retain TempData for the next request
            return View(model);
        }

        [HttpGet]
        public IActionResult TreatmentHistoryForm()
        {
            return View(new TreatmentHistoryViewModel());
        }

        [HttpPost]
        public IActionResult TreatmentHistoryForm(TreatmentHistoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                var parentData = TempData["ParentData"] as ParentViewModel;
                var childData = TempData["ChildData"] as ChildViewModel;

                // Save Parent, Child, and TreatmentHistory data to the database
                // Your save logic here

                return RedirectToAction(nameof(Index), "Dashboard");
            }

            return View(model);
        }
    }
}
