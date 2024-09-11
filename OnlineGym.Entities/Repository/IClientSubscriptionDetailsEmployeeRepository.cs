using OnlineGym.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OnlineGym.Entities.Repository
{
    public interface IClientSubscriptionDetailsEmployeeRepository : IGenericRepository<ClientSubscriptionDetailsEmployee>
    {

        void Update(ClientSubscriptionDetailsEmployee employee);

		
	}
}
