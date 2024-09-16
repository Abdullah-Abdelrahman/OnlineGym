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

        public async Task<IActionResult> Index()
        {
            return View(await _context.Client.GetAllAsync());
        }
        //to Lock or Unlock an user
        public async Task<IActionResult> LockUnlock(string? id)
        {
            //get the user
            var user=await _context.Client.GetFirstOrDefualtAsync(x=>x.Id == id);
            if(user == null)
            {
                return NotFound();
            }

            //if LockoutEnd time is less than the time now
            //that mean that this user is not Locked so Lock him
            if (user.LockoutEnd==null|| user.LockoutEnd <= DateTime.Now)
            {
                // change the time to end the lock to a large date
                //you can add as many time as you want 
                user.LockoutEnd = DateTime.Now.AddYears(10);
            }
            else
            {
                //if the use is locke then simply change the
                //LockoutEnd time to the time now to end the lock
                user.LockoutEnd = DateTime.Now;
            }

            _context.Client.update(user);
            _context.Comlete();
            return RedirectToAction("index");
        }
    }
}
