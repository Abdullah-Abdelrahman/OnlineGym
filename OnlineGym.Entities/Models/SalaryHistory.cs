using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace OnlineGym.Entities.Models
{
	public class SalaryHistory
	{

		public int Id { get; set; }
		
		public int EmployeeId { get; set; }

		[ForeignKey("EmployeeId")]
		public Employee? Employee { get; set; }

		public int Amount { get; set; }

		public int bonus { get; set; }

		[DefaultValue(false)]
		public bool Paid { get; set; }
		public DateTime? PaidAt { get; set; }
	}
}
