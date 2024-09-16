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


        public async Task<IActionResult> GetData()
        {
            //ClientSubscription is simply an Order
            var Orders =await _context.ClientSubscription.GetAllAsync(IncludeWord: "Client,Subscription");

            //Console.WriteLine(employees.First().JobTitle.JopName);
            return Json(new { data = Orders });
        }

        public async Task<IActionResult> Index()
		{
			return View();
		}

        public async Task<IActionResult> Details(int id)
        {

            //clientSubscriptionDetails is simply the order Ditails
            //has the same id as the order(ClientSubscription)
            ClientSubscriptionDetails clientSubscriptionDetails =await  _context.ClientSubscriptionDetails.GetFirstOrDefualtAsync(c=>c.ClientSubscriptionId==id);
            //clientSubscription is the order
            clientSubscriptionDetails.clientSubscription = await _context.ClientSubscription.GetFirstOrDefualtAsync(c => c.ClientSubscriptionId == id, IncludeWord: "Client");
            

            return View(clientSubscriptionDetails);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CancelOrder(int id)
        {
            ClientSubscription clientSubscription = await _context.ClientSubscription.GetFirstOrDefualtAsync(c => c.ClientSubscriptionId == id);

            //You can cancel if its in Aproved State Only
            if(clientSubscription.Status==SD.Aproved)
            {
                //Stripe
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


       //this methode is used to set the team for an client Subscrption
       //in other word add the selected team to an order to handel
        public async Task<IActionResult> Proccessing(int id)
        {
            
            //get the order frome database
            ClientSubscription cs =await _context.ClientSubscription.GetFirstOrDefualtAsync(c => c.ClientSubscriptionId == id);
            //change its state to proccess
            cs.Status = SD.Proccess;
			_context.ClientSubscription.Uppdate(cs);
			_context.Comlete();


            //get the Subscription type in the order
			cs.Subscription =await _context.Subscription.GetFirstOrDefualtAsync(s => s.SubscriptionId == cs.SubscriptionId, IncludeWord: "Benefits");


			ProccessOrderViewModel POVM = new ProccessOrderViewModel();
            // again order id is ClientSubscriptionId
            POVM.orderId = id;


            //iterate over all benefite in the subscription
            foreach(var i in cs.Subscription.Benefits)
            {
                // get all of the jobs type needed for the benefit
                List<JobTitle> jobTitles = (await _context.Benefit.GetFirstOrDefualtAsync(b => b.BenefitId == i.BenefitId, IncludeWord: "jobTitles")).jobTitles.ToList();

                //iterate over  all of the jobs in the benefit
                for (int j=0; j<jobTitles.Count; j++)
                {
                    // if the the list of the jobs in the
                    // viewModel does not has this current job enter
                    if (!POVM.JobsNeed.Any(b=>b.JobTitleId==jobTitles[j].JobTitleId))
                    {
                        //insert the needed job
                        POVM.JobsNeed.Add(jobTitles[j]);

                        //this class define the relathion between an order
                        //and the employees selected for the order
                        ClientSubscriptionDetailsEmployee CSDE = new ClientSubscriptionDetailsEmployee();
                        
                        CSDE.ClientSubscriptionId = id;
                        //get an employee that its job matches the neded one
                        Employee emp =await _context.Employee.GetFirstOrDefualtAsync(e => e.JobTitleId == jobTitles[j].JobTitleId, IncludeWord: "clientSubscriptionDetailsEmployees");

						CSDE.EmployeeId = emp.EmployeeId;
       
                        await _context.ClientSubscriptionDetailsEmployee.AddAsync(CSDE);
                        _context.Comlete();
                        

                    }
                }
            }




            ///
			_context.ClientSubscription.Uppdate(cs);
			_context.Comlete();
			return RedirectToAction("Details", new {id=id });
        }


        // Methode that responspile for adding
        // the Clients information for a spcific Order
        [HttpGet]
        public async Task<IActionResult> TakeClientInformation(int id)
        {
			ClientSubscriptionDetails CsD = await _context.ClientSubscriptionDetails.GetFirstOrDefualtAsync(cs => cs.ClientSubscriptionId ==id);
			return View(CsD);
        }

		[HttpPost]
		public async Task<IActionResult> TakeClientInformation(ClientSubscriptionDetails Details)
		{

            ClientSubscriptionDetails CsD=await _context.ClientSubscriptionDetails.GetFirstOrDefualtAsync(cs=>cs.ClientSubscriptionId==Details.ClientSubscriptionId);

            // did not make validation !!!!! to run fast
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

	
		public async Task<IActionResult> Proccessed(int id)
		{

			ClientSubscription cs =await _context.ClientSubscription.GetFirstOrDefualtAsync(c => c.ClientSubscriptionId == id);
			cs.Status = SD.proccessed;
			_context.ClientSubscription.Uppdate(cs);
			_context.Comlete();
			return RedirectToAction("Details", new { id = id });
		}


        //caled when the Subscription time end
		[HttpPost]
		public async Task<IActionResult> OrderEnd(int id)
		{

			ClientSubscription cs =await _context.ClientSubscription.GetFirstOrDefualtAsync(c => c.ClientSubscriptionId == id);
			cs.Status = SD.Finished;
			_context.ClientSubscription.Uppdate(cs);
			_context.Comlete();
			return View();
		}

	}
}
