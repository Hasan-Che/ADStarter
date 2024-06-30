//using ADStarter.DataAccess.Data;
//using ADStarter.Models.ViewModels;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;

//namespace ADStarterWeb.Areas.Therapist.Controllers
//{
//    [Area("Therapist")]
//    public class TherapistController : Controller

//    {
//        private readonly ApplicationDBContext _context;

//        public TherapistController(ApplicationDBContext context)
//        {
//            _context = context;
//        }

//        public async Task<IActionResult> TherapistSchedule(int therapistId)
//        {
//            var today = DateTime.Today;

//            // Retrieve therapist with related schedules, children, slots, and programs
//            var therapist = await _context.Therapists
//                .Include(t => t.Schedules).ThenInclude(s => s.Child)
//                .Include(t => t.Schedules).ThenInclude(s => s.Slot)
//                .Include(t => t.Schedules).ThenInclude(s => s.Program)
//                /*.Where(t => t.t_ID == therapistId)*/
//                .Where(t => t.t_ID == 2)
//                .FirstOrDefaultAsync();

//            if (therapist == null)
//            {
//                return NotFound();
//            }

//            // Filter schedules to show only today and future sessions, map to view model
//            var schedules = therapist.Schedules
//                .Where(s => s.session_datetime >= today)
//                .OrderBy(s => s.session_datetime)
//                .Select(s => new TherapistScheduleViewModel
//                {
//                    ChildName = s.Child.c_name,
//                    SessionDateTime = s.session_datetime,
//                    SlotTime = s.Slot.slot_time,
//                    ProgramName = s.Program.prog_name,
//                    SlotPrice = s.slot_price
//                })
//                .ToList();

//            return View("TherapistSchedule", schedules);
//        }
//    }
//}
