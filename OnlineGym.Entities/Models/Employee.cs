using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace OnlineGym.Entities.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }

        public string Name { get; set; } = null!;

        public bool gender { get; set; }

        public string Email { get; set; } = null!;

        public string? Phone { get; set; }

        public DateTime HireDate { get; set; }

        public int JobTitleId { get; set; }
        [ForeignKey("JobTitleId")]
        public JobTitle? JobTitle { get; set; }

   

        public ICollection<ClientSubscriptionDetails>?ClientSubscriptionDetails { get; set; }

		public List<ClientSubscriptionDetailsEmployee>? clientSubscriptionDetailsEmployees { get; set; }

  

    }
}
