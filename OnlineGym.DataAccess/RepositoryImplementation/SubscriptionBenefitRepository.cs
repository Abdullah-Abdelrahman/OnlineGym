using OnlineGym.DataAccess.Data;
using OnlineGym.Entities.Models;
using OnlineGym.Entities.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineGym.DataAccess.RepositoryImplementation
{
	public class SubscriptionBenefitRepository:GenericRepository<SubscriptionBenefit>,ISubscriptionBenefitRepository
	{
		private readonly OnlineGymContext _context;
		public SubscriptionBenefitRepository(OnlineGymContext context) : base(context)
		{

			_context = context;

		}

		public void Update(SubscriptionBenefit subscriptionBenefit)
		{
			throw new NotImplementedException();
		}
	}
}
