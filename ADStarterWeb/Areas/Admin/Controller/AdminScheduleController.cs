using ADStarter.DataAccess.Repository.IRepository;
using ADStarter.Models;
using ADStarter.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ADStarterWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminScheduleController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public AdminScheduleController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: /Admin/Schedule
        public IActionResult Index()
        {
            var children = _unitOfWork.Child.GetAll(); // Retrieve all children
            var viewModel = new List<ChildScheduleViewModel>();

            foreach (var child in children)
            {
                var schedules = _unitOfWork.Schedule.GetAll(s => s.c_myKid == child.c_myKid && s.session_datetime >= DateTime.Today)
                                                     .OrderBy(s => s.session_datetime)
                                                     .ToList();

                foreach (var schedule in schedules)
                {
                    var slot = _unitOfWork.Slot.GetFirstOrDefault(sl => sl.slot_ID == schedule.slot_ID);
                    var program = _unitOfWork.Program.GetFirstOrDefault(p => p.prog_ID == schedule.prog_ID);
                    var therapist = _unitOfWork.Therapist.GetFirstOrDefault(t => t.t_ID == child.t_ID);

                    viewModel.Add(new ChildScheduleViewModel
                    {
                        TherapistName = therapist?.t_name ?? "Not Assigned",
                        ChildName = child.c_name,
                        SessionDate = schedule.session_datetime,
                        SlotTime = slot.slot_time.ToString(),
                        ProgramName = program.prog_name,
                        ScheduleId = schedule.schedule_ID // Include ScheduleId for edit/delete actions
                    });
                }
            }

            return View("~/Areas/Admin/Views/AdminSchedule/Index.cshtml", viewModel);
        }

        // GET: /Admin/Schedule/Manage/{id}
        public IActionResult Manage(int id)
        {
            var schedule = _unitOfWork.Schedule.GetFirstOrDefault(s => s.schedule_ID == id);
            if (schedule == null)
            {
                return NotFound();
            }

            // Optionally, you can perform additional operations here if needed before rendering the view

            return View("~/Areas/Admin/Views/AdminSchedule/Manage.cshtml", schedule);
        }

        // GET: /Admin/Schedule/Edit/{id}
        public IActionResult Edit(int id)
        {
            var schedule = _unitOfWork.Schedule.GetFirstOrDefault(s => s.schedule_ID == id);
            if (schedule == null)
            {
                return NotFound();
            }

            return View("~/Areas/Admin/Views/AdminSchedule/Edit.cshtml", schedule);
        }

        // POST: /Admin/Schedule/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Schedule schedule)
        {
            if (ModelState.IsValid)
            {
                // Ensure session_date is later than today
                if (schedule.session_datetime.Date <= DateTime.Today)
                {
                    ModelState.AddModelError("session_datetime", "Session Date must be later than today.");
                    return View("~/Areas/Admin/Views/AdminSchedule/Edit.cshtml", schedule);
                }

                // Check if session_date is different from current date
                var existingSchedule = _unitOfWork.Schedule.GetFirstOrDefault(s => s.schedule_ID == schedule.schedule_ID);
                if (existingSchedule != null && existingSchedule.session_datetime.Date == schedule.session_datetime.Date)
                {
                    ModelState.AddModelError("session_datetime", "Session Date must be different from the current date.");
                    return View("~/Areas/Admin/Views/AdminSchedule/Edit.cshtml", schedule);
                }

                // Update only the session_date
                existingSchedule.session_datetime = schedule.session_datetime;
                _unitOfWork.Schedule.Update(existingSchedule);
                _unitOfWork.Save();

                TempData["Success"] = "Schedule updated successfully.";
                return RedirectToAction(nameof(Index));
            }

            return View("~/Areas/Admin/Views/AdminSchedule/Edit.cshtml", schedule);
        }


        // POST: /Admin/Schedule/Delete/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var schedule = _unitOfWork.Schedule.GetFirstOrDefault(s => s.schedule_ID == id);
            if (schedule == null)
            {
                return NotFound();
            }

            _unitOfWork.Schedule.Remove(schedule);
            _unitOfWork.Save();

            TempData["Success"] = "Schedule deleted successfully.";
            return RedirectToAction(nameof(Index));
        }
    }
}
