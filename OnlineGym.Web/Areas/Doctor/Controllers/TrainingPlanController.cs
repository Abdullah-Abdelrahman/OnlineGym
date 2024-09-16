using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineGym.Entities.Models;
using OnlineGym.Entities.Repository;
using OnlineGym.Entities.ViewModels;
using OnlineGym.Utilities;
using System;

namespace OnlineGym.Web.Areas.Doctor.Controllers
{
    [Area("Doctor")]
    [Authorize(Roles = SD.DoctorRole)]
    public class TrainingPlanController : Controller
    {

        private readonly IUnitOfWork _context;

        public TrainingPlanController(IUnitOfWork context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            List<TrainingPlan> Plans = (await _context.trainingPlan.GetAllAsync(IncludeWord: "Days")).ToList();

            for(int i = 0; i < Plans.Count; i++)
            {
                for(int j = 0; j < Plans[i].Days.Count; j++)
                {
                    Plans[i].Days[j] =await _context.Day.GetFirstOrDefualtAsync(d=>d.DayId == Plans[i].Days[j].DayId,IncludeWord: "exercises");

				}
            }
            
            return View(Plans);
        }


        [HttpGet]
        public async Task<IActionResult> CreateForClient(int OrderId)
        {
            
            PlanViewModel planViewModel = new PlanViewModel();

            planViewModel.AssignClientDetails(await _context.ClientSubscriptionDetails.GetFirstOrDefualtAsync(s => s.ClientSubscriptionId == OrderId));
           
            planViewModel.days = new List<Day>();
            planViewModel.TrainingPlan = new TrainingPlan();
            planViewModel.Exercises= (await _context.Exercise.GetAllAsync()).Select(e => new SelectListItem { Text = e.Name, Value = e.Id.ToString() }).ToList();
            planViewModel.DayExercisesIds=new List<List<int>>();
			for (int i = 0; i < 28; i++)
            {
                planViewModel.days.Add(new Day());

                planViewModel.DayExercisesIds.Add(new List<int>());
                
            }



            return View("Create", planViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateForClient(PlanViewModel planViewModel)
        {

            TrainingPlan plan = new TrainingPlan();

            plan.ClientId =(await _context.ClientSubscription.GetFirstOrDefualtAsync(s => s.ClientSubscriptionId == planViewModel.OrderId)).ClientId;
            plan.ClientSubscriptionId = planViewModel.OrderId;
            plan.Days = new List<Day>();    
                for(int i = 0; i < 28; i++)
                {
                    Day day = new Day();

                    day.exercises=new List<Exercise>();
				
				    for (int j = 1; j < planViewModel?.DayExercisesIds?[i].Count; j++)
					{
                   

					    if (planViewModel.DayExercisesIds[i][j] != 0)
						{

                            day.exercises.Add(await _context.Exercise.GetFirstOrDefualtAsync(e => e.Id == planViewModel.DayExercisesIds[i][j]));
						}
					}



					plan.Days.Add(day);
                }





            //(await _context.ClientSubscription.GetFirstOrDefualtAsync(s => s.ClientSubscriptionId == planViewModel.OrderId)).Status = SD.Working;

	
            plan.Started = (await _context.ClientSubscriptionDetails.GetFirstOrDefualtAsync(s => s.ClientSubscriptionId == planViewModel.OrderId)).StartDate;

            _context.trainingPlan.Add(plan);


                _context.Comlete();
                return RedirectToAction("Index");
           


		}

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            _context.trainingPlan.Delete(await _context.trainingPlan.GetFirstOrDefualtAsync(t => t.Id == id));
            _context.Comlete();
            return Json(new { success = true, message = "Item has been deleted" });
        }
    }
}


