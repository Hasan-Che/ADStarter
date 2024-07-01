using ADStarter.DataAccess.Data;
using ADStarter.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ADStarterWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class InvoiceController : Controller
    {
        private readonly ApplicationDBContext _context;

        public InvoiceController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: Admin/Invoice
        public IActionResult Index(bool showAll = false)
        {
            var invoices = _context.Invoices
                .Include(i => i.Child)
                .Include(i => i.Child.Parent)
                .Include(i => i.Schedule)
                .Include(i => i.Schedule.Program)
                .Include(i => i.Payments)
                .ToList();

            if (!showAll)
            {
                invoices = invoices.Where(i => !i.Payments.Any(p => p.pay_desc == "Paid")).ToList();
            }

            return View(invoices);
        }

        public ActionResult CreateInvoice()
        {
            var children = _context.Children.ToList(); // Fetch all children
            return View(children);
        }

        public JsonResult GetSchedulesForChild(string childId)
        {
            var schedules = _context.Schedules
                .Where(s => s.c_myKid == childId && !_context.Invoices.Any(i => i.schedule_ID == s.schedule_ID))
                .ToList();
            return Json(schedules);
        }

        public JsonResult GetParentDetails(int parentId)
        {
            var parent = _context.Parents.Find(parentId);
            return Json(parent);
        }

        [HttpPost]
        public ActionResult CreateInvoice(InvoiceViewModel model)
        {
            if (ModelState.IsValid)
            {
                var invoice = new Invoice
                {
                    c_myKid = model.C_MyKid,
                    schedule_ID = model.Schedule_ID,
                    due_date = model.Due_Date,
                    invoice_amount = model.Invoice_Amount,
                };
                _context.Invoices.Add(invoice);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult EditInvoice(int invoiceId, DateTime scheduleDate, DateTime dueDate, double amount)
        {
            var invoice = _context.Invoices.FirstOrDefault(i => i.invoice_ID == invoiceId);
            if (invoice != null)
            {
                invoice.due_date = dueDate;
                invoice.invoice_amount = amount;

                try
                {
                    _context.SaveChanges();
                    return Json(new { success = true });
                }
                catch (Exception ex)
                {
                    // Log the exception for debugging purposes
                    Console.WriteLine(ex.Message);
                    return Json(new { success = false, message = "Failed to save changes" });
                }
            }
            return Json(new { success = false, message = "Invoice not found" });
        }

        // POST: /Admin/Invoice/Delete/{id}
        [HttpDelete]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var invoice = _context.Invoices.Find(id);
            if (invoice == null)
            {
                return NotFound();
            }

            _context.Invoices.Remove(invoice);
            _context.SaveChanges();

            TempData["Success"] = "Invoice deleted successfully.";
            return RedirectToAction(nameof(Index));
        }
    }
}
