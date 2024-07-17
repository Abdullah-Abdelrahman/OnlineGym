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
	public class SalaryRepository : GenericRepository<Salary>, ISalaryRepository
	{

		private readonly OnlineGymContext _context;
		public SalaryRepository(OnlineGymContext context) : base(context)
		{
			_context = context;

		}

		public void Update(Salary salary)
		{
			var salaryInDb = _context.Salaries.FirstOrDefault(x => x.EmployeeId == salary.EmployeeId);

			if (salaryInDb != null)
			{
				salaryInDb.MounthSalary = salary.MounthSalary;
				salaryInDb.bonus = salary.bonus;
				salaryInDb.Amount = salary.Amount;
				

			}
		}
	}
}
