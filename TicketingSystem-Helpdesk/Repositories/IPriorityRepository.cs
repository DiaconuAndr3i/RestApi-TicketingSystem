using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketingSystem_Helpdesk.Entities;

namespace TicketingSystem_Helpdesk.Repositories
{
    public interface IPriorityRepository
    {
        IQueryable<Priority> GetPriorities();
    }
}
