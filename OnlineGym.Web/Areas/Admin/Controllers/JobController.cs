using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineGym.DataAccess.Data;
using OnlineGym.DataAccess.RepositoryImplementation;
using OnlineGym.Entities.Models;
using OnlineGym.Entities.Repository;
using OnlineGym.Utilities;


namespace OnlineGym.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles =SD.AdminRole)]
    public class JobController : Controller
    {
        private readonly IUnitOfWork _context;

        public JobController(IUnitOfWork context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View(_context.Jobs.GetAll());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(JobTitle job)
        {
            
            if (ModelState.IsValid)
            {
                _context.Jobs.Add(job);
                _context.Comlete();

                TempData["Created"] = "true";
                return RedirectToAction("Index");
            }
            else
            {
                return View(job);
            }


        }




        [HttpGet]
        public IActionResult Update(int id)
        {

            return View(_context.Jobs.GetFirstOrDefualt(e => e.JobTitleId == id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(JobTitle job)
        {

            if (ModelState.IsValid)
            {
                _context.Jobs.Update(job);
                _context.Comlete();
                TempData["Updated"] = "true";
                return RedirectToAction("Index");
            }
            else
            {
                return View(job);
            }


        }


        [HttpGet]
        public IActionResult Delete(int id)
        {

            return View(_context.Jobs.GetFirstOrDefualt(e => e.JobTitleId == id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(JobTitle job)
        {


            _context.Jobs.Delete(job);
            _context.Comlete();
            TempData["Deleted"] = "true";
            return RedirectToAction("Index");



        }
    }
}
