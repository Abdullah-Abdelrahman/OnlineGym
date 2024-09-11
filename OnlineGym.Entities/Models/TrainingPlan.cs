using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineGym.Entities.Models
{
    public class TrainingPlan
    {
        public int Id { get; set; }

        public string ClientId { get; set; }

        public DateTime? Started { get; set; }

        [ForeignKey("ClientId")]
        public virtual Client? Client { get; set; }
        public List<OnlineGym.Entities.Models.Day> Days { get; set; }

    }
}
