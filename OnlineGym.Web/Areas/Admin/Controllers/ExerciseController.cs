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
        public async Task<IActionResult> Index()
        {
            return View((await _context.Exercise.GetAllAsync()).ToList());
        }




        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ExerciseViewModel exerciseVM = new ExerciseViewModel();

            exerciseVM.Exercise=new Exercise();
            exerciseVM.Videos=(await _context.Video.GetAllAsync()).Select(i => new SelectListItem { Text = i.Title, Value = i.Id.ToString() }).ToList();
			return View(exerciseVM);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ExerciseViewModel exerciseVM)
        {
            if (exerciseVM.Exercise.Name!=""&&exerciseVM.Exercise.VideoUrl!="")
            {


                await _context.Exercise.AddAsync(exerciseVM.Exercise);
                _context.Comlete();

                return RedirectToAction("Index");
            }


            return View(exerciseVM);

        }


    }
}
