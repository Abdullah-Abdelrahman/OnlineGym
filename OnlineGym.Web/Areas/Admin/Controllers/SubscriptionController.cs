using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
    }
}
