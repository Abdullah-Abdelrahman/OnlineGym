using OnlineGym.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineGym.Entities.ViewModels
{
	public class OrderViewModel
	{


		public ClientSubscription ClientSubscription { get; set; }

		public bool hasPlan { get; set; }
	}
}
