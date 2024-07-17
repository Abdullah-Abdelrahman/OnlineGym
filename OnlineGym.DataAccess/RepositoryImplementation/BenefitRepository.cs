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
	public class BenefitRepository : GenericRepository<Benefit>, IBenefitRepository
	{

		private readonly OnlineGymContext _context;
		public BenefitRepository(OnlineGymContext context) : base(context)
		{

			_context = context;

		}
		public void Update(Benefit benefit)
		{
            var BenefitInDb = _context.Benefits.FirstOrDefault(c => c.BenefitId == benefit.BenefitId);

            if (BenefitInDb != null)
            {

                BenefitInDb.description = benefit.description;


            }
        }
	}
}
