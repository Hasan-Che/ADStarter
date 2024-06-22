using ADStarter.DataAccess.Data;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace ADStarterWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
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
            var programs = _context.Programs
                                .Select(p => new {
                                    prog_ID = p.prog_ID,
                                    prog_name = p.prog_name,
                                    prog_desc = p.prog_desc
                                }).ToList();
            return Json(programs);
        }

        [HttpGet]
        public JsonResult GetSessionPrices(int programId)
        {
            var sessionPrices = _context.SessionPrices
                                        .Where(sp => sp.prog_ID == programId)
                                        .Select(sp => new {
                                            session_ID = sp.session_ID,
                                            session_name = sp.session_name,
                                            session_day = sp.session_day,
                                            sp_price = sp.sp_price,
                                            session_bilangan = sp.session_bilangan
                                        }).ToList();
            return Json(sessionPrices);
        }
    }
}
