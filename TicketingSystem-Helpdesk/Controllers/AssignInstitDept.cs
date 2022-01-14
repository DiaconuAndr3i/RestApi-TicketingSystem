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
    public class AssignInstitDept : ControllerBase
    {
        private readonly IInstitutionDepartmentManager institutionDepartmentManager;

        public AssignInstitDept(IInstitutionDepartmentManager institutionDepartmentManager)
        {
            this.institutionDepartmentManager = institutionDepartmentManager;
        }

        [HttpPost("/createAssignInstitDept")]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> CreateAssignInstitDept([FromBody] InstitDeptModel institDeptModel)
        {
            await institutionDepartmentManager.CreateInstitDept(institDeptModel);

            return Ok("Assignment created.");
        }

        [HttpDelete("/deleteIntitDept")]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> DeleteInstitDept([FromBody] InstitDeptModel institDeptModel)
        {
            if (await institutionDepartmentManager.DeleteInstitDept(institDeptModel))
                return Ok("Deleted succesffully.");
            return BadRequest("Something went wrong.");
        }

        [HttpGet("/getDepartmentByInstitution/{idInstitution}")]
        [Authorize(Policy = "AllRolesWithoutGuest")]
        public async Task<IActionResult> GetDepartmentByInstitution([FromRoute] string idInstitution)
        {
            var departments = await institutionDepartmentManager.GetDepartmentsByInstitution(idInstitution);

            return Ok(departments);
        }

        [HttpGet("/getDepartmentsGroupByInstitution")]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> GetDepartmentsGroupByInstitution()
        {
            var list = await institutionDepartmentManager.GetDepartmentsGroupByInstitution();

            return Ok(list);
        }
    }
}
