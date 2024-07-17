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
	public class ClientSubscriptionDetailsRepository : GenericRepository<ClientSubscriptionDetails>, IClientSubscriptionDetailsRepository
	{

		private readonly OnlineGymContext _context;
		public ClientSubscriptionDetailsRepository(OnlineGymContext context) : base(context)
		{

			_context = context;

		}
		public void Update(ClientSubscriptionDetails clientSubscriptionDetails)
		{
			var CSInDb = _context.ClientSubscriptionDetails.FirstOrDefault(e => e.ClientSubscriptionId == clientSubscriptionDetails.ClientSubscriptionId);

			if (CSInDb != null)
			{

				CSInDb.paymentDate = clientSubscriptionDetails.paymentDate;

				

			}
		}
	}
}
