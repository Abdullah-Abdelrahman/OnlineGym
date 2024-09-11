using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Newtonsoft.Json;
using NuGet.Protocol;
using OnlineGym.Entities.Models;
using OnlineGym.Entities.Repository;
using OnlineGym.Entities.ViewModels;
using OnlineGym.Utilities;

namespace OnlineGym.Web.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = SD.AdminRole)]
	public class SubscriptionController : Controller
	{

		private readonly IUnitOfWork _context;

		public SubscriptionController(IUnitOfWork context)
		{
			_context = context;
		}

		public IActionResult Index()
		{
	
			return View(_context.Subscription.GetAll().ToList());
		}


       
		[HttpGet]
        public IActionResult Create()
        {

            SubscriptionViewModel viewModel = new SubscriptionViewModel();

            viewModel.Benefits = _context.Benefit.GetAll().Select(i => new SelectListItem{Text=i.description,Value=i.BenefitId.ToString() });
            viewModel.BenefitIDs = new List<int>();
             
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(SubscriptionViewModel subscriptionViewModel)
        {


            if (ModelState.IsValid)
            {

                _context.Subscription.Add(subscriptionViewModel.Subscription);

                _context.Comlete();


				int subId = _context.Subscription.last().SubscriptionId;
				
				for (int i = 0; i < subscriptionViewModel.BenefitIDs.Count; i++)
				{

					if (subscriptionViewModel.BenefitIDs[i] != 0)
					{
						_context.SubscriptionBenefit.Add(new SubscriptionBenefit { BenefitId = subscriptionViewModel.BenefitIDs[i], SubscriptionId = subId });
					}
				}

				
				_context.Comlete();
			}
			else
			{
				return View(subscriptionViewModel);
			}



			return RedirectToAction("Index");
        }



		[HttpGet]
		public IActionResult Update(int id)
		{

			SubscriptionViewModel subscriptionVM = new SubscriptionViewModel();

			subscriptionVM.Subscription = _context.Subscription.GetFirstOrDefualt(s => s.SubscriptionId == id);
			subscriptionVM.BenefitIDs = _context.SubscriptionBenefit.GetAll(s => s.SubscriptionId == id).Select(i => i.BenefitId).ToList();
			subscriptionVM.Benefits = _context.Benefit.GetAll().Select(i => new SelectListItem { Text = i.description, Value = i.BenefitId.ToString() });


			return View(subscriptionVM);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Update(SubscriptionViewModel subscriptionVM)
		{

			Subscription subscription = _context.Subscription.GetFirstOrDefualt(s => s.SubscriptionId == subscriptionVM.Subscription.SubscriptionId);


			List<SubscriptionBenefit> subscriptionBenefits = _context.SubscriptionBenefit.GetAll(s => s.SubscriptionId == subscriptionVM.Subscription.SubscriptionId).ToList();

			// remove deleted ones
			for (int i = 0; i < subscriptionBenefits.Count; i++)
			{
				if (!subscriptionVM.BenefitIDs.Any(b => b == subscriptionBenefits[i].BenefitId))
				{
					subscriptionBenefits.Remove(subscriptionBenefits[i]);
				}

			}

			for (int i = 0; i < subscriptionVM.BenefitIDs.Count; i++)
			{
				if (!subscriptionBenefits.Any(b => b.BenefitId == subscriptionVM.BenefitIDs[i]))
				{
					SubscriptionBenefit subscriptionBenefit = new SubscriptionBenefit()
					{
						SubscriptionId = subscription.SubscriptionId,
						BenefitId = subscriptionVM.BenefitIDs[i]
					};

					subscriptionBenefits.Add(subscriptionBenefit);
				}

			}

			subscription.SubscriptionBenefits = subscriptionBenefits;
			_context.Comlete();

			return RedirectToAction("Index");

		}



        [HttpGet]
        public IActionResult ChangeState(int id)
		{
			
			if (id != -1)
			{
                Subscription subscription = _context.Subscription.GetFirstOrDefualt(s => s.SubscriptionId == id);

                if (subscription.IsActive == true)
                {

                    subscription.IsActive = false;
                    _context.Comlete();
                    int c = _context.Subscription.GetAll(s => s.IsActive == true).ToList().Count;

                    string json = JsonConvert.SerializeObject(new { count = c });
                    return Json(new { success = true, data = json, Message = "sub has DisActivated" });

                }
                else
                {
                    subscription.IsActive = true;
                    _context.Comlete();
                    int c = _context.Subscription.GetAll(s => s.IsActive == true).ToList().Count;

                    string json = JsonConvert.SerializeObject(new { count = c });
                    return Json(new { success = true, data = json, Message = "sub has Activated" });

                }

            }
            else
			{

				Console.WriteLine("nuguygyyffhjtydffhd");
                int c = _context.Subscription.GetAll(s => s.IsActive == true).ToList().Count;

                string json = JsonConvert.SerializeObject(new { count = c });
                return Json(new { success = true, data = json,});
            }
			
		


        }




		[HttpGet]

		public IActionResult Details(int id)
		{
            SubscriptionViewModel subscriptionVM = new SubscriptionViewModel();

            subscriptionVM.Subscription = _context.Subscription.GetFirstOrDefualt(s => s.SubscriptionId == id);
            subscriptionVM.BenefitIDs = _context.SubscriptionBenefit.GetAll(s => s.SubscriptionId == id).Select(i => i.BenefitId).ToList();
            subscriptionVM.Benefits = _context.Benefit.GetAll().Select(i => new SelectListItem { Text = i.description, Value = i.BenefitId.ToString() });

            return View(subscriptionVM);
		}
	}
}
