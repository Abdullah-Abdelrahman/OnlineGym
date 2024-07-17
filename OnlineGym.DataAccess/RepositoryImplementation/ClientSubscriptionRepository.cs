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
	public class ClientSubscriptionRepository : GenericRepository<ClientSubscription>, IClientSubscriptionRepository
	{
		private readonly OnlineGymContext _context;
		public ClientSubscriptionRepository(OnlineGymContext context) : base(context)
		{

			_context = context;

		}
		public void Uppdate(ClientSubscription subscription)
		{
			var CSInDb = _context.ClientSubscriptions.FirstOrDefault(e => e.ClientSubscriptionId == subscription.ClientSubscriptionId);

			if (CSInDb != null)
			{

				CSInDb.Status = subscription.Status;
				
				CSInDb.SessionId = subscription.SessionId;

				CSInDb.PaymentIntentId = subscription.PaymentIntentId;
				CSInDb.orderDate = subscription.orderDate;

			}
		}



		public void DeleteById(int id)
		{
			 ClientSubscription cs=_context.ClientSubscriptions.FirstOrDefault(c => c.ClientSubscriptionId == id);

			if (cs != null)
			{
				_context.ClientSubscriptions.Remove(cs);
			}
		}
	}
}
