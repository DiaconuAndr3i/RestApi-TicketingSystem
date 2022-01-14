using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketingSystem_Helpdesk.Models;

namespace TicketingSystem_Helpdesk.Managers
{
    public interface IDepartmentsSubdepartmentsManager
    {
        Task<List<DepartmentModel>> GetAllDepartments();
        Task CreateDepartment(DepartmentModel departmentModel);
        Task<bool> UpdateNameDescriptionDepartment(DepartmentModel newDepartmentModel);
        Task<bool> DeleteDepartment(string idDepartment);
        Task<List<SubdepartmentModel>> GetAllSubdepartmentByDepartmentId(string idDepartment);
        Task CreateNewSubDepartmentIntoDepartment(SubdepartmentModel subdepartmentModel);
        Task<bool> DeleteSubdepartmentFromId(string idSubdepartment);
        Task<string> GetNameDepartmentById(string IdDepartment);
    }
}
