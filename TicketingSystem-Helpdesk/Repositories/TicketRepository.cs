using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketingSystem_Helpdesk.Entities;

namespace TicketingSystem_Helpdesk.Repositories
{
    public class TicketRepository : ITicketRepository
    {
        private readonly Context context;

        public TicketRepository(Context context)
        {
            this.context = context;
        }

        public async Task AddTicket(Ticket ticket)
        {
            await context.Tickets.AddAsync(ticket);

            await context.SaveChangesAsync();
        }

        public IQueryable<Ticket> GetAllTickets()
        {
            var tickets = context.Tickets;

            return tickets;
        }

        public async Task DeleteTicket(Ticket ticket)
        {
            context.Tickets.Remove(ticket);

            await context.SaveChangesAsync();
        }

        public async Task SaveChanges()
        {
            await context.SaveChangesAsync();
        }
    }
}
