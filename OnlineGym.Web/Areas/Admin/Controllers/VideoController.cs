using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineGym.Entities.Models;
using OnlineGym.Entities.Repository;
using OnlineGym.Utilities;

namespace OnlineGym.Web.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = SD.AdminRole)]
	public class VideoController : Controller
	{

		private readonly IUnitOfWork _context;

		public VideoController(IUnitOfWork context)
		{
			_context = context;
		}

		public IActionResult Index()
		{
			return View(_context.Video.GetAll().ToList());
		}

		[HttpGet]
		public IActionResult Create()
		{
			Video video = new Video();


			return View(video);

		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create(Video video)
		{
			if (ModelState.IsValid)
			{


				_context.Video.Add(video);
				_context.Comlete();

				return RedirectToAction("Index");
			}


			return View(video);

		}





        [HttpDelete]
        public IActionResult Delete(int id)
        {
            Video video = _context.Video.GetFirstOrDefualt(e => e.Id == id);
            if (video != null)
            {
                _context.Video.Delete(video);
            }
            _context.Comlete();

            return Json(new { success = true, message = "Item has been deleted" });

        }

    }
}
