using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OnlineGym.Entities.Models
{
    public class Day
    {
        public int DayId { get; set; }

        public int TrainingPlanId {  get; set; }

        public bool isDone { get; set; }
        [JsonIgnore]
        [ForeignKey("TrainingPlanId")]
        public TrainingPlan TrainingPlan { get; set; }




        public List<Exercise>exercises {  get; set; } 
        public List<DayExercise> dayExercises { get; set; }
    }
}
