using ADStarter.DataAccess.Data;
using ADStarter.Models.ViewModels;
using ADStarter.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ADStarterWeb.Areas.CustomerService.Controllers
{
    [Area("CustomerService")]
    [Authorize(Roles = SD.Role_Customer_service)]
    public class ReportController : Controller
    {
        private readonly ApplicationDBContext _context; // Assuming your DbContext is named ApplicationDBContext

        public ReportController(ApplicationDBContext context)
        {
            _context = context;
        }

        public IActionResult PendingReports()
        {
            var pendingReports = _context.Reports
                .Include(r => r.Schedule)
                .Where(r => r.Schedule.prog_ID == 1 && r.rep_status == "Pending")
                // not a good code sepatutnya c_step = 1 bukan program is=1;
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
                return NotFound("Pending report not found for the given schedule.");
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
                var existingReport = await _context.Reports.FirstOrDefaultAsync(r => r.rep_ID == model.ReportId);
                if (existingReport == null)
                {
                    return NotFound("Report not found.");
                }

                existingReport.rep_title = model.ReportTitle;
                existingReport.rep_datetime = model.ReportDateTime;
                existingReport.rep_remark = model.ReportRemark;

                if (model.ReportFile != null && model.ReportFile.Length > 0)
                {
                    string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(model.ReportFile.FileName);
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await model.ReportFile.CopyToAsync(fileStream);
                    }

                    existingReport.rep_file = uniqueFileName;
                }

                existingReport.rep_status = "Approved";

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(PendingReports));
            }

            return View(model);
        }
    }
}
