using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineGym.Entities.Models
{
	public class BenefitJobTitle
	{

		public int BenefitId { get; set; }

		public Benefit Benefit { get; set; }

		public int JobTitleId { get; set; }
		public JobTitle JobTitle { get; set; }

	}
}
