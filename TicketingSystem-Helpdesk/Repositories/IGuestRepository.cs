using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketingSystem_Helpdesk.Entities;

namespace TicketingSystem_Helpdesk.Repositories
{
    public interface IGuestRepository
    {
        Task AddGuestRolesAsync(GuestRoles guestRoles);
        Task AddGuestAsync(Guest guest);
        Task DeleteGuest(Guest guest);
        IQueryable<string> GetGuestRoles(string idUser);
        IQueryable<Guest> GetAllGuests();
    }
}
