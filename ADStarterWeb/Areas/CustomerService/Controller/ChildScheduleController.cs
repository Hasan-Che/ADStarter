using ADStarter.DataAccess.Repository.IRepository;
using ADStarter.Models.ViewModels;
using ADStarter.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ADStarter.Utility;
using Microsoft.AspNetCore.Authorization;

namespace ADStarterWeb.Areas.CustomerService.Controllers
{
    [Area("CustomerService")]
    [Authorize(Roles = SD.Role_Customer_service)]
    public class ChildScheduleController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<IdentityUser> _userManager;

        public ChildScheduleController(IUnitOfWork unitOfWork, UserManager<IdentityUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public IActionResult ViewChildSchedule()
        {
            var today = DateTime.Today;
            var scheduleList = new List<ChildScheduleViewModel>();

            var schedules = _unitOfWork.Schedule.GetAll(s => s.prog_ID == 1 && s.session_datetime >= today)
                                                 .OrderBy(s => s.session_datetime)
                                                 .ToList();

            foreach (var schedule in schedules)
            {
                var child = _unitOfWork.Child.GetFirstOrDefault(c => c.c_myKid == schedule.c_myKid);
                var slot = _unitOfWork.Slot.GetFirstOrDefault(sl => sl.slot_ID == schedule.slot_ID);
                var program = _unitOfWork.Program.GetFirstOrDefault(p => p.prog_ID == schedule.prog_ID);
                var therapist = _unitOfWork.Therapist.GetFirstOrDefault(t => t.t_ID == child.t_ID);

                scheduleList.Add(new ChildScheduleViewModel
                {
                    TherapistName = therapist?.t_name ?? "Not Assigned",
                    ChildName = child?.c_name ?? "Unknown Child",
                    SessionDate = schedule.session_datetime,
                    SlotTime = slot?.slot_time.ToString(),
                    ProgramName = program?.prog_name
                });
            }

            return View(scheduleList);
        }
    }
}
