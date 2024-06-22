using ADStarter.DataAccess.Data;
using ADStarter.Models;
using ADStarter.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ADStarterWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ChildScheduleAdminController : Controller
    {
        private readonly ApplicationDBContext _context;

        public ChildScheduleAdminController(ApplicationDBContext context)
        {
            _context = context;
        }


        // GET: Admin/ChildScheduleAdmin/SelectChild
        public IActionResult SelectChild()
        {
            List<ChildListViewModel> model = _context.Children
                .Select(child => new ChildListViewModel
                {
                    ChildId = child.c_myKid,
                    ChildName = child.c_name
                })
                .ToList();

            return View(model);
        }

        // GET: Admin/ChildScheduleAdmin/ViewSchedule?childId={childId}
        public async Task<IActionResult> ViewSchedule(int? childId)
        {
            if (childId == null)
            {
                return NotFound();
            }

            var child = await _context.Children.FindAsync(childId);
            if (child == null)
            {
                return NotFound();
            }

            var schedules = await _context.Schedules
                .Include(s => s.SessionPrice)
                .Include(s => s.Therapist)
                .Include(s => s.Program)
                .Include(s => s.Slot)
                .Where(s => s.c_myKid == childId && s.session_datetime.Date >= DateTime.Today)
                .OrderBy(s => s.session_datetime)
                .Select(s => new ScheduleViewModel
                {
                    Child_ID = s.c_myKid,
                    SessionDate = s.session_datetime,
                    Slot_ID = s.slot_ID,
                    Session_ID = s.schedule_ID,
                    ChildName = child.c_name,
                    SlotTime = s.Slot.slot_time,
                    ProgramName = s.Program.prog_name,
                    TherapistName = s.Therapist.t_name
                })
                .ToListAsync();

            var viewModel = new ChildScheduleDisplayViewModel
            {
                ChildId = child.c_myKid,
                ChildName = child.c_name,
                Schedules = schedules
            };

            return View(viewModel);
        }


        // Add more actions as needed for CRUD operations or other functionalities

    }
}
