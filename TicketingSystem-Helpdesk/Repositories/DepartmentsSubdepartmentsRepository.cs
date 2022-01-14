using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketingSystem_Helpdesk.Entities;

namespace TicketingSystem_Helpdesk.Repositories
{
    public class DepartmentsSubdepartmentsRepository : IDepartmentsSubdepartmentsRepository
    {
        private readonly Context context;

        public DepartmentsSubdepartmentsRepository(Context context)
        {
            this.context = context;
        }

        public async Task AddDepartment(Department department)
        {
            await context.Departments.AddAsync(department);

            await SaveChanges();
        }

        public async Task DeleteDepartment(Department department)
        {
            context.Departments.Remove(department);

            await SaveChanges();
        }

        public IQueryable<Department> GetAllDepartments()
        {
            var departments = context.Departments;

            return departments;
        }

        public IQueryable<Subdepartment> GetAllSubdepartments()
        {
            var subdepartments = context.Subdepartments;

            return subdepartments;
        }

        public async Task SaveChanges()
        {
            await context.SaveChangesAsync();
        }

        public async Task AddSubdepartmentIntoDepartment(Subdepartment subdepartment)
        {
            await context.Subdepartments.AddAsync(subdepartment);

            await SaveChanges();
        }

        public async Task DeleteSubdepartment(Subdepartment subdepartment)
        {
            context.Subdepartments.Remove(subdepartment);

            await SaveChanges();
        }
    }
}
