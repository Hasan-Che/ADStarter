using ADStarter.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace ADStarterWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProgramController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProgramController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: Program/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Program/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ADStarter.Models.Program obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Program.Add(obj);
                _unitOfWork.Save();
                return RedirectToAction("ListPrograms");
            }
            return View();
        }

        // GET: Program/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null|| id == 0)
            {
                return NotFound();
            }
            ADStarter.Models.Program? ProgramFromDb = _unitOfWork.Program.Get(u =>u.prog_ID ==  id);
            if (ProgramFromDb == null)
            {
                return NotFound();
            }
            return View(ProgramFromDb);
        }

        // POST: Program/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ADStarter.Models.Program obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Program.Update(obj);
                _unitOfWork.Save();
                TempData["Success"] = "Program updated succesfully";
                return RedirectToAction("ListPrograms");
            }
            return View(obj);
        }


        // GET: Program/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            ADStarter.Models.Program? ProgramFromDb = _unitOfWork.Program.Get(u => u.prog_ID == id);
            if (ProgramFromDb == null)
            {
                return NotFound();
            }
            return View(ProgramFromDb);
        }

        // POST: Program/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            ADStarter.Models.Program? obj = _unitOfWork.Program.Get(u => u.prog_ID == id);
            if (obj == null )
            {
                return NotFound();
            }
            _unitOfWork.Program.Remove(obj);
            _unitOfWork.Save();
            TempData["Success"] = "Program deleted succesfully";
            return RedirectToAction("Index");
        }

        // GET: Program/ListPrograms
        // GET: Program/ListPrograms
        // GET: Program/ListPrograms
        ////public IActionResult ListPrograms()
        ////{
        //// List<Program> programList = _unitOfWork.Program.GetAll();
        ////    return View(programList);
        ////}

        ////private bool ProgramExists(int id)
        ////{
        ////    return _unitOfWork.Program.Get(u => u.prog_ID == id) != null;
        ////}
        // GET: Program/ListPrograms
        // GET: Program/ListPrograms
        public IActionResult ListPrograms()
        {
            var products = _unitOfWork.Program.GetAll().ToList(); // Fetch all products from the database
            return View(products);
        }

        private bool ProgramExists(int id)
        {
            return _unitOfWork.Program.Get(u => u.prog_ID == id) != null;
        }
    }
}
