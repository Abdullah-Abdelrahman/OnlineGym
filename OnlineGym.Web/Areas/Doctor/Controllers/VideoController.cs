using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineGym.Entities.Repository;
using OnlineGym.Utilities;

namespace OnlineGym.Web.Areas.Doctor.Controllers
{
    [Area("Doctor")]
    [Authorize(Roles = SD.DoctorRole)]
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

    }
}
