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

        public IActionResult ManageInvoice()
        {
            return View();
        }

        public IActionResult ManagePayment()
        {
            return View();
        }

        // GET: Admin/Payment/HistoryPayment
        public IActionResult HistoryPayment()
        {
            // Assuming you have a way to identify the admin user or role
            // For example, checking if the user is in an admin role:
            if (!User.IsInRole("Admin"))
            {
                return Unauthorized(); // or handle according to your application's authorization logic
            }

            // Retrieve all payments
            var payments = _context.Payments
                .Select(p => new Payment
                {
                    pay_ID = p.pay_ID,
                    invoice_ID = p.invoice_ID,
                    pay_amount = p.pay_amount,
                    pay_date = p.pay_date,
                    pay_desc = p.pay_desc,
                    pay_balance = p.pay_balance,
                    stripe_charge_id = p.stripe_charge_id,
                    Invoice = _context.Invoices.FirstOrDefault(i => i.invoice_ID == p.invoice_ID),
                    Child = _context.Children.FirstOrDefault(c => c.c_myKid == p.c_myKid)
                })
                .ToList();

            return View(payments);
        }
    }
}
