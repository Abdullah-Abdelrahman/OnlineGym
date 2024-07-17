using OnlineGym.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineGym.Entities.ViewModels
{
	public class EmployeeDetailsViewModel
	{
		public EmployeeRate EmployeeRate { get; set; }
		public Employee Employee { get; set; }

		public Salary Salary { get; set; }
	}
}
