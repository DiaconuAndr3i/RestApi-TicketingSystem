using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketingSystem_Helpdesk.Entities;

namespace TicketingSystem_Helpdesk.Repositories
{
    public class GuestRepository : IGuestRepository
    {
        private readonly Context context;
        
        public GuestRepository(Context context)
        {
            this.context = context;
        }

        public async Task AddGuestRolesAsync(GuestRoles guestRoles)
        {
            await context.GuestRoles.AddAsync(guestRoles);

            await context.SaveChangesAsync();
        }

        public async Task AddGuestAsync(Guest guest)
        {
            await context.Guests.AddAsync(guest);

            await context.SaveChangesAsync();
        }

        public async Task DeleteGuest(Guest guest)
        {
            context.Guests.Remove(guest);

            await context.SaveChangesAsync();
        }

        public IQueryable<string> GetGuestRoles(string idUser)
        {
            var roles = context.GuestRoles
                .Include(x => x.Guest)
                .Where(x => x.Guest.UserId.Equals(idUser))
                .Select(x => x.Name);

            return roles;
        }

        public IQueryable<Guest> GetAllGuests()
        {
            var guests = context.Guests;

            return guests;
        }
    }
}
