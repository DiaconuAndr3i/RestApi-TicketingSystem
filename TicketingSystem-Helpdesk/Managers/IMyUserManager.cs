using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketingSystem_Helpdesk.Entities;
using TicketingSystem_Helpdesk.Models;

namespace TicketingSystem_Helpdesk.Managers
{
    public interface IMyUserManager
    {
        Task<List<string>> GetUserRolesByInstitution(User user, string institutionName);
        Task<bool> AddToUserRole(HandleUserRoleModel handleUserRoleModel);
        Task<List<Role>> GetAllUserRoles(User user);
        Task<List<Institution>> GetAllUserInstitutions(User user);
        Task<List<UserInformationsModel>> GetUserInformations(string institutionName);
        //For admins to view user informations
        Task<UserRole> ConstructUserRole(HandleUserRoleModel handleUserRoleModel);
        Task<bool> DeleteRoleByInstitution(HandleUserRoleModel handleUserRoleModel);
        Task<bool> AssignmentOfRequiredRole(HandleUserRoleModel handleUserRoleModel);
        Task<List<GuestInformationsModel>> GetGuestInformations(string institutionName);
        //For admins to view guest informations
        Task<bool> DeleteUserAccount(string emailUser);
        Task<List<UserInformationsModel>> GetUserInformationsByInstitDeptSubdept(string institutionName,
            string departmentName, string subdepartmentName);
        Task<Boolean> ProduceMessageForKafkaBroker();
    }
}
