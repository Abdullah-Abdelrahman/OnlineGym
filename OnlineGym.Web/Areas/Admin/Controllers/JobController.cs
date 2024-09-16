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

        public async Task<IActionResult> Index()
        {
            return View((await _context.Jobs.GetAllAsync()).ToList());
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(JobTitle job)
        {
            
            if (ModelState.IsValid)
            {
                await _context.Jobs.AddAsync(job);
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
        public async Task<IActionResult> Update(int id)
        {

            return View(await _context.Jobs.GetFirstOrDefualtAsync(e => e.JobTitleId == id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(JobTitle job)
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
        public async Task<IActionResult> Delete(int id)
        {

            return View(await _context.Jobs.GetFirstOrDefualtAsync(e => e.JobTitleId == id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(JobTitle job)
        {


            _context.Jobs.Delete(job);
            _context.Comlete();
            TempData["Deleted"] = "true";
            return RedirectToAction("Index");



        }
    }
}
