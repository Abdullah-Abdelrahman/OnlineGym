using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineGym.Entities.Models;
using OnlineGym.Entities.Repository;
using OnlineGym.Entities.ViewModels;
using OnlineGym.Utilities;
using System;

namespace OnlineGym.Web.Areas.Coach.Controllers
{
    [Area("Coach")]
    [Authorize(Roles = SD.CoachRole)]
    public class TrainingPlanController : Controller
    {

        private readonly IUnitOfWork _context;

        public TrainingPlanController(IUnitOfWork context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<TrainingPlan> Plans = _context.trainingPlan.GetAll(IncludeWord: "Days").ToList();

            for(int i = 0; i < Plans.Count; i++)
            {
                for(int j = 0; j < Plans[i].Days.Count; j++)
                {
                    Plans[i].Days[j] = _context.Day.GetFirstOrDefualt(d=>d.DayId == Plans[i].Days[j].DayId,IncludeWord: "exercises");

				}
            }
            
            return View(Plans);
        }


        [HttpGet]
        public IActionResult CreateForClient(int OrderId)
        {
            
            PlanViewModel planViewModel = new PlanViewModel();

            planViewModel.AssignClientDetails(_context.ClientSubscriptionDetails.GetFirstOrDefualt(s => s.ClientSubscriptionId == OrderId));

            planViewModel.days = new List<Day>();
            planViewModel.TrainingPlan = new TrainingPlan();
            planViewModel.Exercises= _context.Exercise.GetAll().Select(e => new SelectListItem { Text = e.Name, Value = e.Id.ToString() }).ToList();
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
        public IActionResult CreateForClient(PlanViewModel planViewModel)
        {

            TrainingPlan plan = new TrainingPlan();

            plan.ClientId = _context.ClientSubscription.GetFirstOrDefualt(s => s.ClientSubscriptionId == planViewModel.OrderId).ClientId;

            plan.Days = new List<Day>();    
                for(int i = 0; i < 28; i++)
                {
                    Day day = new Day();

                    day.exercises=new List<Exercise>();
				
				    for (int j = 1; j < planViewModel?.DayExercisesIds?[i].Count; j++)
					{
                   

					    if (planViewModel.DayExercisesIds[i][j] != 0)
						{

                            day.exercises.Add(_context.Exercise.GetFirstOrDefualt(e => e.Id == planViewModel.DayExercisesIds[i][j]));
						}
					}



					plan.Days.Add(day);
                }

            _context.ClientSubscription.GetFirstOrDefualt(s => s.ClientSubscriptionId == planViewModel.OrderId).Status = SD.Working;

			
            plan.Started=DateTime.Now.AddDays(1);
			_context.trainingPlan.Add(plan);


                _context.Comlete();
                return RedirectToAction("Index");
           


		}

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            _context.trainingPlan.Delete(_context.trainingPlan.GetFirstOrDefualt(t => t.Id == id));
            _context.Comlete();
            return Json(new { success = true, message = "Item has been deleted" });
        }
    }
}


