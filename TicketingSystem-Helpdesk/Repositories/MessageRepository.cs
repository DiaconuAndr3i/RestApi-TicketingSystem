using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketingSystem_Helpdesk.Entities;

namespace TicketingSystem_Helpdesk.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly Context context;

        public MessageRepository(Context context)
        {
            this.context = context;
        }

        public async Task AddMessage(Message message)
        {
            await context.Messages.AddAsync(message);

            await SaveChanges();
        }

        public IQueryable<Message> GetAllMessages()
        {
            var messages = context.Messages;

            return messages;
        }

        public async Task DeleteMessage(Message message)
        {
            context.Messages.Remove(message);

            await SaveChanges();
        }

        public async Task SaveChanges()
        {
            await context.SaveChangesAsync();
        }
    }
}
