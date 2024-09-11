using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineGym.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineGym.Entities.ViewModels
{
	public class ExerciseViewModel
	{
		public Exercise Exercise { get; set; }

		public List<SelectListItem>? Videos { get; set; }

	}
}
