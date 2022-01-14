using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketingSystem_Helpdesk.Entities;

namespace TicketingSystem_Helpdesk.Repositories
{
    public class PriorityRepository : IPriorityRepository
    {
        private readonly Context context;

        public PriorityRepository(Context context)
        {
            this.context = context;
        }

        public IQueryable<Priority> GetPriorities()
        {
            var priorities = context.Priorities;

            return priorities;
        }
    }
}
