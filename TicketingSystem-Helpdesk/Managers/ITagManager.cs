using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketingSystem_Helpdesk.Entities;

namespace TicketingSystem_Helpdesk.Managers
{
    public interface ITagManager
    {
        Task<List<Tag>> GetAllTags();
        Task<string> GetIdTagByName(string tagName);
        Task<string> GetNameTagById(string tagId);
        Task AddTagTicket(string idTicket, string idTag);
        Task<List<string>> GetTagsNameTicket(string ticketId);
    }
}
