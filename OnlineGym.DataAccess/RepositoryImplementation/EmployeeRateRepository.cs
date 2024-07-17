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
    public class EmployeeRateRepository : GenericRepository<EmployeeRate>, IEmployeeRateRepository
    {

        private readonly OnlineGymContext _context;
        public EmployeeRateRepository(OnlineGymContext context) : base(context)
        {

            _context = context;

        }
        public void Update(EmployeeRate entity)
        {
            throw new NotImplementedException();
        }
    }
}
