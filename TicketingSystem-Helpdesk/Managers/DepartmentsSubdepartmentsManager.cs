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
    public class DepartmentsSubdepartmentsManager : IDepartmentsSubdepartmentsManager
    {
        private readonly IDepartmentsSubdepartmentsRepository departmentsSubdepartmentsRepository;

        public DepartmentsSubdepartmentsManager(IDepartmentsSubdepartmentsRepository departmentsSubdepartmentsRepository)
        {
            this.departmentsSubdepartmentsRepository = departmentsSubdepartmentsRepository;
        }

        public async Task<List<DepartmentModel>> GetAllDepartments()
        {
            var departments = await departmentsSubdepartmentsRepository
                .GetAllDepartments().ToListAsync();

            var list = new List<DepartmentModel>();

            foreach(var department in departments)
            {
                list.Add(new DepartmentModel()
                {
                    Id = department.Id,
                    Name = department.Name,
                    Description = department.Description
                });
            }

            return list;
        }

        public async Task CreateDepartment(DepartmentModel departmentModel)
        {
            var department = new Department()
            {
                Id = departmentModel.Id,
                Name = departmentModel.Name,
                Description = departmentModel.Description
            };

            await departmentsSubdepartmentsRepository.AddDepartment(department);
        }

        public async Task<bool> UpdateNameDescriptionDepartment(DepartmentModel newDepartmentModel)
        {
            var existingDepartment = await departmentsSubdepartmentsRepository.GetAllDepartments()
                .Where(x => x.Id.Equals(newDepartmentModel.Id))
                .FirstOrDefaultAsync();

            if (existingDepartment == null)
                return false;

            
            existingDepartment.Name = newDepartmentModel.Name;

            existingDepartment.Description = newDepartmentModel.Description;

            await departmentsSubdepartmentsRepository.SaveChanges();

            return true;
        }

        public async Task<bool> DeleteDepartment(string idDepartment)
        {
            var department = await departmentsSubdepartmentsRepository.GetAllDepartments()
                .Where(x => x.Id.Equals(idDepartment))
                .FirstOrDefaultAsync();

            if (department == null)
                return false;

            await departmentsSubdepartmentsRepository.DeleteDepartment(department);

            return true;
        }

        public async Task<List<SubdepartmentModel>> GetAllSubdepartmentByDepartmentId(string idDepartment)
        {
            var subdepartments = await departmentsSubdepartmentsRepository.GetAllSubdepartments()
                .Where(x => x.DepartmentId.Equals(idDepartment))
                .ToListAsync();

            var list = new List<SubdepartmentModel>();

            foreach(var subdepartment in subdepartments)
            {
                list.Add(new SubdepartmentModel()
                {
                    DepartmentModel = new DepartmentModel() { 
                        Id = subdepartment.DepartmentId,
                        Name = subdepartment.Name,
                        Description = subdepartment.Description
                    },
                    Id = subdepartment.Id
                });
            }

            return list;

        }

        public async Task CreateNewSubDepartmentIntoDepartment(SubdepartmentModel subdepartmentModel)
        {
            var subdepartment = new Subdepartment()
            {
                DepartmentId = subdepartmentModel.DepartmentModel.Id,
                Name = subdepartmentModel.DepartmentModel.Name,
                Description = subdepartmentModel.DepartmentModel.Description,
                Id = subdepartmentModel.Id
            };

            await departmentsSubdepartmentsRepository.AddSubdepartmentIntoDepartment(subdepartment);
        }

        public async Task<bool> DeleteSubdepartmentFromId(string idSubdepartment)
        {
            var subdepartment = await departmentsSubdepartmentsRepository.GetAllSubdepartments()
                .Where(x => x.Id.Equals(idSubdepartment))
                .FirstOrDefaultAsync();

            if (subdepartment == null)
                return false;

            await departmentsSubdepartmentsRepository.DeleteSubdepartment(subdepartment);

            return true;
        }

        public async Task<string> GetNameDepartmentById(string IdDepartment)
        {
            var department = await departmentsSubdepartmentsRepository.GetAllDepartments()
                .Where(x => x.Id.Equals(IdDepartment))
                .Select(x => x.Name)
                .FirstOrDefaultAsync();

            return department;
        }
    }
}
