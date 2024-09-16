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

		public async Task<IActionResult> Index()
		{
	
			return View((await _context.Subscription.GetAllAsync()).ToList());
		}


       
		[HttpGet]
        public async Task<IActionResult> Create()
        {

            SubscriptionViewModel viewModel = new SubscriptionViewModel();

            viewModel.Benefits =(await _context.Benefit.GetAllAsync()).Select(i => new SelectListItem{Text=i.description,Value=i.BenefitId.ToString() });
            viewModel.BenefitIDs = new List<int>();
             
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SubscriptionViewModel subscriptionViewModel)
        {


            if (ModelState.IsValid)
            {

                await _context.Subscription.AddAsync(subscriptionViewModel.Subscription);

                _context.Comlete();


				int subId = _context.Subscription.last().SubscriptionId;
				
				for (int i = 0; i < subscriptionViewModel.BenefitIDs.Count; i++)
				{

					if (subscriptionViewModel.BenefitIDs[i] != 0)
					{
						await _context.SubscriptionBenefit.AddAsync(new SubscriptionBenefit { BenefitId = subscriptionViewModel.BenefitIDs[i], SubscriptionId = subId });
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
		public async Task<IActionResult> Update(int id)
		{

			SubscriptionViewModel subscriptionVM = new SubscriptionViewModel();

			subscriptionVM.Subscription =await _context.Subscription.GetFirstOrDefualtAsync(s => s.SubscriptionId == id);
			subscriptionVM.BenefitIDs =(await _context.SubscriptionBenefit.GetAllAsync(s => s.SubscriptionId == id)).Select(i => i.BenefitId).ToList();
			subscriptionVM.Benefits =(await _context.Benefit.GetAllAsync()).Select(i => new SelectListItem { Text = i.description, Value = i.BenefitId.ToString() });


			return View(subscriptionVM);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Update(SubscriptionViewModel subscriptionVM)
		{

			Subscription subscription =await _context.Subscription.GetFirstOrDefualtAsync(s => s.SubscriptionId == subscriptionVM.Subscription.SubscriptionId);

			subscription.DurationDays=subscriptionVM.Subscription.DurationDays;
            subscription.SubscriptionName = subscriptionVM.Subscription.SubscriptionName;
            List<SubscriptionBenefit> subscriptionBenefits = (await _context.SubscriptionBenefit.GetAllAsync(s => s.SubscriptionId == subscriptionVM.Subscription.SubscriptionId)).ToList();

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
        public async Task<IActionResult> ChangeState(int id)
		{
			
			if (id != -1)
			{
                Subscription subscription =await _context.Subscription.GetFirstOrDefualtAsync(s => s.SubscriptionId == id);

                if (subscription.IsActive == true)
                {

                    subscription.IsActive = false;
                    _context.Comlete();
                    int c =(await _context.Subscription.GetAllAsync(s => s.IsActive == true)).ToList().Count;

                    string json = JsonConvert.SerializeObject(new { count = c });
                    return Json(new { success = true, data = json, Message = "sub has DisActivated" });

                }
                else
                {
                    subscription.IsActive = true;
                    _context.Comlete();
                    int c =(await _context.Subscription.GetAllAsync(s => s.IsActive == true)).ToList().Count;

                    string json = JsonConvert.SerializeObject(new { count = c });
                    return Json(new { success = true, data = json, Message = "sub has Activated" });

                }

            }
            else
			{

				Console.WriteLine("nuguygyyffhjtydffhd");
                int c =(await _context.Subscription.GetAllAsync(s => s.IsActive == true)).ToList().Count;

                string json = JsonConvert.SerializeObject(new { count = c });
                return Json(new { success = true, data = json,});
            }
			
		


        }




		[HttpGet]

		public async Task<IActionResult> Details(int id)
		{
            SubscriptionViewModel subscriptionVM = new SubscriptionViewModel();

            subscriptionVM.Subscription =await _context.Subscription.GetFirstOrDefualtAsync(s => s.SubscriptionId == id);
            subscriptionVM.BenefitIDs =(await _context.SubscriptionBenefit.GetAllAsync(s => s.SubscriptionId == id)).Select(i => i.BenefitId).ToList();
            subscriptionVM.Benefits =(await _context.Benefit.GetAllAsync()).Select(i => new SelectListItem { Text = i.description, Value = i.BenefitId.ToString() });

            return View(subscriptionVM);
		}
	}
}
