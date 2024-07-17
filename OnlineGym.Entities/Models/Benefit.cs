using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineGym.Entities.Models
{
	public class Benefit
	{
        
        public int BenefitId { get; set; }	

		public string? description { get; set; }


		public ICollection<Subscription>? Subscriptions { get; set; }
		public List<SubscriptionBenefit>? SubscriptionBenefits { get; set; }


        public ICollection<JobTitle>? jobTitles { get; set; }

        public List<BenefitJobTitle>? benefitJobTitles { get; set; }
    }
}
