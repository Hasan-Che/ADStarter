using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ADStarter.DataAccess.Data;
using System.Linq;
using System.Threading.Tasks;
using ADStarter.Models.ViewModels;
using ADStarter.Utility;
using Microsoft.AspNetCore.Authorization;

namespace ADStarterWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class ChildAdminController : Controller
    {
        private readonly ApplicationDBContext _context;

        public ChildAdminController(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var children = await _context.Children
                .Include(c => c.Therapist)
                .Select(c => new ChildDetailsViewModel
                {
                    ChildName = c.c_name,
                    Step = c.c_step,
                    TherapistName = c.Therapist.t_name,
                    ChildId = c.c_myKid,
                    HasInvoices = _context.Invoices.Any(i => i.c_myKid == c.c_myKid),
                    HasPayments = _context.Payments.Any(p => p.c_myKid  == c.c_myKid)  // Check if payments exist
                })
                .ToListAsync();

            return View(children);
        }


        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var child = await _context.Children
                .Include(c => c.Therapist)
                .Include(c => c.Parent)
                .Where(c => c.c_myKid == id)
                .Select(c => new ChildDetailsViewModel
                {
                    ChildName = c.c_name,
                    Step = c.c_step,
                    TherapistName = c.Therapist.t_name,
                    ChildId = c.c_myKid,
                    Age = c.c_age,
                    Gender = c.c_gender,
                    DOB = c.c_dob,
                    Nationality = c.c_nationality,
                    Religion = c.c_religion,
                    Race = c.c_race,
                    ParentName = c.Parent.f_name
                })
                .FirstOrDefaultAsync();

            if (child == null)
            {
                return NotFound();
            }

            return PartialView("_ChildDetails", child);
        }
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> UpdateStep(string childId, int newStep)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var child = await _context.Children.FindAsync(childId);
        //        if (child != null)
        //        {
        //            child.c_step = newStep;
        //            await _context.SaveChangesAsync();
        //            return RedirectToAction(nameof(Index));
        //        }
        //        ModelState.AddModelError("", "Child not found.");
        //    }

        //    // If ModelState is not valid or child is not found, reload the view
        //    var children = await _context.Children
        //        .Include(c => c.Therapist)
        //        .Select(c => new ChildDetailsViewModel
        //        {
        //            ChildId = c.c_myKid,
        //            ChildName = c.c_name,
        //            Step = c.c_step,
        //            TherapistName = c.Therapist.t_name
        //        })
        //        .ToListAsync();

        //    return View("Index", children);
        //}

        public async Task<IActionResult> Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var child = await _context.Children
                .Where(c => c.c_myKid == id)
                .Select(c => new ChildDetailsViewModel
                {
                    ChildId = c.c_myKid,
                    ChildName = c.c_name,
                    Step = c.c_step
                })
                .FirstOrDefaultAsync();

            if (child == null)
            {
                return NotFound();
            }

            return View(child);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateStep(string ChildId, int NewStep)
        {
            if (string.IsNullOrEmpty(ChildId))
            {
                return NotFound();
            }

            var child = await _context.Children.FindAsync(ChildId);
            if (child == null)
            {
                return NotFound();
            }

            child.c_step = NewStep;
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

    }
}