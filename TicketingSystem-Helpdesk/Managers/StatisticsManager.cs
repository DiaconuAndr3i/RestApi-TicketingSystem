using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TicketingSystem_Helpdesk.Entities;
using TicketingSystem_Helpdesk.Models;
using TicketingSystem_Helpdesk.Repositories;

namespace TicketingSystem_Helpdesk.Managers
{
    public class StatisticsManager : IStatisticsManager
    {
        private readonly IMyUserRepository myUserRepository;
        private readonly ITicketRepository ticketRepository;
        private readonly IInstitutionRepository institutionRepository;
        private readonly IGuestRepository guestRepository;
        private readonly IDepartmentsSubdepartmentsRepository departmentsSubdepartmentsRepository;
        private readonly UserManager<User> userManager;
        private readonly IInstitutionManager institutionManager;
        private readonly IServicesManager servicesManager;
        private readonly IMyUserManager myUserManager;

        public StatisticsManager(IMyUserRepository myUserRepository,
            ITicketRepository ticketRepository,
            IInstitutionRepository institutionRepository,
            IGuestRepository guestRepository,
            IDepartmentsSubdepartmentsRepository departmentsSubdepartmentsRepository,
            UserManager<User> userManager,
            IInstitutionManager institutionManager,
            IServicesManager servicesManager,
            IMyUserManager myUserManager)
        {
            this.myUserRepository = myUserRepository;
            this.ticketRepository = ticketRepository;
            this.institutionRepository = institutionRepository;
            this.guestRepository = guestRepository;
            this.departmentsSubdepartmentsRepository = departmentsSubdepartmentsRepository;
            this.userManager = userManager;
            this.institutionManager = institutionManager;
            this.servicesManager = servicesManager;
            this.myUserManager = myUserManager;
        }

        public async Task<decimal> GuestPercentage()
        {
            var numberOfGuests = await NumberOfGuests();
            var numberOfUsers = await NumberOfUsers();

            var percentage = Decimal.Round(Decimal.Divide(numberOfGuests * 100, numberOfUsers), 2);
         

            return percentage;
        }

        public async Task<int> NumberOfInstitutions()
        {
            var institutions = await institutionRepository.GetAllInstitutions().ToListAsync();

            return institutions.Count;
        }

        public async Task<int> NumberOfTickets()
        {
            var tickets = await ticketRepository.GetAllTickets().ToListAsync();

            return tickets.Count;
        }

        public async Task<object> NumberOfTicketsOpenClosed()
        {
            var ticketsTotal = await ticketRepository.GetAllTickets().ToListAsync();
            var ticketsOpen = await ticketRepository.GetAllTickets()
                .Where(x => x.StatusId.Equals("Open"))
                .ToListAsync();

            return new { 
                Open = ticketsOpen.Count, 
                Closed = ticketsTotal.Count - ticketsOpen.Count 
            };

        }

        public async Task<int> NumberOfUsers()
        {
            var users = await myUserRepository.GetAllUsers().ToListAsync();

            return users.Count;
        }

        public async Task<MostLabeledDepartmentModel> MostLabeledDepartment(string institution)
        {
            var departments = await departmentsSubdepartmentsRepository
                .GetAllDepartments()
                .Select(x => x.Name)
                .ToListAsync();

            var arrivalAddresses = await ticketRepository.GetAllTickets()
                .Select(x => x.Arrival)
                .ToListAsync();

            Dictionary<string, int> dict = new();

            foreach(var item in departments)
            {
                dict.Add(item, 0);
            }

            foreach(var arrivalAddress in arrivalAddresses)
            {
                var user = await userManager.FindByIdAsync(arrivalAddress);

                if (!servicesManager.ValidationExistUser(user))
                    return null;

                var institId = await institutionManager.GetIdInstitutionByName(institution);

                var rolesForThisUser = await myUserRepository
                    .GetUserRolesByInstitution(user, institId)
                    .ToListAsync();

                var stringForValidation = String.Join(",", rolesForThisUser.ToArray());

                foreach (var item in departments)
                {
                    if (stringForValidation.Contains(item))
                    {
                        dict[item] = dict[item] + 1;
                    }
                }
            }

            var maxValue = dict.Values.Max();
            var maxValueDeptName = dict
                .Where(x => x.Value == maxValue)
                .Select(x => x.Key)
                .FirstOrDefault();

            var mostLabeledDepartmentModel = new MostLabeledDepartmentModel()
            {
                DepartmentName = maxValueDeptName,
                Percentage = arrivalAddresses.Count != 0 ? maxValue / arrivalAddresses.Count * 100: 0
            };

            return mostLabeledDepartmentModel;
        }

        public async Task<int> NumberOfGuests()
        {
            var guests = await guestRepository.GetAllGuests().ToListAsync();

            return guests.Count;
        }

        public async Task<IDictionary<string, int>> PeoplePerDepartment(String institution)
        {
            IDictionary<string, int> departmentsNumberOfUsers = new Dictionary<string, int>();

            var departments = await departmentsSubdepartmentsRepository
                .GetAllDepartments().ToListAsync();

            foreach(var department in departments)
            {
                departmentsNumberOfUsers.Add(department.Name, 0);
            }

            var users = await myUserManager.GetUserInformations(institution);

            foreach(var user in users)
            {
                var userForCall = await userManager.FindByIdAsync(user.IdUser);
                var roles = await myUserRepository.GetUserAllRoles(userForCall).ToListAsync();
                foreach(var role in roles)
                {
                    var roleItem = Regex.Replace(role.Name.Split()[0], @"[^0-9a-zA-Z\ ]+", "");
                    departmentsNumberOfUsers[roleItem] += 1;
                }
            }

            return departmentsNumberOfUsers;

        }
    }
}
