using Microsoft.AspNetCore.Mvc;
using ADStarter.Models.ViewModels;
using ADStarter.DataAccess.Data;
using ADStarter.Models;
using System.Security.Claims;
using ADStarter.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Identity;
using ADStarter.DataAccess.Repository.IRepository;
using ADStarter.Models;
using ADStarter.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ADStarterWeb.Areas.Parent.Controllers
{
    [Area("Parent")]
    public class RegistrationController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public RegistrationController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult FatherForm()
        {
           
            return View();
        }


            public IActionResult Create()
            {
                return View();
            }
        [HttpPost]
        public IActionResult Create(ADStarter.Models.Parent obj)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            obj.User = new IdentityUser { Id = userId };

            _unitOfWork.Parent.Add(obj);
            _unitOfWork.Save();
            TempData["success"] = "Parent Detail created successfully";
            return RedirectToAction("ChildForm");
        }

        public IActionResult Edit(int? parent_ID)
            {
                if (parent_ID == null || parent_ID == 0)
                {
                    return NotFound();
                }
            ADStarter.Models.Parent? ParentFromDb = _unitOfWork.Parent.Get(u => u.parent_ID == parent_ID);
                //Category? categoryFromDb1 = _db.Categories.FirstOrDefault(u=>u.Id==id);
                //Category? categoryFromDb2 = _db.Categories.Where(u=>u.Id==id).FirstOrDefault();

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
                obj.User = new IdentityUser { Id = userId };
                _unitOfWork.Parent.Update(obj);
                    _unitOfWork.Save();
                    TempData["success"] = "ParentDetail updated successfully";
                    return RedirectToAction("Index");
                }
                return View();

            }

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
