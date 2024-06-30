using ADStarter.DataAccess.Data;
using ADStarter.DataAccess.Migrations;
using ADStarter.DataAccess.Repository.IRepository;
using ADStarter.Models;
using ADStarter.Models.ViewModels;
using ADStarter.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ADStarterWeb.Areas.Admin.Controller
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class AnnouncementController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ApplicationDBContext _db;

        public AnnouncementController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment, UserManager<IdentityUser> userManager, ApplicationDBContext db)
        {
            _webHostEnvironment = webHostEnvironment;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _db = db;
        }
        public IActionResult Manage()
        {
            List<Announcement> announcement = _unitOfWork.Announcement.GetAll().ToList();
            return View(announcement);
        }
        public IActionResult Test()
        {
            List<Announcement> announcement = _unitOfWork.Announcement.GetAll().ToList();
            return View(announcement);
        }
        public IActionResult Upsert(int? userId)
        {
            ViewBag.UserId = userId;
            var ann = new Announcement();
            if (userId == null || userId == 0)
            {
                return View(ann);
            }
            else
            {
                ann = _unitOfWork.Announcement.Get(u => u.ann_ID == userId);
                return View(ann);
            }
            return View(ann);
        }

        [HttpPost]
        public async Task<IActionResult> Upsert(Announcement obj, IFormFile? file, int userId)
        {
            //if (obj.Name != null && obj.Name.ToLower() == "test")
            //{
            //	ModelState.AddModelError("", "Test is an invalid value");
            //}
            
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                var adminDetails = _unitOfWork.Admin.Get(filter: a => a.User.Id == user.Id);
                var announcement = _unitOfWork.Announcement.Get(filter: ann => ann.ann_ID == userId);
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string productPath = Path.Combine(wwwRootPath, @"images\announcement");
                    if (!string.IsNullOrEmpty(obj.ImageUrl))
                    {
                        //delete the original file
                        var oldImagePath = Path.Combine(wwwRootPath, obj.ImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }
                    using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    obj.ImageUrl = @"\images\announcement\" + fileName;
                }
                if (announcement == null)
                {
                    obj.a_ID = adminDetails.a_ID;
                    _unitOfWork.Announcement.Add(obj);
                }
                else
                {
                    announcement.ann_title = obj.ann_title;
                    announcement.ann_status = obj.ann_status;
                    announcement.ann_desc = obj.ann_desc;
                    obj.a_ID = adminDetails.a_ID;
                    //_db.Entry(obj).State = EntityState.Detached;
                    _unitOfWork.Announcement.Update(obj);
                }
                _unitOfWork.Save();
                TempData["success"] = "Product added successfully";
                return RedirectToAction("Manage");
            }
            else
            {
                return View();
            }


        }
        #region API CALLS

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var announcementToBeDeleted = _unitOfWork.Announcement.Get(u => u.ann_ID == id);
            if (announcementToBeDeleted == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            var oldImagePath =
                           Path.Combine(_webHostEnvironment.WebRootPath,
                           announcementToBeDeleted.ImageUrl.TrimStart('\\'));

            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }

            _unitOfWork.Announcement.Remove(announcementToBeDeleted);
            _unitOfWork.Save();

            return Json(new { success = true, message = "Delete Successful" });
        }
        #endregion
    }
}
