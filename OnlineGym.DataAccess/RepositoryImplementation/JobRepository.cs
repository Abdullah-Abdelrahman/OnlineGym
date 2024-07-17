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
    public class JobRepository :GenericRepository<JobTitle>, IJobRepository
    {

        private readonly OnlineGymContext _context;
        public JobRepository(OnlineGymContext context) : base(context)
        {

            _context = context;
        }

        public void Update(JobTitle jobTitle)
        {
            var jobInDb = _context.JobTitles.FirstOrDefault(x => x.JobTitleId == jobTitle.JobTitleId);

            if (jobInDb != null) {
                jobInDb.JopName = jobTitle.JopName;
                jobInDb.JopDiscription=jobTitle.JopDiscription;
                jobInDb.MinSalary=jobTitle.MinSalary;
                jobInDb.MaxSalary=jobTitle.MaxSalary;

                jobInDb.JobTitleId=jobTitle.JobTitleId; 
            
            }
        }
    }
}
