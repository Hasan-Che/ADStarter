//using Microsoft.AspNetCore.Mvc;
//using ADStarter.Models;
//using ADStarter.DataAccess.Data;
//using System;
//using System.Linq;
//using ADStarter.Models.ViewModels;

//namespace ADStarterWeb.Controllers
//{
//    public class ProgramController : Controller
//    {
//        private readonly ApplicationDBContext _context;

//        public ProgramController(ApplicationDBContext context)
//        {
//            _context = context;
//        }

//        // GET: Schedule/Create
//        public IActionResult Create()
//        {
//            ViewBag.SessionOptions = _context.SessionPrices.ToList();
//            ViewBag.SlotOptions = _context.Slots.Take(6).ToList(); // Assuming you want only the first 6 slots

//            return View();
//        }

//        // POST: Schedule/Create
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public IActionResult Create(ScheduleViewModel model)
//        {
//            if (ModelState.IsValid)
//            {
//                var schedule = new Schedule
//                {
//                    session_ID = model.session_ID,
//                    session_datetime = model.sessionDate,
//                    slot_ID = model.slot_ID,
//                    prog_ID = 1, // Assuming prog_ID is always 1 for this case
//                    c_myKid = 1, // Example: replace with actual child ID or logic to determine child
//                    therapist_ID = null // Assuming therapist_ID is null initially
//                };

//                _context.Schedules.Add(schedule);
//                _context.SaveChanges();

//                return RedirectToAction(nameof(Index), "Dashboard"); // Redirect to dashboard or another appropriate action
//            }

//            ViewBag.SessionOptions = _context.SessionPrices.ToList();
//            ViewBag.SlotOptions = _context.Slots.Take(6).ToList(); // Refresh options on failure

//            return View(model);
//        }
//    }
//}
