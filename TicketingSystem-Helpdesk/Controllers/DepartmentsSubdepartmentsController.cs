using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketingSystem_Helpdesk.Managers;
using TicketingSystem_Helpdesk.Models;

namespace TicketingSystem_Helpdesk.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsSubdepartmentsController : ControllerBase
    {
        private readonly IDepartmentsSubdepartmentsManager departmentsSubdepartmentsManager;

        public DepartmentsSubdepartmentsController(IDepartmentsSubdepartmentsManager departmentsSubdepartmentsManager)
        {
            this.departmentsSubdepartmentsManager = departmentsSubdepartmentsManager;
        }

        [HttpGet("/getAllDepartments")]
        [Authorize(Policy = "AllRolesWithoutGuest")]
        public async Task<IActionResult> GetAllDepartments()
        {
            var departments = await departmentsSubdepartmentsManager.GetAllDepartments();

            return Ok(departments);
        }


        [HttpPost("/createDepartment")]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> CreateDepartment([FromBody] DepartmentModel departmentModel)
        {
            await departmentsSubdepartmentsManager.CreateDepartment(departmentModel);

            return Ok("Department added.");
        }

        [HttpPut("/updateNameDescriptionDepartment")]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> UpdateNameDescriptionDepartment([FromBody] DepartmentModel departmentModel)
        {
            if (await departmentsSubdepartmentsManager.UpdateNameDescriptionDepartment(departmentModel))
                return Ok("Department updated.");
            return BadRequest("Something went wrong.");
        }

        [HttpDelete("/deleteDepartment")]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> DeleteDepartment([FromBody] string idDepartment)
        {
            if (await departmentsSubdepartmentsManager.DeleteDepartment(idDepartment))
                return Ok("Department deleted.");
            return BadRequest("Something went wrong.");
        }

        [HttpGet("/getSubdepartmentsByDepartment/{idDepartment}")]
        [Authorize(Policy = "AllRolesWithoutGuest")]
        public async Task<IActionResult> GetSubdepartmentsByDepartment([FromRoute] string idDepartment)
        {
            var subdepartments = await departmentsSubdepartmentsManager.GetAllSubdepartmentByDepartmentId(idDepartment);

            return Ok(subdepartments);
        }

        [HttpPost("/createSubdepartmentIntoDep")]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> CreateSubdepartmentIntoDep([FromBody] SubdepartmentModel subdepartmentModel)
        {
            await departmentsSubdepartmentsManager.CreateNewSubDepartmentIntoDepartment(subdepartmentModel);

            return Ok("Subdepartment added.");
        }

        [HttpDelete("/deleteSubdepartment")]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> DeleteSubdepartment([FromBody] string idSubdepartment)
        {
            if (await departmentsSubdepartmentsManager.DeleteSubdepartmentFromId(idSubdepartment))
                return Ok("Subdepartment deleted.");
            return BadRequest("Something went wrong.");
        }

    }
}
