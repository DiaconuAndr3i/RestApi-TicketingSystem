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
    public class InstitutionDepartmentManager : IInstitutionDepartmentManager
    {
        private readonly IInstitutionDepartmentRepository institutionDepartmentRepository;
        private readonly IInstitutionManager institutionManager;
        private readonly IDepartmentsSubdepartmentsManager departmentsSubdepartmentsManager;

        public InstitutionDepartmentManager(IInstitutionDepartmentRepository institutionDepartmentRepository,
            IInstitutionManager institutionManager,
            IDepartmentsSubdepartmentsManager departmentsSubdepartmentsManager)
        {
            this.institutionDepartmentRepository = institutionDepartmentRepository;
            this.institutionManager = institutionManager;
            this.departmentsSubdepartmentsManager = departmentsSubdepartmentsManager;
        }

        public async Task CreateInstitDept(InstitDeptModel institDeptModel)
        {
            var departmentInstitution = new DepartmentInstitution()
            {
                DepartmentId = institDeptModel.IdDept,
                InstitutionId = institDeptModel.IdInstit
            };

            await institutionDepartmentRepository.CreateInstitDept(departmentInstitution);
        }

        public async Task<bool> DeleteInstitDept(InstitDeptModel institDeptModel)
        {
            var institDept = await institutionDepartmentRepository.GetAllInstitDept()
                .Where(x => x.DepartmentId.Equals(institDeptModel.IdDept) &&
                x.InstitutionId.Equals(institDeptModel.IdInstit))
                .FirstOrDefaultAsync();

            if (institDept == null)
                return false;

            await institutionDepartmentRepository.DeleteInstitDept(institDept);

            return true;
        }

        public async Task<List<Department>> GetDepartmentsByInstitution(string idInstitution)
        {
            var listDepartmenst = await institutionDepartmentRepository
                .GetDepartmentByInstitution(idInstitution)
                .ToListAsync();

            return listDepartmenst;
        }

        public async Task<List<KeyValuePair<string, List<string>>>> GetDepartmentsGroupByInstitution()
        {
            var institDept = await institutionDepartmentRepository.GetAllInstitDept().ToListAsync();

            var query = from obj in institDept
                        group obj by obj.InstitutionId into newGroup
                        orderby newGroup.Key
                        select newGroup;

            var list = new List<KeyValuePair<string, List<string>>>();

            foreach (var i in query)
            {
                var mylist = new List<string>();
                foreach (var j in i)
                {
                    mylist.Add(await departmentsSubdepartmentsManager
                        .GetNameDepartmentById(j.DepartmentId));
                }
                list.Add(new KeyValuePair<string, List<string>>(await institutionManager
                    .GetNameInstitutionById(i.Key), mylist));
            }

            return list;
        }
    }
}
