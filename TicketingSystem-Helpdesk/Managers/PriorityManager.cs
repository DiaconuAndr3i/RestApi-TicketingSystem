using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketingSystem_Helpdesk.Entities;
using TicketingSystem_Helpdesk.Repositories;

namespace TicketingSystem_Helpdesk.Managers
{
    public class PriorityManager : IPriorityManager
    {
        private readonly IPriorityRepository priorityRepository;

        public PriorityManager(IPriorityRepository priorityRepository)
        {
            this.priorityRepository = priorityRepository;
        }

        public async Task<List<Priority>> GetAllPriorities()
        {
            var priorities = await priorityRepository.GetPriorities().ToListAsync();

            return priorities;
        }

        public async Task<List<string>> GetAllPrioritiesName()
        {
            var list = new List<string>();

            foreach(var item in await GetAllPriorities())
            {
                list.Add(item.Name);
            }

            return list;
        }

        public async Task<string> GetPriorityIdByName(string priorityName)
        {
            var priorId = await priorityRepository.GetPriorities()
                .Where(x => x.Name.Equals(priorityName))
                .Select(x => x.Id)
                .FirstOrDefaultAsync();

            return priorId;
        }

        public async Task<string> GetPriorityNameById(string priorityId)
        {
            var priorName = await priorityRepository.GetPriorities()
                .Where(x => x.Id.Equals(priorityId))
                .Select(x => x.Name)
                .FirstOrDefaultAsync();

            return priorName;
        }
    }
}
