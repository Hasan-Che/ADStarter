using ADStarter.DataAccess.Data;
using ADStarter.Models;
using Microsoft.AspNetCore.Mvc;
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

        public IActionResult CreateInvoice()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateInvoice([FromBody] Invoice invoice)
        {
            if (ModelState.IsValid)
            {
                _context.Invoices.Add(invoice);
                _context.SaveChanges();
                return Ok(new { success = true, message = "Invoice created successfully" });
            }
            return BadRequest(new { success = false, message = "Invalid data" });
        }

        public IActionResult ManageInvoice()
        {
            return View();
        }

        public IActionResult ManagePayment()
        {
            return View();
        }

        public IActionResult HistoryPayment()
        {
            return View();
        }
    }
}
