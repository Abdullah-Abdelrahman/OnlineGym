using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineGym.Entities.Models;
using OnlineGym.Entities.Repository;
using OnlineGym.Entities.ViewModels;
using OnlineGym.Utilities;
using Stripe;

namespace OnlineGym.Web.Areas.Admin.Controllers
{
	[Area("Admin")]
    [Authorize(Roles = SD.AdminRole)]
    public class OrderController : Controller
	{


        private readonly IUnitOfWork _context;

        public OrderController(IUnitOfWork context)
        {
            _context = context;
        }


        public IActionResult GetData()
        {
            var Orders = _context.ClientSubscription.GetAll(IncludeWord: "Client,Subscription");

            //Console.WriteLine(employees.First().JobTitle.JopName);
            return Json(new { data = Orders });
        }

        public IActionResult Index()
		{
			return View();
		}

        public IActionResult Details(int id)
        {

            ClientSubscriptionDetails clientSubscriptionDetails = _context.ClientSubscriptionDetails.GetFirstOrDefualt(c=>c.ClientSubscriptionId==id);

            clientSubscriptionDetails.clientSubscription = _context.ClientSubscription.GetFirstOrDefualt(c => c.ClientSubscriptionId == id, IncludeWord: "Client");
            

            return View(clientSubscriptionDetails);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CancelOrder(int id)
        {
            ClientSubscription clientSubscription = _context.ClientSubscription.GetFirstOrDefualt(c => c.ClientSubscriptionId == id);

            if(clientSubscription.Status=="Aproved")
            {
                var option = new RefundCreateOptions()
                {
                    Reason=RefundReasons.RequestedByCustomer,
                    PaymentIntent=clientSubscription.PaymentIntentId
                };

                var service = new RefundService();
                 Refund refund= service.Create(option);
                clientSubscription.Status = "Canceled";

				_context.ClientSubscription.Uppdate(clientSubscription);
                _context.Comlete();

            }
            else
            {
				clientSubscription.Status = "Canceled";
				_context.ClientSubscription.Uppdate(clientSubscription);
				_context.Comlete();
			}

            return RedirectToAction("Details", new { id = clientSubscription.ClientSubscriptionId });
        }


        [HttpGet]
        public IActionResult Proccessing(int id)
        {
            

            ClientSubscription cs = _context.ClientSubscription.GetFirstOrDefualt(c => c.ClientSubscriptionId == id);
            cs.Status = "Proccessing";
			_context.ClientSubscription.Uppdate(cs);
			_context.Comlete();


			cs.Subscription = _context.Subscription.GetFirstOrDefualt(s => s.SubscriptionId == cs.SubscriptionId, IncludeWord: "Benefits");




            

			ProccessOrderViewModel POVM = new ProccessOrderViewModel();

            POVM.orderId = id;

            POVM.JobsNeed =new List<JobTitle>();


            POVM.Emps=new List<IEnumerable<SelectListItem>>();
            foreach(var i in cs.Subscription.Benefits)
            {
                List<JobTitle> jobTitles = _context.Benefit.GetFirstOrDefualt(b => b.BenefitId == i.BenefitId, IncludeWord: "jobTitles").jobTitles.ToList();

                for(int j=0; j<jobTitles.Count; j++)
                {
                    if (!POVM.JobsNeed.Any(b=>b.JobTitleId==jobTitles[j].JobTitleId))
                    {
                        POVM.JobsNeed.Add(jobTitles[j]);


                        POVM.Emps.Add(_context.Employee.GetAll(e => e.JobTitleId == jobTitles[j].JobTitleId).Select(p => new SelectListItem { Text = p.Name, Value = p.EmployeeId.ToString() }));

                    }
                }
            }

			return View(POVM);
        }

		[HttpPost]
        [ValidateAntiForgeryToken]
		public IActionResult Proccessing(ProccessOrderViewModel POVM)
		{
			ClientSubscription cs = _context.ClientSubscription.GetFirstOrDefualt(c => c.ClientSubscriptionId == POVM.orderId);

			 cs.Subscription = _context.Subscription.GetFirstOrDefualt(s => s.SubscriptionId == cs.SubscriptionId, IncludeWord: "Benefits");

			POVM.JobsNeed = new List<JobTitle>();

			POVM.Emps = new List<IEnumerable<SelectListItem>>();
			foreach (var i in cs.Subscription.Benefits)
			{
				List<JobTitle> jobTitles = _context.Benefit.GetFirstOrDefualt(b => b.BenefitId == i.BenefitId, IncludeWord: "jobTitles").jobTitles.ToList();

				for (int j = 0; j < jobTitles.Count; j++)
				{
					if (!POVM.JobsNeed.Any(b => b.JobTitleId == jobTitles[j].JobTitleId))
					{
						POVM.JobsNeed.Add(jobTitles[j]);


						POVM.Emps.Add(_context.Employee.GetAll(e => e.JobTitleId == jobTitles[j].JobTitleId).Select(p => new SelectListItem { Text = p.Name, Value = p.EmployeeId.ToString() }));

					}
				}
			}


			if (POVM!=null&&POVM.valid())
            {
				
				cs.Status = "Proccessed";
				_context.ClientSubscription.Uppdate(cs);
				_context.Comlete();
			}
            else
            {
				return View(POVM);

			}

            return RedirectToAction("Details",new { id = POVM.orderId });











		}



        [HttpGet]
        public IActionResult Shipping(int id)
        {
            return View();
        }

		[HttpPost]
		public IActionResult Shipping()
		{
			return View();
		}

	}
}
