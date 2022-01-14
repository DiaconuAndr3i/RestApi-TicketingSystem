using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketingSystem_Helpdesk.Entities;

namespace TicketingSystem_Helpdesk.Repositories
{
    public interface IInstitutionRepository
    {
        IQueryable<Institution> GetAllInstitutions();
        Task AddInsttitution(Institution institution);
        Task DeleteInstitution(Institution institution);
        Task SaveChanges();
        Task AddAddress(AddressInstitution addressInstitution);
        IQueryable<AddressInstitution> GetAllAddressInstitutions();
        Task DeleteAddress(AddressInstitution addressInstitution);
    }
}
