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
    public class ClientController : Controller
    {
        private readonly IUnitOfWork _context;

        public ClientController(IUnitOfWork context)
        {
            _context = context;
        }
        public IActionResult Index()
        {

            var clamisIdentity = (ClaimsIdentity)User.Identity;
            var claim = clamisIdentity.FindFirst(ClaimTypes.NameIdentifier);

            int empId = _context.Employee.GetFirstOrDefualt(e => e.UserId == claim.Value).EmployeeId;

            var ordersDetails = _context.ClientSubscriptionDetailsEmployee.GetAll(cs => cs.EmployeeId ==empId).ToList();

            List<ClientSubscription> orders = new List<ClientSubscription>();

            for (int i = 0; i < ordersDetails.Count; i++)
            {
                orders.Add(_context.ClientSubscription.GetFirstOrDefualt(c => c.ClientSubscriptionId == ordersDetails[i].ClientSubscriptionId,IncludeWord: "Subscription,Client"));
            }

            return View(orders);
        }

      
    }
}
