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
		public async Task<IActionResult> Index()
		{


			return View((await _context.Exercise.GetAllAsync()).ToList());
		}

	}
}
