using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketingSystem_Helpdesk.Entities;
using TicketingSystem_Helpdesk.Models;

namespace TicketingSystem_Helpdesk.Managers
{
    public interface IMessageManager
    {
        Task AddMessage(MessageModel messageModel);
        MessageModel CreateObjectMessageModel(CreateTicketModel createTicketModel, string idTicket);
        Task<List<ReceiveMessageModel>> GetMessagesFromTicket(string idTicket);
        Task<Message> GetMessageById(string idMessage);
        Task<bool> DeleteMessage(string idMessage);
        Task<bool> UpdateMessage(string idMessage, MessageModel messageModel);
    }
}
