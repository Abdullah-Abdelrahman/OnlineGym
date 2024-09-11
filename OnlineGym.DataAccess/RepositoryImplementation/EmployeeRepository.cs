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
    public class EmployeeRepository :GenericRepository<Employee> ,IEmployeeRepository
    {

        private readonly OnlineGymContext _context;
        public EmployeeRepository(OnlineGymContext context) : base(context)
        {

            _context = context;

        }

        public void Update(Employee employee)
        {
            var employeeInDb = _context.Employees.FirstOrDefault(e =>e.EmployeeId  == employee.EmployeeId);

            if (employeeInDb != null) {

                employeeInDb.gender = employee.gender;
                employeeInDb.Email = employee.Email;
                employeeInDb.HireDate = employee.HireDate;
                employeeInDb.Name = employee.Name;
                employeeInDb.Phone = employee.Phone;
                            
            
            }

          
        }

		public Employee GetTheOneThatHasLessOrders(int jobId)
		{
			int minID = 1, value = 0;

            List<Employee>employees=_context.Employees.Where(e=>e.JobTitleId==jobId).ToList();

            for(int i = 0; i < employees.Count; i++)
            {

                employees[i].clientSubscriptionDetailsEmployees = _context.ClientSubscriptionDetailsEmployees
                    .Where(cs => cs.EmployeeId == employees[i].EmployeeId
                    &&_context.ClientSubscriptions.Where(c=>c.ClientSubscriptionId==cs.ClientSubscriptionId&&c.Status=="ongoing").ToList().Count==1)
                    .ToList();


                if (employees[i].clientSubscriptionDetailsEmployees.Count < value)
                {
                    value = employees[i].clientSubscriptionDetailsEmployees.Count;
                    minID = employees[i].EmployeeId;

				}
            }

			return _context.Employees.FirstOrDefault(e => e.EmployeeId == minID);

		}

	}
}
