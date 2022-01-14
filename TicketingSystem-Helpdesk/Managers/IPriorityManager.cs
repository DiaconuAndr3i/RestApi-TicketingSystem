using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketingSystem_Helpdesk.Entities;

namespace TicketingSystem_Helpdesk.Managers
{
    public interface IPriorityManager
    {
        Task<List<Priority>> GetAllPriorities();
        Task<List<string>> GetAllPrioritiesName();
        Task<string> GetPriorityIdByName(string priorityName);
        Task<string> GetPriorityNameById(string priorityId);
    }
}
