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
	public class ClientSubscriptionDetailsEmployee
	{
		
		public int ClientSubscriptionId { get; set; }
		[ForeignKey("ClientSubscriptionId")]
		[JsonIgnore]
		public ClientSubscriptionDetails ClientSubscriptionDetails { get; set; }


		public int EmployeeId { get; set; }

		[ForeignKey("EmployeeId")]
		public Employee Employee { get; set; }


	}
}
