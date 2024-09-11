using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineGym.Entities.Models
{
    public class DayExercise
    {
        public int ExerciseId { get; set; }
        [ForeignKey("ExerciseId")]
        public Exercise Exercise { get; set; }

        public int dayId { get; set; }
        [ForeignKey("dayId")]
        public OnlineGym.Entities.Models.Day day { get; set; }

        public bool isDone { get; set; }

    }
}
