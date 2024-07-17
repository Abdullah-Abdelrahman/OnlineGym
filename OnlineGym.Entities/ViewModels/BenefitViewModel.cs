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

		public List<int>? JobsId { get; set; }

		public IEnumerable<SelectListItem>? Jobs { get; set; }
	}
}
