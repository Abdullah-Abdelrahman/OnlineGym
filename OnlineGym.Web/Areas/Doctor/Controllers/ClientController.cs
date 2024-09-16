using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineGym.Entities.Models;
using OnlineGym.Entities.Repository;
using OnlineGym.Entities.ViewModels;
using OnlineGym.Utilities;
using System.Security.Claims;

namespace OnlineGym.Web.Areas.Doctor.Controllers
{
    [Area("Doctor")]
    [Authorize(Roles = SD.DoctorRole)]
    public class ClientController : Controller
    {
        private readonly IUnitOfWork _context;

        public ClientController(IUnitOfWork context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {

            var clamisIdentity = (ClaimsIdentity)User.Identity;
            var claim = clamisIdentity.FindFirst(ClaimTypes.NameIdentifier);

            int empId =(await _context.Employee.GetFirstOrDefualtAsync(e => e.UserId == claim.Value)).EmployeeId;

            var ordersDetails = (await _context.ClientSubscriptionDetailsEmployee.GetAllAsync(cs => cs.EmployeeId ==empId)).ToList();



            List<OrderViewModel> orders = new List<OrderViewModel>();

            for (int i = 0; i < ordersDetails.Count; i++)
            {
                orders.Add(new OrderViewModel()
                {
                    ClientSubscription = _context.ClientSubscription.GetFirstOrDefualt(c => c.ClientSubscriptionId == ordersDetails[i].ClientSubscriptionId, IncludeWord: "Subscription,Client,ClientSubscriptionDetails"),
                    hasPlan =await GetOrderHasPlan(ordersDetails[i].ClientSubscriptionId)
                });
            }

            return View(orders);
        }


        [HttpPost]
        public async Task<IActionResult> ChangeOrderStartDate(int OrderId,DateTime? newdate)
        {


           
            if (newdate != null)
            {
                (await _context.ClientSubscriptionDetails.GetFirstOrDefualtAsync(cs=>cs.ClientSubscriptionId==OrderId)).StartDate = newdate;
				(await _context.ClientSubscriptionDetails.GetFirstOrDefualtAsync(cs => cs.ClientSubscriptionId == OrderId)).EndDate = newdate.Value.AddDays(27);
				_context.Comlete();


                if(_context.trainingPlan.GetFirstOrDefualt(t => t.ClientSubscriptionId == OrderId) != null)
                {
					_context.trainingPlan.GetFirstOrDefualt(t => t.ClientSubscriptionId == OrderId).Started = newdate;
					
				}
					
				
              


            }

            return RedirectToAction("Index");
        }



        [HttpGet]
        public async Task<bool> GetOrderHasPlan(int OrderId)
        {
            bool hasPlan=_context.trainingPlan.GetFirstOrDefualt(t=>t.ClientSubscriptionId == OrderId) !=null;

            return hasPlan;
        }
    }
}
