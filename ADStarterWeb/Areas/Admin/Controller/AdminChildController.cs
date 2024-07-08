using System.Linq;
using Microsoft.EntityFrameworkCore;
using ADStarter.DataAccess.Data;
using Microsoft.AspNetCore.Mvc;

namespace ADStarterWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminChildController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly ApplicationDBContext _context;

        public AdminChildController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: AdminChild
        public async Task<IActionResult> Index()
        {
            var children = await _context.Children
                .Include(c => c.Parent)
                .Include(c => c.Therapist)
                .ToListAsync();

            return View(children);
        }

        // GET: AdminChild/AssignTherapist
        public IActionResult AssignTherapist(string id)
        {
            var child = _context.Children
                .Include(c => c.Therapist)
                .FirstOrDefault(c => c.c_myKid == id);

            if (child == null)
            {
                return NotFound();
            }

            var therapists = _context.Therapists.ToList();

            ViewBag.ChildId = id;
            ViewBag.Therapists = therapists;

            return View(child);
        }

        // POST: AdminChild/AssignTherapist
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AssignTherapist(string childId, int? therapistId)
        {
            if (childId == null || therapistId == null)
            {
                return NotFound();
            }

            var child = await _context.Children.FindAsync(childId);

            if (child == null)
            {
                return NotFound();
            }

            child.t_ID = therapistId;
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
