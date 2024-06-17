using Microsoft.AspNetCore.Mvc;

namespace Cuba.Controllers
{
    public class FormLayoutController : Controller
    {
        public IActionResult DefaultForm()
        {
            return View();
        }
        public IActionResult FormWizard1()
        {
            return View();
        }
        public IActionResult FormWizard2()
        {
            return View();
        }
        public IActionResult FormWizard3()
        {
            return View();
        }
    }
}
