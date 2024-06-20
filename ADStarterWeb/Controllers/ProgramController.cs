using Microsoft.AspNetCore.Mvc;
using ADStarter.DataAccess.Data;
using ADStarter.Models;
using ADStarter.DataAccess.Repository.IRepository;
using System.ComponentModel;

namespace ADStarterWeb.Controllers
{
    public class ProgramController : Controller
    {
        // GET: Program/Create
        private readonly IUnitOfWork _unitOfWork;
        public ProgramController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        //public IActionResult Index()
        //{
        //    List<Program> objProgramList = _unitOfWork.Program.GetAll().ToList();
        //    return View(objProgramList);
        //}
        //public IActionResult Create()
        //{
        //    return View();
        //}
        //[HttpPost]
        //public IActionResult Create(Program obj)
        //{
            
        //}
        //public IActionResult Edit(int? id)
        //{

        //}
        public IActionResult Success()
        {
            return View();
        }
    }
}
