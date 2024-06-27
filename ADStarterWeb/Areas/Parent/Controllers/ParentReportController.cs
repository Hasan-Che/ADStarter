using ADStarter.DataAccess.Data;
using ADStarter.Models;
using ADStarter.Models.ViewModels;
using ADStarter.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ADStarterWeb.Areas.Parent.Controllers
{
    [Area("Parent")]
    [Authorize(Roles = SD.Role_Parent)]
    public class ParentReportController : Controller
    {
        private readonly ApplicationDBContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public ParentReportController(ApplicationDBContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<IActionResult> ChildReport()
        {
            var userId = _userManager.GetUserId(User);
            var parent = await _context.Parents.FirstOrDefaultAsync(p => p.UserId == userId);

            if (parent == null)
            {
                return NotFound("Parent not found.");
            }

            var childReports = await _context.Schedules
                .Include(s => s.Program)
                .Include(s => s.Child)
                .Include(s => s.Reports)
                .Where(s => s.Child.parent_ID == parent.parent_ID)
                .SelectMany(s => s.Reports.Select(r => new ParentReportViewModel
                {
                    ScheduleId = s.schedule_ID,
                    ChildName = s.Child.c_name,
                    SessionDateTime = s.session_datetime,
                    ProgramName = s.Program.prog_name,
                    SlotTime = s.Slot.slot_time,
                    ReportFile = r.rep_file,
                    ReportRemark = r.rep_remark,
                       ReportStatus = r.rep_status
                }))
                .ToListAsync();

            return View(childReports);
        }
        [HttpGet]
        public IActionResult DownloadReport(string fileName)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", fileName);

            try
            {
                if (System.IO.File.Exists(filePath))
                {
                    byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);
                    return File(fileBytes, "application/pdf", fileName);
                }
                else
                {
                    return NotFound($"File '{fileName}' not found.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error retrieving file '{fileName}': {ex.Message}");
            }
        }
        }
}
