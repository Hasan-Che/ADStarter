using ADStarter.DataAccess.Repository.IRepository;
using ADStarter.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ADStarterWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class InvoiceController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public InvoiceController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: Admin/Invoice
        public IActionResult Index()
        {
            var invoices = _unitOfWork.Invoice.GetAll(includeProperties: "Child,Schedule")
                                               .ToList();
            return View(invoices);
        }
    }
}
