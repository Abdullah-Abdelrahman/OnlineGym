using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineGym.Entities.Models
{
	public class SubscriptionBenefit
	{
		public int SubscriptionId { get; set; }
		public Subscription Subscription { get; set; }


		public int BenefitId { get; set; }

		public Benefit Benefit { get; set; }

	}
}
