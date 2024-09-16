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
		public async Task<IActionResult> Index()
		{
			return View(await _context.Benefit.GetAllAsync());
		}
		[HttpGet]
		public async Task<IActionResult> Create()
		{
			BenefitViewModel benefitViewModel = new BenefitViewModel();

			benefitViewModel.Benefit=new Benefit();
		
			benefitViewModel.Jobs = (await _context.Jobs.GetAllAsync()).Select(i => new SelectListItem { Text = i.JopName, Value = i.JobTitleId.ToString() });

		
			return View(benefitViewModel);
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(BenefitViewModel benefitVM)
		{
			if (ModelState.IsValid&&benefitVM.JobsId!=null)
			{
				await _context.Benefit.AddAsync(benefitVM.Benefit);
				_context.Comlete();



				int BenId = _context.Benefit.last().BenefitId;

				
				// iterate on all of the selected jobs id and add records
				// for the benifit and the jobs needed
				for (int i = 0; i < benefitVM?.JobsId?.Count; i++)
				{

					if (benefitVM.JobsId[i] != 0)
					{
						await _context.BenefitJobTitle.AddAsync(new BenefitJobTitle { BenefitId =BenId , JobTitleId= benefitVM.JobsId[i] });
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
		public async Task<IActionResult> Update(int id)
		{

			BenefitViewModel benefitViewModel = new BenefitViewModel();

			benefitViewModel.Benefit = await _context.Benefit.GetFirstOrDefualtAsync(b=>b.BenefitId==id);
			benefitViewModel.JobsId = (await _context.BenefitJobTitle.GetAllAsync(b=>b.BenefitId==id)).Select(i=>i.JobTitleId).ToList();
			benefitViewModel.Jobs = (await _context.Jobs.GetAllAsync()).Select(i => new SelectListItem { Text = i.JopName, Value = i.JobTitleId.ToString() });


			return View(benefitViewModel);
		}

        [HttpPost]
		[ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(BenefitViewModel benefitViewModel)
        {

			// get the record from the database
			Benefit benefit = await _context.Benefit.GetFirstOrDefualtAsync(e => e.BenefitId == benefitViewModel.Benefit.BenefitId);

            // get the records from the database
            List<BenefitJobTitle> benefitJobTitles =(await _context.BenefitJobTitle.GetAllAsync(b => b.BenefitId == benefitViewModel.Benefit.BenefitId)).ToList();

			// remove deleted ones
			for (int i = 0; i < benefitJobTitles.Count; i++)
			{
                // iterate over all off the benefitJobTitles in the database
                // if there is no job Id  in the view model
				// that is equal to a one in the database remove the record frome the database
                if (!benefitViewModel.JobsId.Any(b => b == benefitJobTitles[i].JobTitleId))
				{
					benefitJobTitles.Remove(benefitJobTitles[i]);
				}

			}
            // add the new ones
            for (int i = 0; i < benefitViewModel.JobsId.Count; i++)
			{
                // iterate over all off the benefitJobTitles in the database
                // if there is an job Id  in the view model
                // that is not equal to a one in the database add the record to the database
                if (!benefitJobTitles.Any(b=>b.JobTitleId== benefitViewModel.JobsId[i]))
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
        public async Task<IActionResult> Delete(int id)
        {
            Benefit benefit =await _context.Benefit.GetFirstOrDefualtAsync(e => e.BenefitId == id);
			//may be not in the database
            if (benefit != null)
            {
                _context.Benefit.Delete(benefit);
            }
            _context.Comlete();

            return Json(new { success = true, message = "Item has been deleted" });

        }



		[HttpGet]
		public async Task<IActionResult> Details(int id)
		{


			return View(await _context.Benefit.GetFirstOrDefualtAsync(i=>i.BenefitId==id,IncludeWord: "jobTitles"));
		}
    }
}
