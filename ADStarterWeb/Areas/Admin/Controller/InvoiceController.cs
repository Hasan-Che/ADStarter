using ADStarter.DataAccess;
using ADStarter.DataAccess.Data;
using ADStarter.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

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
        public IActionResult Index()
        {
            var invoices = _context.Invoices
                .Include(i => i.Child)
                .Include(i => i.Child.Parent)
                .Include(i => i.Schedule)
                .Include(i => i.Schedule.Program)
                .ToList();
            return View(invoices);
        }

        // GET: Admin/Invoice/GetParents
        [HttpGet]
        public IActionResult GetParents()
        {
            var parents = _context.Parents.ToList();
            return Json(parents);
        }

        [HttpPost]
        public IActionResult EditInvoice(int invoiceId, DateTime scheduleDate, DateTime dueDate, double amount)
        {
            var invoice = _context.Invoices.FirstOrDefault(i => i.invoice_ID == invoiceId);
            if (invoice != null)
            {
                invoice.Schedule.session_datetime = scheduleDate;
                invoice.due_date = dueDate;
                invoice.invoice_amount = amount;

                _context.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false, message = "Invoice not found" });
        }

        // POST: /Admin/Invoice/Delete/{id}
        [HttpPost]
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
