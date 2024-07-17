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
    public class ClientRepository : GenericRepository<Client>, IClientRepository
    {
        private readonly OnlineGymContext _context;
        public ClientRepository(OnlineGymContext context) : base(context)
        {

            _context = context;

        }
        public void update(Client client)
        {
            var clientInDb = _context.Clients.FirstOrDefault(c => c.Id == client.Id);

            if (clientInDb != null)
            {

                clientInDb.LockoutEnd=client.LockoutEnd;


            }

        }
    }
}
