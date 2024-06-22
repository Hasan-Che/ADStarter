using ADStarter.DataAccess.Data;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace ADStarterWeb.Areas.Parent.Controllers
{
    [Area("Parent")]
    public class PaymentController : Controller
    {
        private readonly ApplicationDBContext _context;

        public PaymentController(ApplicationDBContext context)
        {
            _context = context;
        }

        public IActionResult Invoice()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetPrograms()
        {
            var programs = _context.Programs.Select(p => new { p.prog_ID, p.prog_name }).ToList();
            return Json(programs);
        }

        [HttpGet]
        public JsonResult GetSessionPrices(int programId)
        {
            var sessionPrices = _context.SessionPrices
                                        .Where(sp => sp.prog_ID == programId)
                                        .Select(sp => new { sp.session_ID, sp.session_day, sp.session_name, sp.sp_price })
                                        .ToList();
            return Json(sessionPrices);
        }
    }
}
