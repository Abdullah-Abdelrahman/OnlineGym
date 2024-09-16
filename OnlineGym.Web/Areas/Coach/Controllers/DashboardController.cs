using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineGym.Entities.Models;
using OnlineGym.Entities.Repository;
using OnlineGym.Utilities;
using System.Security.Claims;

namespace OnlineGym.Web.Areas.Coach.Controllers
{
	[Area("Coach")]
    [Authorize(Roles = SD.CoachRole)]
    public class DashboardController : Controller
	{

        private readonly IUnitOfWork _context;

        public DashboardController(IUnitOfWork context)
        {
            _context = context;
        }
        
        public async Task<IActionResult> Index()
		{

            var clamisIdentity = (ClaimsIdentity)User.Identity;
            var claim = clamisIdentity.FindFirst(ClaimTypes.NameIdentifier);

            int empId = (await _context.Employee.GetFirstOrDefualtAsync(e => e.UserId == claim.Value)).EmployeeId;
           
          

        

			List<int> list = new List<int>(3) { 0,0,0};

            if (empId != null)
            {
               

                list[0] =(await _context.ClientSubscriptionDetails.GetAllAsync(c => c.ClientSelectedEmployees.Any(i => i.EmployeeId == empId))).ToList().Count;

               
            }

          
			
			return View(list);
		}
	}
}
