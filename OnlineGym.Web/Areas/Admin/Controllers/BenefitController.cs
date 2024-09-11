using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineGym.Entities.Models;
using OnlineGym.Entities.Repository;
using OnlineGym.Entities.ViewModels;
using OnlineGym.Utilities;

namespace OnlineGym.Web.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = SD.AdminRole)]
	public class BenefitController : Controller
	{
		private readonly IUnitOfWork _context;

		public BenefitController(IUnitOfWork context)
		{
			_context = context;
		}
		public IActionResult Index()
		{
			return View(_context.Benefit.GetAll());
		}
		[HttpGet]
		public IActionResult Create()
		{
			BenefitViewModel benefitViewModel = new BenefitViewModel();

			benefitViewModel.Benefit=new Benefit();
			benefitViewModel.JobsId=new List<int>();
			benefitViewModel.Jobs = _context.Jobs.GetAll().Select(i => new SelectListItem { Text = i.JopName, Value = i.JobTitleId.ToString() });

		
			return View(benefitViewModel);
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create(BenefitViewModel benefitVM)
		{
			if (ModelState.IsValid&&benefitVM.JobsId!=null)
			{
				_context.Benefit.Add(benefitVM.Benefit);
				_context.Comlete();



				int BenId = _context.Benefit.last().BenefitId;

				
				for (int i = 0; i < benefitVM?.JobsId?.Count; i++)
				{

					if (benefitVM.JobsId[i] != 0)
					{
						_context.BenefitJobTitle.Add(new BenefitJobTitle { BenefitId =BenId , JobTitleId= benefitVM.JobsId[i] });
					}
				}


				_context.Comlete();



				return RedirectToAction("Index");
			}
			else
			{
				return View(benefitVM);
			}

			
		}





		[HttpGet]
		public IActionResult Update(int id)
		{

			BenefitViewModel benefitViewModel = new BenefitViewModel();

			benefitViewModel.Benefit = _context.Benefit.GetFirstOrDefualt(b=>b.BenefitId==id);
			benefitViewModel.JobsId = _context.BenefitJobTitle.GetAll(b=>b.BenefitId==id).Select(i=>i.JobTitleId).ToList();
			benefitViewModel.Jobs = _context.Jobs.GetAll().Select(i => new SelectListItem { Text = i.JopName, Value = i.JobTitleId.ToString() });


			return View(benefitViewModel);
		}

        [HttpPost]
		[ValidateAntiForgeryToken]
        public IActionResult Update(BenefitViewModel benefitViewModel)
        {

			Benefit benefit = _context.Benefit.GetFirstOrDefualt(e => e.BenefitId == benefitViewModel.Benefit.BenefitId);


			List<BenefitJobTitle> benefitJobTitles = _context.BenefitJobTitle.GetAll(b => b.BenefitId == benefitViewModel.Benefit.BenefitId).ToList();

			// remove deleted ones
			for (int i = 0; i < benefitJobTitles.Count; i++)
			{
				if (!benefitViewModel.JobsId.Any(b => b == benefitJobTitles[i].JobTitleId))
				{
					benefitJobTitles.Remove(benefitJobTitles[i]);
				}

			}

			for (int i = 0; i < benefitViewModel.JobsId.Count; i++)
			{
				if(!benefitJobTitles.Any(b=>b.JobTitleId== benefitViewModel.JobsId[i]))
				{
					BenefitJobTitle benefitJobTitle = new BenefitJobTitle()
					{
						BenefitId=benefit.BenefitId,
						JobTitleId= benefitViewModel.JobsId[i]
					};

					benefitJobTitles.Add(benefitJobTitle);
				}
				
			}

			benefit.benefitJobTitles= benefitJobTitles;
			_context.Comlete();

			return RedirectToAction("Index");

        }



        [HttpDelete]
        public IActionResult Delete(int id)
        {
            Benefit benefit = _context.Benefit.GetFirstOrDefualt(e => e.BenefitId == id);

          


            if (benefit != null)
            {
                _context.Benefit.Delete(benefit);
            }

        


            _context.Comlete();

            return Json(new { success = true, message = "Item has been deleted" });

        }



		[HttpGet]
		public IActionResult Details(int id)
		{


			return View(_context.Benefit.GetFirstOrDefualt(i=>i.BenefitId==id,IncludeWord: "jobTitles"));
		}
    }
}
