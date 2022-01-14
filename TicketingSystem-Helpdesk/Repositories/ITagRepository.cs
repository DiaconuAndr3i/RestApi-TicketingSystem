using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketingSystem_Helpdesk.Entities;
using TicketingSystem_Helpdesk.Models;

namespace TicketingSystem_Helpdesk.Repositories
{
    public interface ITagRepository
    {
        Task AddTagTicket(TagTicket tagTicket);
        IQueryable<Tag> GetAllTags();
        IQueryable<TagTicketModel> GetTicketIdTagName();
    }
}
