using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketingSystem_Helpdesk.Entities;

namespace TicketingSystem_Helpdesk.Repositories
{
    public class StatusRepository : IStatusRepository
    {
        private readonly Context context;

        public StatusRepository(Context context)
        {
            this.context = context;
        }

        public IQueryable<Status> GetStatuses()
        {
            var statuses = context.Statuses;

            return statuses;
        }
    }
}
