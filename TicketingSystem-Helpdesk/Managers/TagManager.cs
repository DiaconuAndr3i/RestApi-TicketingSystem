using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketingSystem_Helpdesk.Entities;
using TicketingSystem_Helpdesk.Repositories;

namespace TicketingSystem_Helpdesk.Managers
{
    public class TagManager : ITagManager
    {
        private readonly ITagRepository tagRepository;

        public TagManager(ITagRepository tagRepository)
        {
            this.tagRepository = tagRepository;
        }

        public async Task AddTagTicket(string idTicket, string idTag)
        {
            var tagTicket = new TagTicket
            {
                TagId = idTag,
                TicketId = idTicket
            };

            await tagRepository.AddTagTicket(tagTicket);
        }

        public async Task<List<Tag>> GetAllTags()
        {
            var tags = await tagRepository.GetAllTags().ToListAsync();

            return tags;
        }

        public async Task<string> GetIdTagByName(string tagName)
        {
            var tags = tagRepository.GetAllTags();
            var tagId = await tags
                .Where(x => x.TagType.Equals(tagName))
                .Select(x => x.Id)
                .FirstOrDefaultAsync();

            return tagId;
        }

        public async Task<string> GetNameTagById(string tagId)
        {
            var tagName = await tagRepository.GetAllTags()
                .Where(x => x.Id.Equals(tagId))
                .Select(x => x.TagType)
                .FirstOrDefaultAsync();

            return tagName;
        }

        public async Task<List<string>> GetTagsNameTicket(string ticketId)
        {
            var tagTickets = await tagRepository.GetTicketIdTagName()
                .Where(x => x.Ticket.Equals(ticketId))
                .Select(x => x.Tag)
                .ToListAsync();

            return tagTickets;
        }
    }
}
