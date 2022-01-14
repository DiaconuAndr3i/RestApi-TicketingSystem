using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketingSystem_Helpdesk.Entities;

namespace TicketingSystem_Helpdesk.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly Context context;

        public RoleRepository(Context context)
        {
            this.context = context;
        }

        public IQueryable<Role> GetAllRoles()
        {
            var roles = context.Roles;

            return roles;
        }

        public async Task AddRole(Role role)
        {
            await context.Roles.AddAsync(role);

            await SaveChanges();
        }

        public async Task SaveChanges()
        {
            await context.SaveChangesAsync();
        }

        public async Task DeleteRole(Role role)
        {
            context.Roles.Remove(role);

            await SaveChanges();
        }
    }
}
