using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketingSystem_Helpdesk.Entities;

namespace TicketingSystem_Helpdesk.Managers
{
    public interface IStatusManager
    {
        Task<List<Status>> GetAllStatus();
        Task<List<string>> GetAllStatusName();
        Task<string> GetStatusIdByName(string statusName);
        Task<string> GetStatusNameById(string statusId);
    }
}
