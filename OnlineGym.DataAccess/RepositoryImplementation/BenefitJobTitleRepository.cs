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
	public class BenefitJobTitleRepository : GenericRepository<BenefitJobTitle>, IBenefitJobTitleRepository
	{

		private readonly OnlineGymContext _context;
		public BenefitJobTitleRepository(OnlineGymContext context) : base(context)
		{

			_context = context;

		}
		public void Update(BenefitJobTitle entity)
		{
			throw new NotImplementedException();
		}
	}
}
