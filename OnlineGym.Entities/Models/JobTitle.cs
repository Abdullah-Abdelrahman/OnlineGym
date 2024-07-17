using System.Text.Json.Serialization;

namespace OnlineGym.Entities.Models
{
    public class JobTitle
    {

        public int JobTitleId { get; set; }
        public string? JopName  { get; set; }

        public string? JopDiscription { get; set; }

        public int MinSalary { get; set; }
        public int MaxSalary { get; set; }

        [JsonIgnore]
        public List<Employee>? Employees { get; set; }


		[JsonIgnore]
		public ICollection<Benefit>? benefits { get; set; }

		public List<BenefitJobTitle>? benefitJobs { get; set; }

	}
}
