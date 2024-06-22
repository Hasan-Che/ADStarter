using ADStarter.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ADStarterWeb.Areas.Parent.Controllers
{
    public class RegistrationController : Controller
    {
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
                TempData["ParentData"] = model;
                return RedirectToAction("ChildForm");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult ChildForm()
        {
            return View(new ChildViewModel());
        }

        [HttpPost]
        public IActionResult ChildForm(ChildViewModel model, string action)
        {
            if (action == "skip")
            {
                return RedirectToAction("Index", "Dashboard");
            }

            if (ModelState.IsValid)
            {
                TempData["ChildData"] = model;
                return RedirectToAction("TreatmentHistoryForm");
            }

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

                return RedirectToAction("Index", "Dashboard");
            }

            return View(model);
        }
    }
}

