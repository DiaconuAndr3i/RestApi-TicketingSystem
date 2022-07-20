using Confluent.Kafka;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketingSystem_Helpdesk.Entities;
using TicketingSystem_Helpdesk.Models;
using TicketingSystem_Helpdesk.Repositories;

namespace TicketingSystem_Helpdesk.Managers
{
    public class MyUserManager : IMyUserManager
    {
        private readonly IMyUserRepository myUserRepository;
        private readonly UserManager<User> userManager;
        private readonly IInstitutionManager institutionManager;
        private readonly IGuestManager guestManager;
        private readonly IRoleManager roleManager;
        private readonly IConfiguration configuration;
        private readonly IServicesManager servicesManager;
        private readonly IGuestRepository guestRepository;
        private readonly ProducerConfig producerConfig;

        public MyUserManager(IMyUserRepository myUserRepository,
            UserManager<User> userManager,
            IInstitutionManager institutionManager,
            IGuestManager guestManager,
            IRoleManager roleManager, 
            IConfiguration configuration,
            IServicesManager servicesManager,
            IGuestRepository guestRepository,
            ProducerConfig producerConfig)
        {
            this.myUserRepository = myUserRepository;
            this.userManager = userManager;
            this.institutionManager = institutionManager;
            this.guestManager = guestManager;
            this.roleManager = roleManager;
            this.configuration = configuration;
            this.servicesManager = servicesManager;
            this.guestRepository = guestRepository;
            this.producerConfig = producerConfig;
        }

        public async Task<UserRole> ConstructUserRole(HandleUserRoleModel handleUserRoleModel)
        {
            var user = await userManager.FindByEmailAsync(handleUserRoleModel.Email);
            var userRole = new UserRole
            {
                UserId = user.Id,
                InstitutionId = await institutionManager.GetIdInstitutionByName(handleUserRoleModel.InstitutionName),
                RoleId = await roleManager.GetIdRoleByName(handleUserRoleModel.RoleName)
            };

            return userRole;
        }

        public async Task<List<string>> GetUserRolesByInstitution(User user, string institutionName)
        {
            var institutionId = await institutionManager .GetIdInstitutionByName(institutionName);
            var roles = await myUserRepository.GetUserRolesByInstitution(user, institutionId).ToListAsync();

            return roles;
        }

        public async Task<bool> AddToUserRole(HandleUserRoleModel handleUserRoleModel)
        {
            if (!servicesManager
                    .ValidationExistElement(await roleManager.GetNameRoles(), handleUserRoleModel.RoleName) ||
                !servicesManager
                    .ValidationExistElement(await institutionManager.GetNameInstitutions(), handleUserRoleModel.InstitutionName))
                return false;

            var userRole = await ConstructUserRole(handleUserRoleModel);

            await myUserRepository.AddUserRoleAsync(userRole);

            return true;
        }

        public async Task<bool> DeleteRoleByInstitution(HandleUserRoleModel handleUserRoleModel)
        {
            var user = await userManager.FindByEmailAsync(handleUserRoleModel.Email);
            if (user == null)
                return false;

            var listRoles = await GetUserRolesByInstitution(user, handleUserRoleModel.InstitutionName);

            if (!servicesManager.ValidationExistElement(listRoles, handleUserRoleModel.RoleName))
                return false;
            var userRole = await ConstructUserRole(handleUserRoleModel);

            await myUserRepository.DeleteUserRoleAsync(userRole);

            return true;
        }


        public async Task<List<Role>> GetAllUserRoles(User user)
        {
            var userRoles = await myUserRepository.GetUserAllRoles(user).ToListAsync();

            return userRoles;
        }

        public async Task<List<Institution>> GetAllUserInstitutions(User user)
        {
            var userInstitutions = await myUserRepository.GetUserInstitutions(user).ToListAsync();

            return userInstitutions;
        }

        public async Task<List<UserInformationsModel>> GetUserInformations(string institutionName)
        {
            var listUserInformation = new List<UserInformationsModel>();
            var users = await myUserRepository.GetAllUsers()
                .ToListAsync();

            foreach(var user in users)
            {
                var userInstitution = await myUserRepository.GetUserInstitutions(user).ToListAsync();
                var userInstitutionName = institutionManager.GetNameUserInstitutions(userInstitution);

                if (!servicesManager.ValidationExistElement(userInstitutionName, institutionName))
                    continue;

                listUserInformation.Add(new UserInformationsModel
                {
                    IdUser = await userManager.GetUserIdAsync(user),
                    FirstName = user.First_name,
                    LastName = user.Last_name,
                    Email = await userManager.GetEmailAsync(user),
                    PhoneNumber = await userManager.GetPhoneNumberAsync(user),
                    Roles = await GetUserRolesByInstitution(user, institutionName),
                    Institution = institutionName
                });
            }

            return listUserInformation;
        }

        public async Task<List<UserInformationsModel>> GetUserInformationsByInstitDeptSubdept(string institutionName,
            string departmentName, string subdepartmentName)
        {
            var userInformations = new List<UserInformationsModel>();
            var users = await GetUserInformations(institutionName);

            var nameRoleFromEndPoint = departmentName + " " + subdepartmentName;
            if (subdepartmentName.Equals("-"))
                nameRoleFromEndPoint = departmentName;

            foreach (var user in users)
            {
                if (user.Roles.Contains(nameRoleFromEndPoint))
                    userInformations.Add(user);
            }

            return userInformations;
        }

        public async Task<bool> AssignmentOfRequiredRole(HandleUserRoleModel handleUserRoleModel)
        {
            var defaultRole = configuration.GetSection("Roles").GetSection("DefaultRole").Get<string>();
            var deleteRolebyInstitution = new HandleUserRoleModel
            {
                Email = handleUserRoleModel.Email,
                InstitutionName = handleUserRoleModel.InstitutionName,
                RoleName = defaultRole
            };

            var user = await userManager.FindByEmailAsync(handleUserRoleModel.Email);


            if (!servicesManager.ValidationExistUser(user))
                return false;


            if (!await DeleteRoleByInstitution(deleteRolebyInstitution) ||
                !await AddToUserRole(handleUserRoleModel))
                return false;

            await guestManager.DeleteGuest(user.Id);

            _ = await ProduceMessageForKafkaBroker();

            return true;
        }

        //Producer for kafka broker
        public async Task<Boolean> ProduceMessageForKafkaBroker() {
            var numberOfGuests = (await guestRepository.GetAllGuests().ToListAsync()).Count;
            var numberOfUsers = (await myUserRepository.GetAllUsers().ToListAsync()).Count;
            var percentageGuest = Decimal.Round(Decimal.Divide(numberOfGuests * 100, numberOfUsers), 2);

            string serializedJson = JsonConvert.SerializeObject(percentageGuest);

            using (var producerForKafkaBroker = new ProducerBuilder<Null, string>(producerConfig).Build()) 
            {
                await producerForKafkaBroker.ProduceAsync("userGuest", new Message<Null, string> { Value = serializedJson });
                producerForKafkaBroker.Flush(TimeSpan.FromSeconds(20));
                return true;
            }
        }

        public async Task<List<GuestInformationsModel>> GetGuestInformations(string institutionName)
        {
            List<UserInformationsModel> listUserInformation = await GetUserInformations(institutionName);
            var filteredListUserInformation = new List<GuestInformationsModel>();
            var defaulRole = configuration.GetSection("Roles").GetSection("DefaultRole").Get<string>();


            foreach (var item in listUserInformation)
            {
                if (item.Roles.Contains(defaulRole))
                {
                    var guestInformationsModel = new GuestInformationsModel
                    {
                        UserInformationsModel = item,
                        RequiredRoles = await guestManager.GetGuestRoles(item.IdUser)
                    };
                    filteredListUserInformation.Add(guestInformationsModel);
                }
            }

            return filteredListUserInformation;
        }

        public async Task<bool> DeleteUserAccount(string emailUser)
        {
            var user = await userManager.FindByEmailAsync(emailUser);

            if (user == null)
                return false;

            await myUserRepository.DeleteUserAccount(user);

            _ = await ProduceMessageForKafkaBroker();

            return true;
        }
    }
}
