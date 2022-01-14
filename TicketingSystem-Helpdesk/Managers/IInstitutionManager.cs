using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketingSystem_Helpdesk.Entities;
using TicketingSystem_Helpdesk.Models;

namespace TicketingSystem_Helpdesk.Managers
{
    public interface IInstitutionManager
    {
        Task<string> GetIdInstitutionByName(string InstitutionName);
        Task<List<Institution>> GetAllInstitutions();
        Task<List<string>> GetNameInstitutions();
        List<string> GetNameUserInstitutions(List<Institution> userInstitutions);
        Task AddInstitution(string nameInstitution);
        Task<bool> UpdateInstitution(string idInstitution, string nameInstitution);
        Task<bool> DeleteInstitution(string institutionId);
        Task<List<object>> GetAllIdNameInstitutions();
        Task AddAddress(AddressModel addressModel);
        Task<object> GetAddressByInstitutionId(string idInstitution);
        Task<bool> DeleteAddress(string idAddress);
        Task<string> GetNameInstitutionById(string idInstitution);
    }
}
