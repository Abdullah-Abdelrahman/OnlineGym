using OnlineGym.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineGym.Entities.Repository
{
	public interface ISubscriptionBenefitRepository:IGenericRepository<SubscriptionBenefit>
	{
		void Update(SubscriptionBenefit subscriptionBenefit);
	}
}
