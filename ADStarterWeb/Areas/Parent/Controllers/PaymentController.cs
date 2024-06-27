using ADStarter.DataAccess.Data;
using ADStarter.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Stripe;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ADStarterWeb.Areas.Parent.Controllers
{
    [Area("Parent")]
    public class PaymentController : Controller
    {
        private readonly ApplicationDBContext _context;
        private readonly IConfiguration _configuration;

        public PaymentController(ApplicationDBContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // GET: Parent/Payment/Index/5
        public IActionResult Index(int invoiceId)
        {
            var invoice = _context.Invoices
                .Where(i => i.invoice_ID == invoiceId)
                .Select(i => new 
                {
                    i.invoice_ID,
                    i.invoice_amount,
                    ChildName = i.Child.c_name,
                    ParentID = i.Child.parent_ID
                })
                .FirstOrDefault();

            if (invoice == null)
            {
                return NotFound();
            }

            // Convert to your model class
            var viewModel = new ADStarter.Models.Invoice
            {
                invoice_ID = invoice.invoice_ID,
                invoice_amount = invoice.invoice_amount,
                Child = new Child { c_name = invoice.ChildName, parent_ID = invoice.ParentID }
            };

            return View(viewModel);
        }

        // POST: Parent/Payment/ProcessPayment
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProcessPayment(int invoiceId, string stripeToken)
        {
            if (string.IsNullOrEmpty(stripeToken))
            {
                return BadRequest("Invalid Stripe token");
            }

            var invoice = _context.Invoices
                .Where(i => i.invoice_ID == invoiceId)
                .Select(i => new 
                {
                    i.invoice_ID,
                    i.invoice_amount,
                    ChildName = i.Child.c_name,
                    ParentID = i.Child.parent_ID
                })
                .FirstOrDefault();

            if (invoice == null)
            {
                return NotFound("Invoice not found.");
            }

            try
            {
                var options = new ChargeCreateOptions
                {
                    Amount = (long)(invoice.invoice_amount * 100), // amount in cents
                    Currency = "myr",
                    Description = $"Payment for invoice #{invoice.invoice_ID}",
                    Source = stripeToken,
                };

                var service = new ChargeService();
                Charge charge = await service.CreateAsync(options);

                if (charge.Status == "succeeded")
                {
                    // Save payment details to the database
                    var payment = new Payment
                    {
                        invoice_ID = invoice.invoice_ID,
                        pay_amount = invoice.invoice_amount,
                        pay_date = DateTime.Now,
                        pay_desc = "Paid",
                        pay_balance = 0,
                        parent_ID = invoice.ParentID,
                        c_myKid = invoice.ChildName,
                        stripe_charge_id = charge.Id,
                        a_ID = 1 // Assuming a_ID is assigned to a default admin ID. Adjust as necessary.
                    };

                    _context.Payments.Add(payment);
                    await _context.SaveChangesAsync();

                    return RedirectToAction("Index", "Invoice");
                }
                else
                {
                    // Handle failed payment
                    ViewBag.ErrorMessage = "Payment failed. Please try again.";
                    var viewModel = new ADStarter.Models.Invoice
                    {
                        invoice_ID = invoice.invoice_ID,
                        invoice_amount = invoice.invoice_amount,
                        Child = new Child { c_name = invoice.ChildName, parent_ID = invoice.ParentID }
                    };
                    return View("Index", viewModel);
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
                ViewBag.ErrorMessage = $"Payment processing error: {ex.Message}";
                var viewModel = new ADStarter.Models.Invoice
                {
                    invoice_ID = invoice.invoice_ID,
                    invoice_amount = invoice.invoice_amount,
                    Child = new Child { c_name = invoice.ChildName, parent_ID = invoice.ParentID }
                };
                return View("Index", viewModel);
            }
        }
    }
}
