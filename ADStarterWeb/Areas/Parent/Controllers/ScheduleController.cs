using ADStarter.DataAccess.Repository.IRepository;
using ADStarter.Models;
using ADStarter.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ADStarterWeb.Areas.Parent.Controllers
{
    [Area("Parent")]
    public class ScheduleController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<IdentityUser> _userManager;

        public ScheduleController(IUnitOfWork unitOfWork, UserManager<IdentityUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        // GET: Schedule/Create
        public async Task<IActionResult> Create()
        {
            var userId = _userManager.GetUserId(User);
            var parent = _unitOfWork.Parent.GetFirstOrDefault(p => p.UserId == userId);

            if (parent == null)
            {
                return NotFound("Parent not found.");
            }

            var children = _unitOfWork.Child.GetAll(c => c.parent_ID == parent.parent_ID).ToList();
            if (children == null || !children.Any())
            {
                return NotFound("No children found for the parent.");
            }

            return View(children);
        }

        // POST: Schedule/SelectProgram
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SelectProgram(string ChildId)
        {
            if (string.IsNullOrEmpty(ChildId))
            {
                ModelState.AddModelError("ChildId", "Please select a child.");
                var userId = _userManager.GetUserId(User);
                var parent = _unitOfWork.Parent.GetFirstOrDefault(p => p.UserId == userId);
                var children = _unitOfWork.Child.GetAll(c => c.parent_ID == parent.parent_ID).ToList();
                return View("Create", children);
            }

            var child = _unitOfWork.Child.GetFirstOrDefault(c => c.c_myKid == ChildId);
            if (child == null)
            {
                return NotFound("Child not found.");
            }

            var programs = _unitOfWork.Program.GetAll(p => p.prog_step == child.c_step).ToList();
            ViewBag.Programs = new SelectList(programs, "prog_ID", "prog_name");

            var viewModel = new ScheduleViewModel { ChildId = ChildId };
            return View("SelectProgram", viewModel);
        }

        // GET: Schedule/SelectDateAndSlot
        // GET: Schedule/SelectDateAndSlot
        //public IActionResult SelectDateAndSlot(int prog_ID, string childId)
        //{
        //    var program = _unitOfWork.Program.GetFirstOrDefault(p => p.prog_ID == prog_ID);
        //    if (program == null)
        //    {
        //        return NotFound();
        //    }

        //    var allSlots = _unitOfWork.Slot.GetAll().ToList();

        //    var viewModel = new CreateScheduleViewModel
        //    {
        //        ProgramId = prog_ID,
        //        ChildId = childId,
        //        AvailableSlots = allSlots.Select(s => new SlotViewModel
        //        {
        //            SlotId = s.slot_ID,
        //            SlotTime = s.slot_time.ToString()
        //        }).ToList(),
        //        ProgSessionCount = program.prog_sessionCount ,// Assign program session count to view model
        //          SlotIds = new List<int>() // Initialize SlotIds as an empty list
        //    };

        //    return View(viewModel);
        //}


        // GET: Schedule/SelectDateAndSlot
        public IActionResult SelectDateAndSlot(int prog_ID, string childId)
        {
            var program = _unitOfWork.Program.GetFirstOrDefault(p => p.prog_ID == prog_ID);
            if (program == null)
            {
                return NotFound();
            }

            var viewModel = new CreateScheduleViewModel
            {
                ProgramId = prog_ID,
                ChildId = childId,
                ProgSessionCount = program.prog_sessionCount // Assign program session count to view model
            };

            return View(viewModel);
        }




        // POST: Schedule/CreateSchedule
        // POST: Schedule/CreateSchedule
        // POST: Schedule/CreateSchedule
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateSchedule(CreateScheduleViewModel model)
        {
            var child = _unitOfWork.Child.GetFirstOrDefault(c => c.c_myKid == model.ChildId);
            if (child == null)
            {
                return NotFound("Child not found.");
            }

                for (int i = 0; i < model.SelectedDates.Count; i++)
                {
                    var schedule = new Schedule
                    {
                        t_ID = child.t_ID,
                        session_datetime = model.SelectedDates[i],
                        prog_ID = model.ProgramId,
                        slot_ID = model.SlotIds[i],
                        c_myKid = model.ChildId
                    };

                    _unitOfWork.Schedule.Add(schedule);
                }

                _unitOfWork.Save();
                TempData["Success"] = "Schedules created successfully.";
                return RedirectToAction(nameof(Create));
            

          
        }




        //}

        //// Repopulate AvailableSlots
        //var allSlots = _unitOfWork.Slot.GetAll().ToList();
        //model.AvailableSlots = allSlots.Select(s => new SlotViewModel
        //{
        //    SlotId = s.slot_ID,
        //    SlotTime = s.slot_time.ToString()
        //}).ToList();

        //// Repopulate SelectedDates if null or empty
        //if (model.SelectedDates == null || !model.SelectedDates.Any())
        //{
        //    var program = _unitOfWork.Program.GetFirstOrDefault(p => p.prog_ID == model.ProgramId);
        //    if (program != null)
        //    {
        //        model.SelectedDates = Enumerable.Range(0, program.prog_sessionCount)
        //                                        .Select(i => DateTime.Now.Date.AddDays(i))
        //                                        .ToList();
        //    }
        //}

        //return View("SelectDateAndSlot", model);



    }
}
