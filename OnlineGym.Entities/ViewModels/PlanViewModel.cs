using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineGym.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineGym.Entities.ViewModels
{
    public class PlanViewModel
    {

        public List<OnlineGym.Entities.Models.Day> days {  get; set; }

        public TrainingPlan TrainingPlan { get; set; }

		// All selected Exercises in each day
		public List<List<int>>? DayExercisesIds { get; set; }


		// All Exercises in the system
		public List<SelectListItem>? Exercises { get; set; }




        public bool? Gender { get; set; }

        public int? Hight { get; set; }

        public float? Weight { get; set; }
        public float? BodyFat { get; set; }

        public int? Age { get; set; }

        public string? Target { get; set; }

        public string? Diseases { get; set; }

        public int OrderId { get; set; }

        public void AssignClientDetails(ClientSubscriptionDetails subscriptionDetails)
        {
                    
            this.Gender  = subscriptionDetails.Gender;
                                                    
            this.Hight   =subscriptionDetails.Hight;
                                                        
            this.Weight  =subscriptionDetails.Weight;
            this.BodyFat =subscriptionDetails.BodyFat;
                                                     
            this.Age     =subscriptionDetails.Age;
                                                      
            this.Target  =subscriptionDetails.Target;
                                                     
            this.Diseases = subscriptionDetails.Diseases;

            this.OrderId = subscriptionDetails.ClientSubscriptionId;

        }


    }
}
