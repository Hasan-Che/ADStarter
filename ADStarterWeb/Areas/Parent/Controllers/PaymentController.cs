using ADStarter.DataAccess.Data;
using ADStarter.Models;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using Stripe.Checkout;
using System.Security.Claims;

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
                    i.c_myKid,
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
                    Amount = (long)(invoice.invoice_amount * 100),
                    Currency = "myr",
                    Description = $"Payment for invoice #{invoice.invoice_ID}",
                    Source = stripeToken,
                };

                var service = new ChargeService();
                Charge charge = await service.CreateAsync(options);

                if (charge.Status == "succeeded")
                {
                    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                    var payment = new Payment
                    {
                        invoice_ID = invoice.invoice_ID,
                        pay_amount = invoice.invoice_amount,
                        pay_date = DateTime.Now,
                        pay_desc = "Paid",
                        pay_balance = 0,
                        parent_ID = invoice.ParentID,
                        c_myKid = invoice.c_myKid,
                        stripe_charge_id = charge.Id,
                        Id = userId
                    };

                    _context.Payments.Add(payment);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Your payment has succcessfuly been made";
                    return RedirectToAction("Index", "Invoice");
                }
                else
                {
                    ViewBag.ErrorMessage = "Payment failed. Please try again.";
                    var viewModel = new ADStarter.Models.Invoice
                    {
                        invoice_ID = invoice.invoice_ID,
                        invoice_amount = invoice.invoice_amount,
                        Child = new Child { c_myKid = invoice.c_myKid, parent_ID = invoice.ParentID }
                    };
                    return View("Index", viewModel);
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"Payment processing error: {ex.Message}";
                var viewModel = new ADStarter.Models.Invoice
                {
                    invoice_ID = invoice.invoice_ID,
                    invoice_amount = invoice.invoice_amount,
                    Child = new Child { c_myKid = invoice.c_myKid, parent_ID = invoice.ParentID }
                };
                return View("Index", viewModel);
            }
        }


        [HttpPost]
        public IActionResult CreateCheckoutSession(int invoiceId)
        {
            var invoice = _context.Invoices
                .Where(i => i.invoice_ID == invoiceId)
                .Select(i => new 
                {
                    i.invoice_ID,
                    i.invoice_amount,
                    ChildName = i.Child.c_name
                })
                .FirstOrDefault();

            if (invoice == null)
            {
                return NotFound("Invoice not found.");
            }

            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string>
                {
                    "card",
                },
                LineItems = new List<SessionLineItemOptions>
                {
                    new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            Currency = "myr",
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = $"Payment for invoice #{invoice.invoice_ID}",
                            },
                            UnitAmount = (long)(invoice.invoice_amount * 100),
                        },
                        Quantity = 1,
                    },
                },
                Mode = "payment",
                SuccessUrl = Url.Action("Success", "Payment", null, Request.Scheme),
                CancelUrl = Url.Action("Cancel", "Payment", null, Request.Scheme),
            };

            var service = new SessionService();
            Session session = service.Create(options);

            return Json(new { id = session.Id });
        }

        // GET: Parent/Payment/PaymentHistory
        public IActionResult PaymentHistory()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var parent = _context.Parents.FirstOrDefault(p => p.UserId == userId);

            if (parent == null)
            {
                return NotFound("Parent not found.");
            }

            var payments = _context.Payments
                .Where(p => p.parent_ID == parent.parent_ID)
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


