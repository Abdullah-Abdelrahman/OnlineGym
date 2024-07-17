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
	public class SubscriptionRepository : GenericRepository<Subscription>, ISubscriptionRepository
	{

		private readonly OnlineGymContext _context;
		public SubscriptionRepository(OnlineGymContext context) : base(context)
		{

			_context = context;

		}

		public void Update(Subscription subscription)
		{

			var subscriptionInDb = _context.Subscriptions.FirstOrDefault(e => e.SubscriptionId == subscription.SubscriptionId);
			throw new NotImplementedException();
		}
	}
}
