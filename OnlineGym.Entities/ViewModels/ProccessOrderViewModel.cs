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
	public class ProccessOrderViewModel
	{

		public int orderId { get; set; }

		[Required]
		public List<int>? EmpIds { get; set; }

		public List<List<SelectListItem>>? Emps { get; set; }

		public List<JobTitle>? JobsNeed { get; set; }

		public ProccessOrderViewModel()
		{
            this.JobsNeed = new List<JobTitle>();
			this.Emps = new List<List<SelectListItem>>();
        }

        public bool valid()
		{

			int c = 0;

			if(this?.EmpIds != null)
			{
				foreach (var i in this.EmpIds)
				{
					if (i != 0)
					{
						c++;
					}
				}
			}

			if (this?.JobsNeed != null)
			{
				if (c == this?.JobsNeed?.Count)
				{
					return true;
				}
			}
		
			return false;
		}
	}
}
