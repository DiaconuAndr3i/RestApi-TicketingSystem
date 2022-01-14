using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketingSystem_Helpdesk.Entities;

namespace TicketingSystem_Helpdesk.Repositories
{
    public interface IMessageRepository
    {
        Task AddMessage(Message message);
        IQueryable<Message> GetAllMessages();
        Task DeleteMessage(Message message);
        Task SaveChanges();
    }
}
