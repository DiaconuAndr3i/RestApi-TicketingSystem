using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketingSystem_Helpdesk.Entities;
using TicketingSystem_Helpdesk.Models;

namespace TicketingSystem_Helpdesk.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly Context context;

        public TagRepository(Context context)
        {
            this.context = context;
        }
        public async Task AddTagTicket(TagTicket tagTicket)
        {
            await context.TagTickets.AddAsync(tagTicket);

            await context.SaveChangesAsync();
        }

        public IQueryable<Tag> GetAllTags()
        {
            var tags = context.Tags;

            return tags;
        }

        public IQueryable<TagTicketModel> GetTicketIdTagName()
        {
            var tagTickets = context.TagTickets
                .Include(x => x.Tag)
                .Select(x => new TagTicketModel
                {
                    Ticket = x.TicketId,
                    Tag = x.Tag.TagType
                });

            return tagTickets;
        }
    }
}
