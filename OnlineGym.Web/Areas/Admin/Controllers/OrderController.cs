using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
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


       
        public IActionResult Proccessing(int id)
        {
            

            ClientSubscription cs = _context.ClientSubscription.GetFirstOrDefualt(c => c.ClientSubscriptionId == id);
            cs.Status = SD.Proccess;
			_context.ClientSubscription.Uppdate(cs);
			_context.Comlete();


			cs.Subscription = _context.Subscription.GetFirstOrDefualt(s => s.SubscriptionId == cs.SubscriptionId, IncludeWord: "Benefits");


			ProccessOrderViewModel POVM = new ProccessOrderViewModel();

            POVM.orderId = id;

            POVM.JobsNeed =new List<JobTitle>();


            POVM.Emps=new List<List<SelectListItem>>();
            foreach(var i in cs.Subscription.Benefits)
            {
                List<JobTitle> jobTitles = _context.Benefit.GetFirstOrDefualt(b => b.BenefitId == i.BenefitId, IncludeWord: "jobTitles").jobTitles.ToList();

                for(int j=0; j<jobTitles.Count; j++)
                {
                    if (!POVM.JobsNeed.Any(b=>b.JobTitleId==jobTitles[j].JobTitleId))
                    {
                        POVM.JobsNeed.Add(jobTitles[j]);

                        ClientSubscriptionDetailsEmployee CSDE = new ClientSubscriptionDetailsEmployee();

                        CSDE.ClientSubscriptionId = id;
                        Employee emp = _context.Employee.GetFirstOrDefualt(e => e.JobTitleId == jobTitles[j].JobTitleId, IncludeWord: "clientSubscriptionDetailsEmployees");

						CSDE.EmployeeId = emp.EmployeeId;
       
                        _context.ClientSubscriptionDetailsEmployee.Add(CSDE);
                        _context.Comlete();
                        

                    }
                }
            }




            ///
			_context.ClientSubscription.Uppdate(cs);
			_context.Comlete();
			return RedirectToAction("Details", new {id=id });
        }


        [HttpGet]
        public IActionResult TakeClientInformation(int id)
        {
			ClientSubscriptionDetails CsD = _context.ClientSubscriptionDetails.GetFirstOrDefualt(cs => cs.ClientSubscriptionId ==id);
			return View(CsD);
        }

		[HttpPost]
		public IActionResult TakeClientInformation(ClientSubscriptionDetails Details)
		{

            ClientSubscriptionDetails CsD=_context.ClientSubscriptionDetails.GetFirstOrDefualt(cs=>cs.ClientSubscriptionId==Details.ClientSubscriptionId);

            CsD.Gender =  Details?.Gender;
                          
            CsD.Hight = Details?.Hight;
                        
            CsD.Weight = Details?.Weight;
            CsD.BodyFat = Details?.BodyFat;
                         
            CsD.Age = Details?.Age;
                        
            CsD.Target = Details?.Target;
                         
            CsD.Diseases = Details?.Diseases;

			_context.Comlete();
			return RedirectToAction("Proccessed", new { id = Details.ClientSubscriptionId });
		}

	
		public IActionResult Proccessed(int id)
		{

			ClientSubscription cs = _context.ClientSubscription.GetFirstOrDefualt(c => c.ClientSubscriptionId == id);
			cs.Status = SD.proccessed;
			_context.ClientSubscription.Uppdate(cs);
			_context.Comlete();
			return RedirectToAction("Details", new { id = id });
		}


		[HttpPost]
		public IActionResult OrderEnd(int id)
		{

			ClientSubscription cs = _context.ClientSubscription.GetFirstOrDefualt(c => c.ClientSubscriptionId == id);
			cs.Status = SD.Finished;
			_context.ClientSubscription.Uppdate(cs);
			_context.Comlete();
			return View();
		}

	}
}
