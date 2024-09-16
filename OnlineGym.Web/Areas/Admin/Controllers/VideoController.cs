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

		public async Task<IActionResult> Index()
		{
			return View((await _context.Video.GetAllAsync()).ToList());
		}

		[HttpGet]
		public async Task<IActionResult> Create()
		{
			Video video = new Video();


			return View(video);

		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(Video video)
		{
			if (ModelState.IsValid)
			{


				_context.Video.AddAsync(video);
				_context.Comlete();

				return RedirectToAction("Index");
			}


			return View(video);

		}


		[HttpGet]
		public async Task<IActionResult> Update(int id)
		{

			return View(await _context.Video.GetFirstOrDefualtAsync(e => e.Id == id));
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Update(Video video)
		{

			if (ModelState.IsValid)
			{
				_context.Video.Update(video);
				_context.Comlete();
				TempData["Updated"] = "true";
				return RedirectToAction("Index");
			}
			else
			{
				return View(video);
			}


		}


		[HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            Video video =await _context.Video.GetFirstOrDefualtAsync(e => e.Id == id);
            if (video != null)
            {
                _context.Video.Delete(video);
            }
            _context.Comlete();

            return Json(new { success = true, message = "Item has been deleted" });

        }

    }
}
