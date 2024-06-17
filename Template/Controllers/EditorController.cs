using Microsoft.AspNetCore.Mvc;

namespace Viho.Controllers
{
    public class EditorController : Controller
    {
        public IActionResult SummerNote()
        {
            return View();
        }
        public IActionResult CkEditor()
        {
            return View();
        }
        public IActionResult MDEEditor()
        {
            return View();
        }
        public IActionResult ACECodeEditor()
        {
            return View();
        }
    }
}
