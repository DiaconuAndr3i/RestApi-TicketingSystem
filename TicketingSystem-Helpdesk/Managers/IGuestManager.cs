using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketingSystem_Helpdesk.Entities;

namespace TicketingSystem_Helpdesk.Managers
{
    public interface IGuestManager
    {
        Task RegisterGuest(User user, string roles);
        Task<List<string>> GetGuestRoles(string idUser);
        Task<Guest> GetGuestByUserId(string idUser);
        Task DeleteGuest(string idUser);
    }
}
