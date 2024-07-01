using ADStarter.DataAccess.Data;
using ADStarter.DataAccess.Repository.IRepository;
using ADStarter.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ADStarterWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TherapistAdminController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ApplicationDBContext _context; // Assuming your DbContext is named AppDbContext
        public TherapistAdminController(IUnitOfWork unitOfWork, ApplicationDBContext context)
        {
            _unitOfWork = unitOfWork;
            _context = context;
        }

        public IActionResult Index()
        {
            var therapists = _unitOfWork.Therapist.GetAll().ToList();
            return View(therapists);
        }

        public IActionResult ViewTherapistSchedule(int id)
        {
            var therapist = _unitOfWork.Therapist.GetFirstOrDefault(t => t.t_ID == id);

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
