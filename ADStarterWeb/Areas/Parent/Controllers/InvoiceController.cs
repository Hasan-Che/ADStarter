using ADStarter.DataAccess.Repository.IRepository;
using ADStarter.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ADStarterWeb.Areas.Parent.Controllers
{
    [Area("Parent")]
    public class InvoiceController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<IdentityUser> _userManager;

        public InvoiceController(IUnitOfWork unitOfWork, UserManager<IdentityUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        // GET: Parent/Invoice
        public IActionResult Index()
        {
            var userId = _userManager.GetUserId(User);
            var parent = _unitOfWork.Parent.GetFirstOrDefault(p => p.UserId == userId);

            if (parent == null)
            {
                return NotFound("Parent not found.");
            }

            var invoices = _unitOfWork.Invoice.GetAll(
                i => i.Child.parent_ID == parent.parent_ID,
                includeProperties: "Child,Schedule,Schedule.Program,Payments"
            ).ToList();

            var invoiceViewModels = invoices.Select(i => new InvoiceViewModel
            {
                Invoice_ID = i.invoice_ID,
                Invoice_Amount = i.invoice_amount,
                ChildName = i.Child.c_name,
                Due_Date = i.due_date,
                ProgramName = i.Schedule.Program.prog_name,
                Session_Date = i.Schedule.session_datetime,
                Payments = i.Payments.ToList(),
                Schedule = i.Schedule,
                IsPaid = i.Payments.Any(p => p.pay_desc == "Paid")
            }).ToList();

            return View(invoiceViewModels);
        }
    }
}
