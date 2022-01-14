using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketingSystem_Helpdesk.Entities;
using TicketingSystem_Helpdesk.Models;
using TicketingSystem_Helpdesk.Repositories;

namespace TicketingSystem_Helpdesk.Managers
{
    public class GuestManager : IGuestManager
    {
        private readonly IGuestRepository guestRepository;
        private readonly IServicesManager servicesManager;
        
        public GuestManager(IGuestRepository guestRepository,
            IServicesManager servicesManager)
        {
            this.guestRepository = guestRepository;
            this.servicesManager = servicesManager;
        }

        public async Task RegisterGuest(User user, string role)
        {
            var guestId = servicesManager.CreateId();
            var guest = new Guest
            {
                Id = guestId,
                UserId = user.Id
            };

            await guestRepository.AddGuestAsync(guest);

            var guestRoles = new GuestRoles
            {
                Id = servicesManager.CreateId(),
                Name = role,
                GuestId = guestId
            };

            await guestRepository.AddGuestRolesAsync(guestRoles);
        }

        public async Task<List<string>> GetGuestRoles(string idUser)
        {
            List<string> list = await guestRepository.GetGuestRoles(idUser).ToListAsync();

            return list;
        }

        public async Task<Guest> GetGuestByUserId(string idUser)
        {
            var guest = await guestRepository.GetAllGuests()
                .Where(x => x.UserId.Equals(idUser)).FirstOrDefaultAsync();

            return guest;
        }

        public async Task DeleteGuest(string idUser)
        {
            var guest = await GetGuestByUserId(idUser);

            await guestRepository.DeleteGuest(guest);
        }
    }
}
