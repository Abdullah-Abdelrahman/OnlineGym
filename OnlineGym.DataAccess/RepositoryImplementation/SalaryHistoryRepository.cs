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
    public class SalaryHistoryRepository:GenericRepository<SalaryHistory>,ISalaryHistoryRepository
    {
        private readonly OnlineGymContext _context;
        public SalaryHistoryRepository(OnlineGymContext context) : base(context)
        {
            _context = context;

        }
    }
}
