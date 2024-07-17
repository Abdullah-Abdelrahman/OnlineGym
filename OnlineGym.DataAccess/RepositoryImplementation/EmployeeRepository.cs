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
    }
}
