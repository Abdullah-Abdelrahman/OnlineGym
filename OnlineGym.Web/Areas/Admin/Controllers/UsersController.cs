using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineGym.DataAccess.Data;
using OnlineGym.Entities.Repository;
using OnlineGym.Utilities;

namespace OnlineGym.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.AdminRole)]
    public class UsersController : Controller
    {


        private readonly IUnitOfWork _context;

        public UsersController(IUnitOfWork context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View(_context.Client.GetAll());
        }

        public IActionResult LockUnlock(string? id)
        {

            var user=_context.Client.GetFirstOrDefualt(x=>x.Id == id);
            if(user == null)
            {
                return NotFound();
            }
           
            if(user.LockoutEnd==null|| user.LockoutEnd <= DateTime.Now)
            {
                user.LockoutEnd = DateTime.Now.AddYears(10);
            }
            else
            {
                user.LockoutEnd = DateTime.Now;
            }

            _context.Client.update(user);
            _context.Comlete();
            return RedirectToAction("index");
        }
    }
}
