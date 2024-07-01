using ADStarter.DataAccess.Data;
using ADStarter.DataAccess.Repository.IRepository;
using ADStarter.Models.ViewModels;
using ADStarter.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;

namespace ADStarterWeb.Areas.Therapist.Controllers
{
    [Area("Therapist")]
    [Authorize(Roles = SD.Role_Therapist)]
    public class AssignedChildController : Controller
    {
        private readonly ApplicationDBContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public AssignedChildController(ApplicationDBContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        public IActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var therapist = _context.Therapists.FirstOrDefault(t => t.User.Id == userId);

            if (therapist == null)
            {
                return NotFound("Therapist not found.");
            }

            var assignedChildren = _context.Children
                .Where(c => c.t_ID == therapist.t_ID)
                .Select(c => new AssignedChildViewModel
                {
                    ChildName = c.c_name,
                    Age = c.c_age,
                    CStep = c.c_step.HasValue ? c.c_step.Value.ToString() : "N/A",
                    Gender = c.c_gender
                })
                .ToList();

            return View(assignedChildren);
        }
    }
}
