using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketingSystem_Helpdesk.Entities;

namespace TicketingSystem_Helpdesk.Repositories
{
    public class InstitutionRepository : IInstitutionRepository
    {
        private readonly Context context;
        public InstitutionRepository(Context context)
        {
            this.context = context;
        }

        public IQueryable<Institution> GetAllInstitutions()
        {
            var institutions = context.Institutions;

            return institutions;
        }

        public async Task AddInsttitution(Institution institution)
        {
            await context.Institutions.AddAsync(institution);

            await SaveChanges();
        }

        public async Task DeleteInstitution(Institution institution)
        {
            context.Institutions.Remove(institution);

            await SaveChanges();
        }

        public async Task SaveChanges()
        {
            await context.SaveChangesAsync();
        }

        public async Task AddAddress(AddressInstitution addressInstitution)
        {
            await context.AddressInstitutions.AddAsync(addressInstitution);

            await SaveChanges();
        }

        public IQueryable<AddressInstitution> GetAllAddressInstitutions()
        {
            var addressInstitution = context.AddressInstitutions;

            return addressInstitution;
        }

        public async Task DeleteAddress(AddressInstitution addressInstitution)
        {
            context.AddressInstitutions.Remove(addressInstitution);

            await SaveChanges();
        }
    }
}
