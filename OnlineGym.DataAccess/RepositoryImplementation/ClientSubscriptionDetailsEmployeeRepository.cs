using OnlineGym.DataAccess.Data;
using OnlineGym.Entities.Models;
using OnlineGym.Entities.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OnlineGym.DataAccess.RepositoryImplementation
{
    public class ClientSubscriptionDetailsEmployeeRepository: GenericRepository<ClientSubscriptionDetailsEmployee>,IClientSubscriptionDetailsEmployeeRepository
    {

        private readonly OnlineGymContext _context;
        public ClientSubscriptionDetailsEmployeeRepository(OnlineGymContext context) : base(context)
        {

            _context = context;

        }

	
		
		public void Update(ClientSubscriptionDetailsEmployee employee)
        {
            throw new NotImplementedException();
        }
    }
}
