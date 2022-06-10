using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketingSystem_Helpdesk.Models;

namespace TicketingSystem_Helpdesk.Managers
{
    public interface IStatisticsManager
    {
        Task<int> NumberOfUsers();
        Task<int> NumberOfGuests();
        Task<IDictionary<string, int>> PeoplePerDepartment(String institution);
        Task<object> NumberOfTicketsOpenClosed();
        Task<int> NumberOfTickets();
        Task<int> NumberOfInstitutions();
        Task<decimal> GuestPercentage();
        Task<MostLabeledDepartmentModel> MostLabeledDepartment(string institution);
    }
}
