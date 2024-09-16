using OnlineGym.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineGym.Entities.ViewModels
{
    public class DayExerciseViewModel
    {
        public DayExercise DayExercise { get; set; }

        public string? url { get; set; }

        public bool? CanUpdate { get; set; }
    }
}
