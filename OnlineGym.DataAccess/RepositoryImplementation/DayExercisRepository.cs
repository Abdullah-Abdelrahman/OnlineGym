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
    public class DayExercisRepository : GenericRepository<DayExercise>, IDayExercisRepository
    {
        private readonly OnlineGymContext _context;
        public DayExercisRepository(OnlineGymContext context) : base(context)
        {

            _context = context;

        }
        public void Update(DayExercise dayExercise)
        {
            throw new NotImplementedException();
        }
    }
}
