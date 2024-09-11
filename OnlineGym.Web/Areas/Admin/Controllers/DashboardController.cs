using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineGym.Entities.Repository;
using OnlineGym.Utilities;

namespace OnlineGym.Web.Areas.Admin.Controllers
{

    [Area("Admin")]
    [Authorize(Roles =SD.AdminRole)]
    public class DashboardController : Controller
    {


        private readonly IUnitOfWork _context;

        public DashboardController(IUnitOfWork context)
        {
            _context = context;
        }

        public IActionResult Index()
        {

            List<int> informations=new List<int>(4) { 0,0,0,0};


            informations[2] = _context.Client.GetAll().ToList().Count;
            informations[0]=_context.ClientSubscription.GetAll(c=>c.Status=="Aproved").ToList().Count;

			informations[1] = _context.ClientSubscription.GetAll(c => c.Status == "Proccessed").ToList().Count;
			return View(informations);
        }


    }

   
}
