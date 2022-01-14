using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketingSystem_Helpdesk.Entities;
using TicketingSystem_Helpdesk.Models;

namespace TicketingSystem_Helpdesk.Managers
{
    public interface IInstitutionDepartmentManager
    {
        Task CreateInstitDept(InstitDeptModel institDeptModel);
        Task<bool> DeleteInstitDept(InstitDeptModel institDeptModel);
        Task<List<Department>> GetDepartmentsByInstitution(string idInstitution);
        Task<List<KeyValuePair<string, List<string>>>> GetDepartmentsGroupByInstitution();
    }
}
