using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketingSystem_Helpdesk.Entities;
using TicketingSystem_Helpdesk.Repositories;

namespace TicketingSystem_Helpdesk.Managers
{
    public class RoleManager : IRoleManager
    {
        private readonly IRoleRepository roleRepository;

        public RoleManager(IRoleRepository roleRepository)
        {
            this.roleRepository = roleRepository;
        }

        public async Task<List<Role>> GetAllRoles()
        {
            var roles = await roleRepository.GetAllRoles().ToListAsync();

            return roles;
        }

        public async Task<string> GetIdRoleByName(string roleName)
        {
            var roles = roleRepository.GetAllRoles();
            var roleId = await roles
                .Where(x => x.Name == roleName)
                .Select(x => x.Id)
                .FirstOrDefaultAsync();

            return roleId;
        }

        public async Task<List<string>> GetNameRoles()
        {
            var roles = new List<string>();
            foreach(var item in await GetAllRoles())
            {
                roles.Add(item.Name);
            }

            return roles;
        }

        public List<string> GetNameUserRoles(List<Role> userRoles)
        {
            var list = new List<string>();

            foreach(var item in userRoles)
            {
                list.Add(item.Name);
            }

            return list;
        }

        public async Task<List<object>> GetIdNameRoles()
        {
            var roles = await GetAllRoles();
            var nameIdRoleObj = new List<object>();

            foreach(var item in roles)
            {
                nameIdRoleObj.Add(new { RoleId = item.Id, RoleName = item.Name });
            }

            return nameIdRoleObj;
        }

        public async Task CreateNewRole(string nameRole)
        {
            var role = new Role()
            {
                Id = nameRole,
                Name = nameRole,
                NormalizedName = nameRole,
                ConcurrencyStamp = null
            };

            await roleRepository.AddRole(role);
        }

        public async Task<bool> UpdateRole(string idRole, string nameRole)
        {
            var role = await roleRepository.GetAllRoles()
                .Where(x => x.Id.Equals(idRole))
                .FirstOrDefaultAsync();

            if (role == null)
                return false;

            role.Name = nameRole;

            await roleRepository.SaveChanges();

            return true;

        }

        public async Task<bool> DelteRole(string idRole)
        {
            var role = await roleRepository.GetAllRoles()
                .Where(x => x.Id.Equals(idRole))
                .FirstOrDefaultAsync();

            if (role == null)
                return false;

            await roleRepository.DeleteRole(role);

            return true;
        }
    }
}
