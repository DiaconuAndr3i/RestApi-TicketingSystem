using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketingSystem_Helpdesk.Entities;

namespace TicketingSystem_Helpdesk.Managers
{
    public interface IRoleManager
    {
        Task<string> GetIdRoleByName(string roleName);
        Task<List<Role>> GetAllRoles();
        Task<List<string>> GetNameRoles();
        List<string> GetNameUserRoles(List<Role> userRoles);
        Task CreateNewRole(string nameRole);
        Task<List<object>> GetIdNameRoles();
        Task<bool> UpdateRole(string idRole, string nameRole);
        Task<bool> DelteRole(string idRole);
    }
}
