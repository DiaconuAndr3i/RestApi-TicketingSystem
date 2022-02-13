using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketingSystem_Helpdesk.Entities;
using TicketingSystem_Helpdesk.Models;

namespace TicketingSystem_Helpdesk.Repositories
{
    public interface IMyUserRepository
    {
        IQueryable<User> GetAllUsers();
        IQueryable<Role> GetUserAllRoles(User user);
        IQueryable<string> GetUserRolesByInstitution(User user, string institutionId);
        IQueryable<Institution> GetUserInstitutions(User user);
        Task AddUserRoleAsync(UserRole userRole);
        Task DeleteUserRoleAsync(UserRole userRole);
        Task DeleteUserAccount(User user);
        void UpdateUser(User user);
    }
}
