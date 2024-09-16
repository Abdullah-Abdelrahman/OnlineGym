using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineGym.Entities.Repository
{
    public interface IUnitOfWork:IDisposable
    {

        IJobRepository Jobs{ get; }
        IEmployeeRepository Employee{ get; }

		ISalaryRepository Salary { get; }

        IClientRepository Client { get; }

        ISubscriptionRepository Subscription { get; }

        IBenefitRepository Benefit { get; }

        ISubscriptionBenefitRepository SubscriptionBenefit { get; }

        IClientSubscriptionRepository ClientSubscription { get; }

        IClientSubscriptionDetailsRepository ClientSubscriptionDetails { get; }
        IBenefitJobTitleRepository BenefitJobTitle { get; }

        IVideoRepository Video { get; }

        IEmployeeRateRepository EmployeeRate { get; }

        IExerciseRepository Exercise { get; }

        IDayRepository Day { get; }

        ITrainingPlanRepository trainingPlan { get; }

        IClientSubscriptionDetailsEmployeeRepository ClientSubscriptionDetailsEmployee { get; }

        IDayExercisRepository DayExercis { get; }

        ISalaryHistoryRepository SalaryHistory { get; }
		int Comlete();
    }
}
