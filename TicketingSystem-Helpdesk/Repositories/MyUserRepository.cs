using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketingSystem_Helpdesk.Entities;
using Microsoft.EntityFrameworkCore;

namespace TicketingSystem_Helpdesk.Repositories
{
    public class MyUserRepository : IMyUserRepository
    {
        private readonly Context context;

        public MyUserRepository(Context context)
        {
            this.context = context;
        }

        public IQueryable<User> GetAllUsers()
        {
            var users = context.Users;

            return users;
        }

        public IQueryable<string> GetUserRolesByInstitution(User user, string institutionId)
        {
            var userRoles = context.UserRoles
                .Include(x => x.Role)
                .Where(x => x.UserId == user.Id && x.InstitutionId == institutionId)
                .Select(x => x.Role.Name);

                return userRoles;
                            
        }

        public IQueryable<Institution> GetUserInstitutions(User user)
        {
            var userInstitutions = context.UserRoles
                .Include(x => x.Institution)
                .Where(x => x.UserId == user.Id)
                .Select(x => new Institution { Id = x.Institution.Id, Name = x.Institution.Name })
                .Distinct();

            return userInstitutions;
        }

        public IQueryable<Role> GetUserAllRoles(User user)
        {
            var userRoles = context.UserRoles
                .Include(x => x.Role)
                .Where(x => x.UserId == user.Id)
                .Select(x => new Role { Id = x.Role.Id, Name = x.Role.Name })
                .Distinct();

            return userRoles;
        }

        public async Task AddUserRoleAsync(UserRole userRole)
        {
            await context.UserRoles.AddAsync(userRole);

            await context.SaveChangesAsync();
        }

        public async Task DeleteUserRoleAsync(UserRole userRole)
        {
            context.UserRoles.Remove(userRole);

            await context.SaveChangesAsync();
        }

        public async Task DeleteUserAccount(User user)
        {
            context.Users.Remove(user);

            await context.SaveChangesAsync();
        }

        public void UpdateUser(User user)
        {
            context.Update(user);

            context.SaveChanges();
        }
    }
}
