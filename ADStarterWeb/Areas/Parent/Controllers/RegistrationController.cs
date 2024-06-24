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
    [Authorize(Roles = SD.Role_Parent)]
    public class RegistrationController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public RegistrationController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // PARENT FORM
        public IActionResult ParentForm()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ParentForm(ADStarter.Models.Parent obj)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            obj.UserId = userId;
            _unitOfWork.Parent.Add(obj);
            _unitOfWork.Save();
            TempData["success"] = "Parent Detail created successfully";
            TempData["parent_ID"] = obj.parent_ID;
            return RedirectToAction("ChildForm");
        }

        // CHILD FORM
        public IActionResult ChildForm()
        {
            if (TempData["parent_ID"] != null)
            {
                ViewBag.ParentID = TempData["parent_ID"];
                TempData.Keep("parent_ID");
            }
            return View();
        }

        [HttpPost]
        public IActionResult ChildForm(ADStarter.Models.Child obj, string action)
        {
            if (TempData["parent_ID"] != null)
            {
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
            }

            TempData["error"] = "Parent ID not found. Please try again.";
            return RedirectToAction("Index");
        }


        // TREATMENT HISTORY FORM
        public IActionResult TreatmentHistoryForm()
        {
            if (TempData["c_myKid"] != null)
            {
                ViewBag.cmyKid = TempData["c_myKid"];
                TempData.Keep("c_myKid");
            }
            return View();
        }

        [HttpPost]
        public IActionResult TreatmentHistoryForm(ADStarter.Models.TreatmentHistory obj, string submit)
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

        // EDIT PARENT
        public IActionResult Edit(int? parent_ID)
        {
            if (parent_ID == null || parent_ID == 0)
            {
                return NotFound();
            }
            ADStarter.Models.Parent? ParentFromDb = _unitOfWork.Parent.Get(u => u.parent_ID == parent_ID);
            if (ParentFromDb == null)
            {
                return NotFound();
            }
            return View(ParentFromDb);
        }

        [HttpPost]
        public IActionResult Edit(ADStarter.Models.Parent obj)
        {
            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                obj.UserId = userId;
                _unitOfWork.Parent.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "ParentDetail updated successfully";
                return RedirectToAction("Index");
            }
            return View();
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
