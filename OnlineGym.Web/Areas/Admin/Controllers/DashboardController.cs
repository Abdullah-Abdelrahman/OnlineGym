using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineGym.Entities.Repository;
using OnlineGym.Entities.ViewModels;
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

        public async Task<IActionResult> Index()
        {

            var Users = (await _context.Client.GetAllAsync()).ToList();

            var subscriptions =(await _context.Subscription.GetAllAsync()).ToList();

            var Orders=(await _context.ClientSubscription.GetAllAsync()).ToList();


            StaticsForAdmin informations = new StaticsForAdmin(Users, Orders, subscriptions);

            return View(informations);
        }



      
    }

   
}
