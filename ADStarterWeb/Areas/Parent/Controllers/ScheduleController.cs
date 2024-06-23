//using Microsoft.AspNetCore.Mvc;
//using ADStarter.Models;
//using ADStarter.Models.ViewModels;
//using ADStarter.DataAccess.Data;
//using System.Linq;
//using Microsoft.EntityFrameworkCore;

//namespace ADStarterWeb.Areas.Parent.Controllers
//{
//    [Area("Parent")]
//    public class ScheduleController : Controller
//    {
//        private readonly ApplicationDBContext _context;

//        public ScheduleController(ApplicationDBContext context)
//        {
//            _context = context;
//        }

//        // GET: Schedule/CreateNewSchedule
//        public IActionResult CreateNewSchedule()
//        {
//            ViewBag.ActiveChildren = _context.Children
//                .Where(c => c.c_status == "Active")
//                .Select(c => new { c.c_myKid, c.c_name })
//                .ToList();

//            ViewBag.SlotOptions = _context.Slots
//                .Where(s => s.slot_ID <= 6)
//                .ToList();

//            return View();
//        }

//        // POST: Schedule/CreateNewSchedule
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public IActionResult CreateNewSchedule([Bind("Child_ID, SessionDate, Slot_ID, Session_ID")] NewScheduleViewModel model)
//        {
//            if (ModelState.IsValid)
//            {
//                // Fetch sp_price from SessionPrice based on Session_ID
//                var sessionPrice = _context.SessionPrices.FirstOrDefault(sp => sp.session_ID == model.Session_ID);
//                if (sessionPrice != null)
//                {
//                    // Fetch Slot based on Slot_ID
//                    var slot = _context.Slots.FirstOrDefault(s => s.slot_ID == model.Slot_ID);
//                    if (slot != null)
//                    {
//                        var schedule = new Schedule
//                        {
//                            c_myKid = model.Child_ID,
//                            session_datetime = model.SessionDate,
//                            slot_ID = model.Slot_ID,
//                            prog_ID = sessionPrice.prog_ID,
//                            t_ID = 2, // Assuming therapist ID is hardcoded for now
//                            session_ID = model.Session_ID,
//                            slot_price = sessionPrice.sp_price // Assign sp_price from SessionPrice
//                        };

//                        _context.Schedules.Add(schedule);
//                        _context.SaveChanges();
//                        return RedirectToAction(nameof(Index), "Dashboard");
//                    }
//                    else
//                    {
//                        ModelState.AddModelError(string.Empty, "Invalid Slot selection.");
//                    }
//                }
//                else
//                {
//                    ModelState.AddModelError(string.Empty, "Invalid Session selection or missing SessionPrice.");
//                }
//            }

//            // If ModelState is not valid, reload necessary data and return to the view
//            ViewBag.ActiveChildren = _context.Children
//                .Where(c => c.c_status == "Active")
//                .Select(c => new { c.c_myKid, c.c_name })
//                .ToList();

//            ViewBag.SlotOptions = _context.Slots
//                .Where(s => s.slot_ID <= 6)
//                .ToList();

//            return View(model);
//        }

//        // GET: Schedule/Create3
//        public IActionResult Create3()
//        {
//            ViewBag.ActiveChildren = _context.Children
//                .Where(c => c.c_status == "Active")
//                .Select(c => new { c.c_myKid, c.c_name })
//                .ToList();

//            ViewBag.SlotOptions = _context.Slots
//                .Where(s => s.slot_ID <= 6)
//                .ToList();

//            ViewBag.TherapistOptions = _context.Therapists
//                .Select(t => new { t.t_ID, t.t_name })
//                .ToList();

//            return View();
//        }

//        // POST: Schedule/Create3
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public IActionResult Create3(Schedule2ViewModel model)
//        {
//            if (ModelState.IsValid)
//            {
//                var validTherapistID = _context.Therapists.FirstOrDefault()?.t_ID ?? 1; // Assuming at least one therapist exists

//                for (int i = 0; i < model.SessionDates.Count; i++)
//                {
//                    var sessionDate = model.SessionDates[i];
//                    var slotID = model.Slot_IDs[i];

//                    var session_ID = sessionDate.DayOfWeek == DayOfWeek.Saturday || sessionDate.DayOfWeek == DayOfWeek.Sunday ? 4 : 3;

//                    var sessionPrice = _context.SessionPrices.FirstOrDefault(sp => sp.session_ID == session_ID);

//                    var schedule = new Schedule
//                    {
//                        c_myKid = model.Child_ID,
//                        session_datetime = sessionDate,
//                        slot_ID = slotID,
//                        prog_ID = 2, // Fixed prog_ID for Create3
//                        t_ID = validTherapistID, // Valid therapist ID
//                        session_ID = session_ID, // Assign session_ID based on weekday/weekend logic
//                        slot_price = sessionPrice != null ? sessionPrice.sp_price : 0 // Assign sp_price from SessionPrice
//                    };

//                    _context.Schedules.Add(schedule);
//                }

//                _context.SaveChanges();
//                return RedirectToAction(nameof(Index), "Dashboard"); // Redirect to dashboard or another appropriate action
//            }

//            // If ModelState is not valid, reload necessary data and return to the view
//            ViewBag.ActiveChildren = _context.Children
//                .Where(c => c.c_status == "Active")
//                .Select(c => new { c.c_myKid, c.c_name })
//                .ToList();

//            ViewBag.SlotOptions = _context.Slots
//                .Where(s => s.slot_ID <= 6)
//                .ToList();

//            ViewBag.TherapistOptions = _context.Therapists
//                .Select(t => new { t.t_ID, t.t_name })
//                .ToList();

//            return View(model);
//        }

//        // GET: Schedule/CreateProgram3
//        public IActionResult CreateProgram3()
//        {
//            ViewBag.ActiveChildren = _context.Children
//                .Where(c => c.c_status == "Active")
//                .Select(c => new { c.c_myKid, c.c_name })
//                .ToList();

//            ViewBag.SlotOptions = _context.Slots
//                .Where(s => s.slot_ID <= 6)
//                .ToList();

//            ViewBag.TherapistOptions = _context.Therapists
//                .Select(t => new { t.t_ID, t.t_name })
//                .ToList();

//            return View(new Schedule2ViewModel());
//        }

//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public IActionResult CreateProgram3(Schedule2ViewModel model)
//        {
//            if (ModelState.IsValid)
//            {
//                var validTherapistID = _context.Therapists.FirstOrDefault()?.t_ID ?? 1;

//                for (int i = 0; i < model.NumberOfSessions; i++)
//                {
//                    var sessionDate = model.SessionDates[i];
//                    var slotID = model.Slot_IDs[i];

//                    // Determine session_ID based on program_ID and session conditions
//                    var session_ID = _context.SessionPrices
//                        .Where(sp => sp.prog_ID == 3 // Assuming prog_ID 3 for this scenario
//                                  && sp.session_bilangan == model.NumberOfSessions
//                                  && (sessionDate.DayOfWeek == DayOfWeek.Saturday || sessionDate.DayOfWeek == DayOfWeek.Sunday
//                                  ? sp.session_day == "Weekend"
//                                  : sp.session_day == "Weekday"))
//                        .Select(sp => sp.session_ID)
//                        .FirstOrDefault();

//                    var sessionPrice = _context.SessionPrices.FirstOrDefault(sp => sp.session_ID == session_ID);

//                    var schedule = new Schedule
//                    {
//                        c_myKid = model.Child_ID,
//                        session_datetime = sessionDate,
//                        slot_ID = slotID,
//                        prog_ID = 3, // Fixed prog_ID for CreateProgram3
//                        t_ID = validTherapistID, // Valid therapist ID
//                        session_ID = session_ID, // Assign session_ID based on the logic
//                        slot_price = sessionPrice != null ? sessionPrice.sp_price : 0 // Assign sp_price from SessionPrice
//                    };

//                    _context.Schedules.Add(schedule);
//                }

//                _context.SaveChanges();
//                return RedirectToAction(nameof(Index), "Dashboard");
//            }

//            ViewBag.ActiveChildren = _context.Children
//                .Where(c => c.c_status == "Active")
//                .Select(c => new { c.c_myKid, c.c_name })
//                .ToList();

//            ViewBag.SlotOptions = _context.Slots
//                .Where(s => s.slot_ID <= 6)
//                .ToList();

//            ViewBag.TherapistOptions = _context.Therapists
//                .Select(t => new { t.t_ID, t.t_name })
//                .ToList();

//            return View(model);
//        }

//        // GET: Schedule/CreateProgram4
//        public IActionResult CreateProgram4()
//        {
//            ViewBag.ActiveChildren = _context.Children
//                .Where(c => c.c_status == "Active")
//                .Select(c => new { c.c_myKid, c.c_name })
//                .ToList();

//            ViewBag.SlotOptions = _context.Slots
//                .Where(s => s.slot_ID <= 6)
//                .ToList();

//            ViewBag.TherapistOptions = _context.Therapists
//                .Select(t => new { t.t_ID, t.t_name })
//                .ToList();

//            return View();
//        }

//        // POST: Schedule/CreateProgram4
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public IActionResult CreateProgram4(Schedule2ViewModel model)
//        {
//            if (ModelState.IsValid)
//            {
//                var validTherapistID = _context.Therapists.FirstOrDefault()?.t_ID ?? 1; // Assuming at least one therapist exists

//                for (int i = 0; i < model.SessionDates.Count; i++)
//                {
//                    var sessionDate = model.SessionDates[i];
//                    var slotID = model.Slot_IDs[i];

//                    var session_ID = sessionDate.DayOfWeek == DayOfWeek.Saturday || sessionDate.DayOfWeek == DayOfWeek.Sunday ? 14 : 13;

//                    // Fetch sp_price from SessionPrice based on Session_ID
//                    var sessionPrice = _context.SessionPrices.FirstOrDefault(sp => sp.session_ID == session_ID);

//                    var schedule = new Schedule
//                    {
//                        c_myKid = model.Child_ID,
//                        session_datetime = sessionDate,
//                        slot_ID = slotID,
//                        prog_ID = 4, // Fixed prog_ID for CreateProgram4
//                        t_ID = validTherapistID, // Valid therapist ID
//                        session_ID = session_ID, // Assign session_ID based on weekday/weekend logic
//                        slot_price = sessionPrice != null ? sessionPrice.sp_price : 0 // Assign sp_price from SessionPrice
//                    };

//                    _context.Schedules.Add(schedule);
//                }

//                _context.SaveChanges();
//                return RedirectToAction(nameof(Index), "Dashboard"); // Redirect to dashboard or another appropriate action
//            }

//            // If ModelState is not valid, reload necessary data and return to the view
//            ViewBag.ActiveChildren = _context.Children
//                .Where(c => c.c_status == "Active")
//                .Select(c => new { c.c_myKid, c.c_name })
//                .ToList();

//            ViewBag.SlotOptions = _context.Slots
//                .Where(s => s.slot_ID <= 6)
//                .ToList();

//            ViewBag.TherapistOptions = _context.Therapists
//                .Select(t => new { t.t_ID, t.t_name })
//                .ToList();

//            return View(model);
//        }

//        // GET: Schedule/CreateProgram5
//        public IActionResult CreateProgram5()
//        {
//            ViewBag.ActiveChildren = _context.Children
//                .Where(c => c.c_status == "Active")
//                .Select(c => new { c.c_myKid, c.c_name })
//                .ToList();

//            ViewBag.TherapistOptions = _context.Therapists
//                .Select(t => new { t.t_ID, t.t_name })
//                .ToList();

//            return View();
//        }

//        // POST: Schedule/CreateProgram5
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public IActionResult CreateProgram5(Schedule2ViewModel model)
//        {
//            if (ModelState.IsValid)
//            {
//                var validTherapistID = _context.Therapists.FirstOrDefault()?.t_ID ?? 1; // Assuming at least one therapist exists

//                for (int i = 0; i < model.SessionDates.Count; i++)
//                {
//                    var sessionDate = model.SessionDates[i];
//                    var slotID = model.Slot_IDs[i];

//                    // Determine the session ID based on the selected date
//                    var session_ID = sessionDate.DayOfWeek == DayOfWeek.Saturday || sessionDate.DayOfWeek == DayOfWeek.Sunday ? 16 : 15;

//                    // Fetch sp_price from SessionPrice based on Session_ID
//                    var sessionPrice = _context.SessionPrices.FirstOrDefault(sp => sp.session_ID == session_ID);

//                    var schedule = new Schedule
//                    {
//                        c_myKid = model.Child_ID,
//                        session_datetime = sessionDate,
//                        slot_ID = slotID,
//                        prog_ID = 5, // Fixed prog_ID for CreateProgram5
//                        t_ID = validTherapistID, // Valid therapist ID
//                        session_ID = session_ID, // Assign session_ID based on weekday/weekend logic
//                        slot_price = sessionPrice != null ? sessionPrice.sp_price : 0 // Assign sp_price from SessionPrice
//                    };

//                    _context.Schedules.Add(schedule);
//                }

//                _context.SaveChanges();
//                return RedirectToAction(nameof(Index), "Dashboard"); // Redirect to dashboard or another appropriate action
//            }

//            // If ModelState is not valid, reload necessary data and return to the view
//            ViewBag.ActiveChildren = _context.Children
//                .Where(c => c.c_status == "Active")
//                .Select(c => new { c.c_myKid, c.c_name })
//                .ToList();

//            ViewBag.TherapistOptions = _context.Therapists
//                .Select(t => new { t.t_ID, t.t_name })
//                .ToList();

//            return View(model);
//        }

//        // GET: Schedule/CreateProgram6
//        public IActionResult CreateProgram6()
//        {
//            ViewBag.ActiveChildren = _context.Children
//                .Where(c => c.c_status == "Active")
//                .Select(c => new { c.c_myKid, c.c_name })
//                .ToList();

//            ViewBag.TherapistOptions = _context.Therapists
//                .Select(t => new { t.t_ID, t.t_name })
//                .ToList();

//            return View();
//        }

//        // POST: Schedule/CreateProgram6
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public IActionResult CreateProgram6(Schedule2ViewModel model)
//        {
//            if (ModelState.IsValid)
//            {
//                var validTherapistID = _context.Therapists.FirstOrDefault()?.t_ID ?? 1; // Assuming at least one therapist exists

//                for (int i = 0; i < model.SessionDates.Count; i++)
//                {
//                    var sessionDate = model.SessionDates[i];
//                    var slotID = model.Slot_IDs[i];

//                    // Determine the session ID based on the selected date
//                    var session_ID = sessionDate.DayOfWeek == DayOfWeek.Saturday || sessionDate.DayOfWeek == DayOfWeek.Sunday ? 18 : 17;

//                    // Fetch sp_price from SessionPrice based on Session_ID
//                    var sessionPrice = _context.SessionPrices.FirstOrDefault(sp => sp.session_ID == session_ID);

//                    var schedule = new Schedule
//                    {
//                        c_myKid = model.Child_ID,
//                        session_datetime = sessionDate,
//                        slot_ID = slotID,
//                        prog_ID = 6, // Fixed prog_ID for CreateProgram6
//                        t_ID = validTherapistID, // Valid therapist ID
//                        session_ID = session_ID, // Assign session_ID based on weekday/weekend logic
//                        slot_price = sessionPrice != null ? sessionPrice.sp_price : 0 // Assign sp_price from SessionPrice
//                    };

//                    _context.Schedules.Add(schedule);
//                }

//                _context.SaveChanges();
//                return RedirectToAction(nameof(Index), "Dashboard"); // Redirect to dashboard or another appropriate action
//            }

//            // If ModelState is not valid, reload necessary data and return to the view
//            ViewBag.ActiveChildren = _context.Children
//                .Where(c => c.c_status == "Active")
//                .Select(c => new { c.c_myKid, c.c_name })
//                .ToList();

//            ViewBag.TherapistOptions = _context.Therapists
//                .Select(t => new { t.t_ID, t.t_name })
//                .ToList();

//            return View(model);
//        }

//        //// GET: Schedule/ViewChildSchedule
//        //public IActionResult ViewChildSchedule()
//        //{
//        //    var activeChildren = _context.Children
//        //        .Where(c => c.c_status == "Active")
//        //        .Select(c => new { c.c_myKid, c.c_name })
//        //        .ToList();

//        //    ViewBag.ActiveChildren = activeChildren;

//        //    return View();
//        //}

//        //[HttpPost]
//        //public IActionResult ViewChildSchedule(int childId)
//        //{
//        //    var child = _context.Children.FirstOrDefault(c => c.c_myKid == childId);

//        //    if (child == null)
//        //    {
//        //        return NotFound();
//        //    }

//        //    var schedules = _context.Schedules
//        //        .Where(s => s.c_myKid == childId)
//        //        .Select(s => new ScheduleViewModel
//        //        {
//        //            Child_ID = s.c_myKid,
//        //            SessionDate = s.session_datetime,
//        //            Slot_ID = s.Slot.slot_ID,
//        //            Session_ID = s.schedule_ID,
//        //            SlotTime = s.Slot.slot_time,
//        //            ProgramName = s.Program.prog_name,
//        //            TherapistName = s.Therapist.t_name
//        //        })
//        //        .ToList();

//        //    var model = new ChildScheduleViewModel
//        //    {
//        //        Child_ID = child.c_myKid,
//        //        ChildName = child.c_name,
//        //        Schedules = schedules
//        //    };

//        //    ViewBag.ActiveChildren = _context.Children
//        //        .Where(c => c.c_status == "Active")
//        //        .Select(c => new { c.c_myKid, c.c_name })
//        //        .ToList();

//        //    return View(model);
//        //}

//        //[HttpGet]
//        //public IActionResult GetSchedules(int childId)
//        //{
//        //    var schedules = _context.Schedules
//        //        .Where(s => s.c_myKid == childId)
//        //        .Select(s => new
//        //        {
//        //            title = s.Program.prog_name + " - " + s.Therapist.t_name,
//        //            start = s.session_datetime.ToString("yyyy-MM-ddTHH:mm:ss"),
//        //            end = s.session_datetime.AddMinutes(60).ToString("yyyy-MM-ddTHH:mm:ss"),
//        //            description = s.Slot.slot_time
//        //        })
//        //        .ToList();

//        //    return Json(schedules);
//        //}

//        // GET: Schedule/ViewChildSchedule
//        public IActionResult ViewChildSchedule()
//        {
//            // Fetch all active children
//            var activeChildren = _context.Children
//                .Where(c => c.c_status == "Active")
//                .Select(c => new { c.c_myKid, c.c_name })
//                .ToList();

//            // Prepare a list to hold ChildScheduleViewModel instances
//            var childSchedules = new List<ChildScheduleViewModel>();

//            // Iterate through each child and fetch their schedules
//            foreach (var child in activeChildren)
//            {
//                var schedules = _context.Schedules
//                    .Where(s => s.c_myKid == child.c_myKid)
//                    .Select(s => new ScheduleViewModel
//                    {
//                        Child_ID = s.c_myKid,
//                        SessionDate = s.session_datetime.Date, // Ensure only date is selected
//                        Slot_ID = s.Slot.slot_ID,
//                        Session_ID = s.schedule_ID,
//                        SlotTime = s.Slot.slot_time,
//                        ProgramName = s.Program.prog_name,
//                        TherapistName = s.Therapist.t_name
//                    })
//                    .ToList();

//                var childScheduleViewModel = new ChildScheduleViewModel
//                {
//                    Child_ID = child.c_myKid,
//                    ChildName = child.c_name,
//                    Schedules = schedules
//                };

//                childSchedules.Add(childScheduleViewModel);
//            }

//            // Pass the list of ChildScheduleViewModel instances to the view
//            return View(childSchedules);
//        }

//    }
//}
