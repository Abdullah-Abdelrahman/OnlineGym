using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineGym.Entities.Models;
using OnlineGym.Entities.Repository;
using OnlineGym.Entities.ViewModels;
using OnlineGym.Utilities;

namespace OnlineGym.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.AdminRole)]
    public class ExerciseController : Controller
    {

        private readonly IUnitOfWork _context;

        public ExerciseController(IUnitOfWork context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View(_context.Exercise.GetAll().ToList());
        }




        [HttpGet]
        public IActionResult Create()
        {
            ExerciseViewModel exerciseVM = new ExerciseViewModel();

            exerciseVM.Exercise=new Exercise();
            exerciseVM.Videos= _context.Video.GetAll().Select(i => new SelectListItem { Text = i.Title, Value = i.Id.ToString() }).ToList();
			return View(exerciseVM);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ExerciseViewModel exerciseVM)
        {
            if (exerciseVM.Exercise.Name!=""&&exerciseVM.Exercise.VideoUrl!="")
            {


                _context.Exercise.Add(exerciseVM.Exercise);
                _context.Comlete();

                return RedirectToAction("Index");
            }


            return View(exerciseVM);

        }


    }
}
