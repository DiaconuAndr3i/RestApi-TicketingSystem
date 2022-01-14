using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketingSystem_Helpdesk.Entities;

namespace TicketingSystem_Helpdesk.Repositories
{
    public interface ITicketRepository
    {
        Task AddTicket(Ticket ticket);
        IQueryable<Ticket> GetAllTickets();
        Task DeleteTicket(Ticket ticket);
        Task SaveChanges();
    }
}
