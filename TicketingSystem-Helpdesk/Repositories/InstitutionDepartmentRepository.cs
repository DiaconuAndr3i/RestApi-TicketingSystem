using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketingSystem_Helpdesk.Entities;

namespace TicketingSystem_Helpdesk.Repositories
{
    public class InstitutionDepartmentRepository : IInstitutionDepartmentRepository
    {
        private readonly Context context;

        public InstitutionDepartmentRepository(Context context)
        {
            this.context = context;
        }

        public async Task CreateInstitDept(DepartmentInstitution departmentInstitution)
        {
            await context.DepartmentInstitutions.AddAsync(departmentInstitution);

            await SaveChanges();
        }

        public async Task DeleteInstitDept(DepartmentInstitution departmentInstitution)
        {
            context.DepartmentInstitutions.Remove(departmentInstitution);

            await SaveChanges();
        }

        public async Task SaveChanges()
        {
            await context.SaveChangesAsync();
        }

        public IQueryable<DepartmentInstitution> GetAllInstitDept()
        {
            var institDept = context.DepartmentInstitutions;

            return institDept;
        }

        public IQueryable<Department> GetDepartmentByInstitution(string idInstitution)
        {
            var listDepartments = context.DepartmentInstitutions
                .Include(x => x.Department)
                .Where(x => x.InstitutionId.Equals(idInstitution))
                .Select(x => x.Department);

            return listDepartments;
        }
    }
}
