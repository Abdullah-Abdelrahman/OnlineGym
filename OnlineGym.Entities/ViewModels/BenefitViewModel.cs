using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineGym.Entities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineGym.Entities.ViewModels
{
	public class BenefitViewModel
	{
		
		public Benefit Benefit { get; set; }

		// the selected jobs Ids need for doing an benefit

		[Required(ErrorMessage ="you must choose some jobs that can do the task")]
		public List<int>? JobsId { get; set; }

		//List of all the jobs you have in the system to choose from
		public IEnumerable<SelectListItem>? Jobs { get; set; }

		public BenefitViewModel()
		{
			JobsId = new List<int>();
		}
	}
}
