using ADStarter.DataAccess.Data; // Replace with your DbContext namespace
using ADStarter.Models; // Replace with your entity namespace
using ADStarter.Models.ViewModels;
using ADStarter.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ADStarterWeb.Areas.Therapist.Controllers
{
    [Area("Therapist")]
    [Authorize(Roles = SD.Role_Therapist)]
    public class ReportController : Controller
    {
        private readonly ApplicationDBContext _context; // Assuming your DbContext is named AppDbContext

        public ReportController(ApplicationDBContext context)
        {
            _context = context;
        }

        public IActionResult PendingReports()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Get current user's ID
            var therapist = _context.Therapists.FirstOrDefault(t => t.User.Id == userId);

            if (therapist == null)
            {
                return NotFound("Therapist not found.");
            }

            // Retrieve pending reports for the therapist
            var pendingReports = _context.Reports
                .Include(r => r.Schedule)
                .Where(r => r.Schedule.t_ID == therapist.t_ID && r.rep_status == "Pending")
                .ToList();

            var viewModelList = pendingReports.Select(report => new PendingReportViewModel
            {
                ChildName = _context.Children.FirstOrDefault(c => c.c_myKid == report.Schedule.c_myKid)?.c_name,
                ReportDateTime = report.rep_datetime,
                ProgramName = _context.Programs.FirstOrDefault(p => p.prog_ID == report.Schedule.prog_ID)?.prog_name,
                SlotTime = _context.Slots.FirstOrDefault(s => s.slot_ID == report.Schedule.slot_ID)?.slot_time,
                ScheduleId = report.schedule_ID
            }).ToList();

            return View(viewModelList);
        }

        [HttpGet]
        public IActionResult Update(int scheduleId)
        {
            var existingReport = _context.Reports.FirstOrDefault(r => r.schedule_ID == scheduleId && r.rep_status == "Pending");

            if (existingReport == null)
            {
                return NotFound("Pending report not found for the given schedule."); // Handle if pending report is not found
            }

            var viewModel = new UpdateReportViewModel
            {
                ReportId = existingReport.rep_ID,
                ScheduleId = existingReport.schedule_ID,
                ReportTitle = existingReport.rep_title,
                ReportDateTime = existingReport.rep_datetime,
                ReportRemark = existingReport.rep_remark
               
            };

            return View(viewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(UpdateReportViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Retrieve the existing report based on ReportId
                var existingReport = await _context.Reports.FirstOrDefaultAsync(r => r.rep_ID == model.ReportId);
                if (existingReport == null)
                {
                    return NotFound("Report not found."); // Handle if report is not found
                }

                // Update the report properties with new values
                existingReport.rep_title = model.ReportTitle;
                existingReport.rep_datetime = model.ReportDateTime;
                existingReport.rep_remark = model.ReportRemark;

                // Handle file upload if there's a new file
                if (model.ReportFile != null && model.ReportFile.Length > 0)
                {
                    string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(model.ReportFile.FileName);
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await model.ReportFile.CopyToAsync(fileStream);
                    }

                    // Update the report's file name
                    existingReport.rep_file = uniqueFileName;
                }

                // Update the report status if needed
                existingReport.rep_status = "Approved"; // Uncomment this if status update is necessary

                // Save changes to the database
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(PendingReports));
            }

            // If ModelState is not valid, return the view with the current model
            return View(model);
        }

        public IActionResult UpcomingSchedules()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var therapist = _context.Therapists.FirstOrDefault(t => t.User.Id == userId);

            if (therapist == null)
            {
                return NotFound("Therapist not found.");
            }

            var upcomingSchedules = _context.Schedules
                .Include(s => s.Child)
                .Include(s => s.Program)
                .Include(s => s.Slot)
                .Where(s => s.t_ID == therapist.t_ID && s.session_datetime > DateTime.Now)
                .Select(s => new TherapistChildScheduleViewModel
                {
                    ChildName = s.Child.c_name,
                    ProgramName = s.Program.prog_name,
                    SessionDateTime = s.session_datetime,
                    SlotTime = s.Slot.slot_time
                })
                .ToList();

            return View(upcomingSchedules);
        }
    }
}
