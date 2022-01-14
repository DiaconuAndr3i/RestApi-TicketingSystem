using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketingSystem_Helpdesk.Managers;

namespace TicketingSystem_Helpdesk.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "Admin")]
    public class RolesController : ControllerBase
    {
        private readonly IRoleManager roleManager;

        public RolesController(IRoleManager roleManager)
        {
            this.roleManager = roleManager;
        }

        [HttpGet("/getAllRoles")]
        public async Task<IActionResult> GetAllRoles()
        {
            var roles = await roleManager.GetIdNameRoles();

            return Ok(roles);
        }

        [HttpPost("/createNewRole/{roleName}")]
        public async Task<IActionResult> CreateNewRole([FromRoute] string roleName)
        {
            await roleManager.CreateNewRole(roleName);

            return Ok("Role added.");
        }

        [HttpPut("/updateRole/{idRole}/{nameRole}")]
        public async Task<IActionResult> UpdateRole([FromRoute] string idRole, string nameRole)
        {
            if (await roleManager.UpdateRole(idRole, nameRole))
                return Ok("Role updated");
            return BadRequest("Something went wrong.");
        }

        [HttpDelete("/deleteRole")]
        public async Task<IActionResult> DeleteRole([FromBody] string idRole)
        {
            if (await roleManager.DelteRole(idRole))
                return Ok("Role deleted.");
            return BadRequest("Something went wrong.");
        }
    }
}