using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketingSystem_Helpdesk.Entities;
using TicketingSystem_Helpdesk.Models;
using TicketingSystem_Helpdesk.Repositories;

namespace TicketingSystem_Helpdesk.Managers
{
    public class InstitutionManager : IInstitutionManager
    {
        private readonly IInstitutionRepository institutionRepository;
        private readonly IServicesManager servicesManager;

        public InstitutionManager(IInstitutionRepository institutionRepository,
            IServicesManager servicesManager)
        {
            this.institutionRepository = institutionRepository;
            this.servicesManager = servicesManager;
        }

        public async Task<List<Institution>> GetAllInstitutions()
        {
            var institutions = await institutionRepository.GetAllInstitutions().ToListAsync();

            return institutions;
        }

        public async Task<List<object>> GetAllIdNameInstitutions()
        {
            var institutions = await GetAllInstitutions();

            var list = new List<object>();

            foreach (var item in institutions)
            {
                list.Add(new { IdInstitution = item.Id, NameInstitution = item.Name });
            }

            return list;
        }

        public async Task<string> GetIdInstitutionByName(string InstitutionName)
        {
            var institutions = institutionRepository.GetAllInstitutions();

            var institutionId = await institutions
                .Where(x => x.Name == InstitutionName)
                .Select(x => x.Id)
                .FirstOrDefaultAsync();

            return institutionId;
        }

        public async Task<List<string>> GetNameInstitutions()
        {
            var institutions = new List<string>();

            foreach(var item in await GetAllInstitutions())
            {
                institutions.Add(item.Name);
            }

            return institutions;
        }

        public List<string> GetNameUserInstitutions(List<Institution> userInstitutions)
        {
            var list = new List<string>();

            foreach(var item in userInstitutions)
            {
                list.Add(item.Name);
            }

            return list;
        }


        public async Task AddInstitution(string nameInstitution)
        {
            var institution = new Institution()
            {
                Id = servicesManager.RemoveWhiteSpacesFromString(nameInstitution),
                Name = nameInstitution
            };

            await institutionRepository.AddInsttitution(institution);
        }

        public async Task<bool> UpdateInstitution(string idInstitution, string nameInstitution)
        {
            var institution = await institutionRepository.GetAllInstitutions()
                .Where(x => x.Id.Equals(idInstitution))
                .FirstOrDefaultAsync();

            if (institution == null)
                return false;

            institution.Name = nameInstitution;

            await institutionRepository.SaveChanges();

            return true;
        }

        public async Task<bool> DeleteInstitution(string institutionId)
        {
            var institution = await institutionRepository.GetAllInstitutions()
                .Where(x => x.Id.Equals(institutionId))
                .FirstOrDefaultAsync();

            if (institution == null)
                return false;

            await institutionRepository.DeleteInstitution(institution);

            return true;


        }

        public async Task AddAddress(AddressModel addressModel)
        {
            var valAddress = new AddressInstitution()
            {
                Id = servicesManager.CreateId(),
                InstitutionId = addressModel.IdAddressInstitution,
                City = addressModel.City,
                Country = addressModel.Country,
                Street = addressModel.Street
            };

            await institutionRepository.AddAddress(valAddress);
        }

        public async Task<object> GetAddressByInstitutionId(string idInstitution)
        {
            var address = await institutionRepository.GetAllAddressInstitutions()
                .Where(x => x.InstitutionId.Equals(idInstitution))
                .FirstOrDefaultAsync();

            if (address == null)
                return null;

            var addressModel = new AddressModel()
            {
                IdAddressInstitution = address.InstitutionId,
                City = address.City,
                Country = address.Country,
                Street = address.Street
            };

            var addressModelWithIdAddress = new
            {
                AddressModel = addressModel,
                IdAddress = address.Id
            };

            return addressModelWithIdAddress;
        }

        public async Task<bool> DeleteAddress(string idAddress)
        {
            var address = await institutionRepository.GetAllAddressInstitutions()
                .Where(x => x.Id.Equals(idAddress))
                .FirstOrDefaultAsync();

            if (address == null)
                return false;

            await institutionRepository.DeleteAddress(address);

            return true;
        }

        public async Task<string> GetNameInstitutionById(string idInstitution)
        {
            var institution = await institutionRepository.GetAllInstitutions()
                .Where(x => x.Id.Equals(idInstitution))
                .Select(x => x.Name)
                .FirstOrDefaultAsync();

            return institution;
        }
    }
}
