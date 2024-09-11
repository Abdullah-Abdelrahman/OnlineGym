using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineGym.Entities.Repository;
using OnlineGym.Utilities;

namespace OnlineGym.Web.Areas.Coach.Controllers
{
	[Area("Coach")]
	[Authorize(Roles = SD.CoachRole)]
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

	}
}
