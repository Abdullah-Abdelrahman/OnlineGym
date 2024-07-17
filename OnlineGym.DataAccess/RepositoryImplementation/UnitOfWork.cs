using OnlineGym.DataAccess.Data;
using OnlineGym.Entities.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineGym.DataAccess.RepositoryImplementation
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly OnlineGymContext _context;
        public IJobRepository Jobs { get; private set; }

        public IEmployeeRepository Employee { get; private set; }

        public ISalaryRepository Salary { get; private set; }

        public IClientRepository Client { get; private set; }

        public ISubscriptionRepository Subscription { get; private set; }
        public IBenefitRepository Benefit { get; private set; }

        public ISubscriptionBenefitRepository SubscriptionBenefit { get; private set; }  


        public IClientSubscriptionRepository ClientSubscription { get; private set; }

        public IClientSubscriptionDetailsRepository ClientSubscriptionDetails { get; private set; }

        public IBenefitJobTitleRepository BenefitJobTitle { get; private set; }

        public IVideoRepository Video { get; private set; }

        public IEmployeeRateRepository EmployeeRate { get; private set; }
        public UnitOfWork(OnlineGymContext context) 
        {

            _context = context;
            Jobs=new JobRepository(context);
            Employee=new EmployeeRepository(context);
            Salary=new SalaryRepository(context);   
            Client=new ClientRepository(context);
            Subscription=new SubscriptionRepository(context);
            Benefit=new BenefitRepository(context);
			SubscriptionBenefit=new SubscriptionBenefitRepository(context);
            ClientSubscription=new ClientSubscriptionRepository(context);
            
            ClientSubscriptionDetails=new ClientSubscriptionDetailsRepository(context);

            BenefitJobTitle=new BenefitJobTitleRepository(context);

            Video=new VideoRepository(context);

            EmployeeRate=new EmployeeRateRepository(context);

		}



        public int Comlete()
        {
           return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
