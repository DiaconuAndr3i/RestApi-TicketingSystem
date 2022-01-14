using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketingSystem_Helpdesk.Entities;

namespace TicketingSystem_Helpdesk.Repositories
{
    public interface IDepartmentsSubdepartmentsRepository
    {
        Task AddDepartment(Department department);
        Task DeleteDepartment(Department department);
        Task SaveChanges();
        IQueryable<Department> GetAllDepartments();
        IQueryable<Subdepartment> GetAllSubdepartments();
        Task AddSubdepartmentIntoDepartment(Subdepartment subdepartment);
        Task DeleteSubdepartment(Subdepartment subdepartment);
    }
}
