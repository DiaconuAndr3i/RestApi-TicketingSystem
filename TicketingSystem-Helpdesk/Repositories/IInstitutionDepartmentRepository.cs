using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketingSystem_Helpdesk.Entities;

namespace TicketingSystem_Helpdesk.Repositories
{
    public interface IInstitutionDepartmentRepository
    {
        Task CreateInstitDept(DepartmentInstitution departmentInstitution);
        Task SaveChanges();
        Task DeleteInstitDept(DepartmentInstitution departmentInstitution);
        IQueryable<DepartmentInstitution> GetAllInstitDept();
        IQueryable<Department> GetDepartmentByInstitution(string idInstitution);
    }
}
