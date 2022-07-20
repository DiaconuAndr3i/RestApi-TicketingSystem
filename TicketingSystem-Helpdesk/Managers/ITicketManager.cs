using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TicketingSystem_Helpdesk.Entities;
using TicketingSystem_Helpdesk.Models;

namespace TicketingSystem_Helpdesk.Managers
{
    public interface ITicketManager
    {
        Task<bool> CreateTicket(CreateTicketModel createTicketModel);
        Task<Ticket> CreateObjectTicket(CreateTicketModel createTicketModel);
        Task<List<ReceiveTicketModel>> GetAllTicketsByCreatorArrivalEmail(string email, bool direction);
        Task<List<ReceiveTicketModel>> GetAllTicketsByStatus(string status, string email, bool direction);
        Task<List<ReceiveTicketModel>> GetAllTicketsByPriority(string priority, string email, bool direction);
        Task<Ticket> GetTicketById(string idTicket);
        Task<bool> DeleteTicket(string idTicket);
        Task<bool> ChangeStatusTicket(string idTicket, string status);
        Task<bool> ChangeNewActivityTicket(string idTicket, bool activity);
        Task<Boolean> ProduceMessageForKafkaBroker(object obj, string topic);
    }
}
