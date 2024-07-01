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
                _unitOfWork.Save(); // Save the schedule first to get the generated ID

                // Create the corresponding report
                CreateReportForSchedule(schedule);

                // Create the corresponding invoice
                CreateInvoiceForSchedule(schedule);
            }

            TempData["Success"] = "Schedules created successfully.";
            return RedirectToAction(nameof(Create));
        }


        private void CreateReportForSchedule(Schedule schedule)
        {
            var report = new Report
            {
                rep_title = null,
                rep_datetime = DateTime.Now,
                schedule_ID = schedule.schedule_ID,
                rep_remark = null,
                rep_file = null, // Assuming no file associated at creation time
                rep_status = "Pending"
            };

            _unitOfWork.Report.Add(report);
            _unitOfWork.Save();
        }



        private void CreateInvoiceForSchedule(Schedule schedule)
        {
            // Determine the due date (30 days after session_datetime)
            DateTime dueDate = schedule.session_datetime.AddDays(30);

            // Fetch the program to determine the invoice amount
            var program = _unitOfWork.Program.GetFirstOrDefault(p => p.prog_ID == schedule.prog_ID);
            if (program == null)
            {
                // Handle the error case where the program is not found
                throw new Exception("Program not found");
            }

            // Determine if the session date is a weekend or weekday
            double invoiceAmount;
            if (schedule.session_datetime.DayOfWeek == DayOfWeek.Friday || schedule.session_datetime.DayOfWeek == DayOfWeek.Saturday)
            {
                invoiceAmount = program.prog_WeekendPrice/program.prog_sessionCount;
            }
            else
            {
                invoiceAmount = program.prog_WeekdayPrice/program.prog_sessionCount;
            }

            // Create the invoice object
            Invoice invoice = new Invoice
            {
                c_myKid = schedule.c_myKid,
                schedule_ID = schedule.schedule_ID,
                due_date = dueDate,
                invoice_amount = invoiceAmount
            };

            // Add the new invoice to the database
            _unitOfWork.Invoice.Add(invoice);
            _unitOfWork.Save();
        }

        // GET: Schedule/ViewChildSchedule
        public IActionResult ViewChildSchedule()
        {
            var userId = _userManager.GetUserId(User);
            var parent = _unitOfWork.Parent.GetFirstOrDefault(p => p.UserId == userId);

            if (parent == null)
            {
                return NotFound("Parent not found.");
            }

            var children = _unitOfWork.Child.GetAll(c => c.parent_ID == parent.parent_ID).ToList();
            var scheduleList = new List<ChildScheduleViewModel>();
            var today = DateTime.Today;

            foreach (var child in children)
            {
                var schedules = _unitOfWork.Schedule.GetAll(s => s.c_myKid == child.c_myKid && s.session_datetime >= today)
                                                     .OrderBy(s => s.session_datetime)
                                                     .ToList();

                foreach (var schedule in schedules)
                {
                    var slot = _unitOfWork.Slot.GetFirstOrDefault(sl => sl.slot_ID == schedule.slot_ID);
                    var program = _unitOfWork.Program.GetFirstOrDefault(p => p.prog_ID == schedule.prog_ID);
                    var therapist = _unitOfWork.Therapist.GetFirstOrDefault(t => t.t_ID == child.t_ID);

                    scheduleList.Add(new ChildScheduleViewModel
                    {
                        TherapistName = therapist?.t_name ?? "Not Assigned",
                        ChildName = child.c_name,
                        SessionDate = schedule.session_datetime,
                        SlotTime = slot?.slot_time.ToString(),
                        ProgramName = program?.prog_name
                    });
                }
            }

            return View(scheduleList);
        }
    }
}
