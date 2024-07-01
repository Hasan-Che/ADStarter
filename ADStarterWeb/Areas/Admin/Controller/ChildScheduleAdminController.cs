//using ADStarter.DataAccess.Data;
//using ADStarter.Models;
//using ADStarter.Models.ViewModels;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace ADStarterWeb.Areas.Admin.Controllers
//{
//    [Area("Admin")]
//    public class ChildScheduleAdminController : Controller
//    {
//        private readonly ApplicationDBContext _context;

//        public ChildScheduleAdminController(ApplicationDBContext context)
//        {
//            _context = context;
//        }

//        // GET: Admin/ChildScheduleAdmin/SelectChild
//        public IActionResult SelectChild()
//        {
//            List<ChildListViewModel> model = _context.Children
//                .Select(child => new ChildListViewModel
//                {
//                    ChildId = child.c_myKid,
//                    ChildName = child.c_name
//                })
//                .ToList();

//            return View(model);
//        }

//        // GET: Admin/ChildScheduleAdmin/ViewSchedule?childId={childId}
//        public async Task<IActionResult> ViewSchedule(int? childId)
//        {
//            if (childId == null)
//            {
//                return NotFound();
//            }

//            var child = await _context.Children.FindAsync(childId);
//            if (child == null)
//            {
//                return NotFound();
//            }

//            var schedules = await _context.Schedules
//                .Include(s => s.SessionPrice)
//                .Include(s => s.Therapist)
//                .Include(s => s.Program)
//                .Include(s => s.Slot)
//                .Where(s => s.c_myKid == childId && s.session_datetime.Date >= DateTime.Today)
//                .OrderBy(s => s.session_datetime)
//                .Select(s => new ScheduleViewModel
//                {
//                    Child_ID = s.c_myKid,
//                    SessionDate = s.session_datetime,
//                    Slot_ID = s.slot_ID,
//                    Session_ID = s.schedule_ID,
//                    ChildName = child.c_name,
//                    SlotTime = s.Slot.slot_time,
//                    ProgramName = s.Program.prog_name,
//                    TherapistName = s.Therapist.t_name
//                })
//                .ToListAsync();

//            var viewModel = new ChildScheduleDisplayViewModel
//            {
//                ChildId = child.c_myKid,
//                ChildName = child.c_name,
//                Schedules = schedules
//            };

//            return View(viewModel); // Return ChildScheduleDisplayViewModel to the view
//        }

//        // GET: Admin/ChildScheduleAdmin/EditSchedule/{id}
//        // GET: Admin/ChildScheduleAdmin/EditSchedule/{id}
//        public async Task<IActionResult> EditSchedule(int? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var schedule = await _context.Schedules
//                .Include(s => s.Program)
//                .Include(s => s.Therapist)
//                .Include(s => s.Slot)
//                .Include(s => s.Child)
//                .FirstOrDefaultAsync(s => s.schedule_ID == id);

//            if (schedule == null)
//            {
//                return NotFound();
//            }

//            var viewModel = new EditScheduleViewModel
//            {
//                Child_ID = schedule.c_myKid, // Ensure Child_ID is set correctly
//                Session_ID = schedule.schedule_ID,
//                SessionDate = schedule.session_datetime,
//                Slot_ID = schedule.slot_ID
//            };

//            return View(viewModel);
//        }

//        // POST: Admin/ChildScheduleAdmin/EditSchedule/{id}
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> EditSchedule(int id, EditScheduleViewModel viewModel)
//        {
//            if (id != viewModel.Session_ID)
//            {
//                return NotFound();
//            }

//            if (ModelState.IsValid)
//            {
//                try
//                {
//                    var schedule = await _context.Schedules.FindAsync(id);
//                    if (schedule == null)
//                    {
//                        return NotFound();
//                    }

//                    schedule.session_datetime = viewModel.SessionDate;
//                    schedule.slot_ID = viewModel.Slot_ID;

//                    _context.Update(schedule);
//                    await _context.SaveChangesAsync();

//                    return RedirectToAction(nameof(ViewSchedule), new { childId = schedule.c_myKid });
//                }
//                catch (DbUpdateConcurrencyException)
//                {
//                    if (!ScheduleExists(viewModel.Session_ID))
//                    {
//                        return NotFound();
//                    }
//                    else
//                    {
//                        throw;
//                    }
//                }
//            }

//            // If ModelState is invalid, return to the Edit view with the viewModel
//            return View(viewModel);
//        }

//        private bool ScheduleExists(int id)
//        {
//            return _context.Schedules.Any(e => e.schedule_ID == id);
//        }


//        // GET: Admin/ChildScheduleAdmin/DeleteSchedule/{id}
//        public async Task<IActionResult> DeleteSchedule(int? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var schedule = await _context.Schedules
//                .Include(s => s.Program)
//                .Include(s => s.Therapist)
//                .Include(s => s.Slot)
//                .Include(s => s.Child) // Ensure Child is included
//                .FirstOrDefaultAsync(s => s.schedule_ID == id);

//            if (schedule == null)
//            {
//                return NotFound();
//            }

//            var viewModel = new ScheduleViewModel
//            {
//                Child_ID = schedule.c_myKid,
//                SessionDate = schedule.session_datetime,
//                Slot_ID = schedule.slot_ID,
//                Session_ID = schedule.schedule_ID,
//                ChildName = schedule.Child?.c_name,
//                SlotTime = schedule.Slot?.slot_time,
//                ProgramName = schedule.Program?.prog_name,
//                TherapistName = schedule.Therapist?.t_name
//            };

//            return View(viewModel);
//        }

//        // POST: Admin/ChildScheduleAdmin/DeleteSchedule/{id}
//        [HttpPost, ActionName("DeleteSchedule")]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> DeleteConfirmed(int id)
//        {
//            var schedule = await _context.Schedules.FindAsync(id);
//            if (schedule == null)
//            {
//                return NotFound();
//            }

//            _context.Schedules.Remove(schedule);
//            await _context.SaveChangesAsync();
//            return RedirectToAction(nameof(ViewSchedule), new { childId = schedule.c_myKid });
//        }


//    }
//}
