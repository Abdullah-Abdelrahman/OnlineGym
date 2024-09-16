using OnlineGym.Entities.Models;
using OnlineGym.Entities.Repository;
using OnlineGym.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineGym.Web.Services
{
	public class ServiceManagment : IServiceManagment
	{
		private readonly IUnitOfWork _context;

		public ServiceManagment(IUnitOfWork context)
		{
			_context = context;
		}
		public  void UpdateOrdersStatus()
		{

			List<ClientSubscriptionDetails> Orders =  _context.ClientSubscriptionDetails.GetAll().ToList();

			foreach (var item in Orders)
			{
				if(item.StartDate <= DateTime.Now &&item.EndDate > DateTime.Now)
				{
					 _context.ClientSubscription.GetFirstOrDefualt(c=>c.ClientSubscriptionId==item.ClientSubscriptionId).Status=SD.Working;
					_context.Comlete();
				}
				else if (item.EndDate <= DateTime.Now)
				{
                    _context.ClientSubscription.GetFirstOrDefualt(c => c.ClientSubscriptionId == item.ClientSubscriptionId).Status = SD.Finished;
                    _context.Comlete();
                }
			}

			Console.WriteLine("Done : UpdateOrderStatus");
		}

        public void UpdateSalaryStatus()
        {
            List<Salary> salaries = _context.Salary.GetAll().ToList();

            foreach (var item in salaries)
            {
                if (item.nextSalaryDate <= DateTime.Now && item.Status ==SD.Pending)
                {
                    _context.Salary.GetFirstOrDefualt(c => c.EmployeeId == item.EmployeeId).Status = SD.NeedToBePaid;

                    _context.Comlete();
                }
            }

            Console.WriteLine("Done : Update Salary Status");
        }
    }
}
