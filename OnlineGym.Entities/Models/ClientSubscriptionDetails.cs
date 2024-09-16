using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OnlineGym.Entities.Models
{
	public class ClientSubscriptionDetails
	{

		[Key]
		public int ClientSubscriptionId { get; set; }

		[ForeignKey("ClientSubscriptionId")]
		[ValidateNever]
		public ClientSubscription? clientSubscription { get; set; }



		[Required]
		public string ClientPhone { get; set; }

		[Required]
		[EmailAddress]

		public string ClientEmail { get; set; }

		[ValidateNever]
		public DateTime? paymentDate { get; set; }

		[ValidateNever]

		public DateTime? StartDate { get; set; }
		[ValidateNever]

		public DateTime? EndDate { get; set; }


		
		public bool? Gender { get; set; }

		public int? Hight {  get; set; }

		public float? Weight { get; set; }
		public float? BodyFat { get; set; }

		public int? Age { get; set; }

		public string? Target { get; set; }

		public string? Diseases { get; set; }




		[ValidateNever]
		public ICollection<Employee>? ClientSelectedTeam { get; set; }
		[ValidateNever]
		[JsonIgnore]
		public List<ClientSubscriptionDetailsEmployee>? ClientSelectedEmployees { get; set; }


	}
}
