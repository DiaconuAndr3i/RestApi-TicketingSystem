using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketingSystem_Helpdesk.Entities;
using TicketingSystem_Helpdesk.Repositories;

namespace TicketingSystem_Helpdesk.Managers
{
    public class StatusManager : IStatusManager
    {
        private readonly IStatusRepository statusRepository;

        public StatusManager(IStatusRepository statusRepository)
        {
            this.statusRepository = statusRepository;
        }
        public async Task<List<Status>> GetAllStatus()
        {
            var statuses = await statusRepository.GetStatuses().ToListAsync();

            return statuses;
        }

        public async Task<List<string>> GetAllStatusName()
        {
            var list = new List<string>();

            foreach(var item in await GetAllStatus())
            {
                list.Add(item.Name);
            }

            return list;
        }

        public async Task<string> GetStatusIdByName(string statusName)
        {
            var statId = await statusRepository.GetStatuses()
                .Where(x => x.Name.Equals(statusName))
                .Select(x => x.Id)
                .FirstOrDefaultAsync();

            return statId;
        }

        public async Task<string> GetStatusNameById(string statusId)
        {
            var statName = await statusRepository.GetStatuses()
                .Where(x => x.Id.Equals(statusId))
                .Select(x => x.Name)
                .FirstOrDefaultAsync();

            return statName;
        }
    }
}
