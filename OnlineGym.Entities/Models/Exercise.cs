using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineGym.Entities.Models
{
    public class Exercise
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string VideoUrl {  get; set; }


        public ICollection<OnlineGym.Entities.Models.Day> days { get; set; }
        public List<DayExercise> dayExercises { get; set; }
    }
}
