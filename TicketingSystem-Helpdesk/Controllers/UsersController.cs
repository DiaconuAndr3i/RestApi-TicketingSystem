using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketingSystem_Helpdesk.Entities;
using TicketingSystem_Helpdesk.Managers;
using TicketingSystem_Helpdesk.Models;

namespace TicketingSystem_Helpdesk.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMyUserManager myUserManager;

        public UsersController(IMyUserManager myUserManager)
        {
            this.myUserManager = myUserManager;

        }

        [HttpGet("/getAllUsers/{institutionName}")]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> GetAllUsers([FromRoute] string institutionName)
        {
            var list = await myUserManager.GetUserInformations(institutionName);

            return Ok(list);
        }

        [HttpGet("/getAllUsersByInstitDeptSubdept/{institutionName}/{departmentName}/{subdepartmentName}")]
        [Authorize(Policy = "AllRolesWithoutGuest")]
        public async Task<IActionResult> GetAllUsersByInstitDeptSubdept([FromRoute] string institutionName, 
            string departmentName, string subdepartmentName)
        {
            var list = await myUserManager
                .GetUserInformationsByInstitDeptSubdept(institutionName, departmentName, subdepartmentName);

            return Ok(list);
        }

        [HttpGet("/getAllGuests/{institutionName}")]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> GetGuestUsersWithRequiredRoles([FromRoute] string institutionName)
        {
            var list = await myUserManager.GetGuestInformations(institutionName);

            return Ok(list);
        }

        [HttpPost("/assignRequiredRolesUsers")]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> AssignRequiredRolesUsers(
            [FromBody] HandleUserRoleModel handleUserRoleModel)
        {
            if (await myUserManager.AssignmentOfRequiredRole(handleUserRoleModel))
                return Ok("User account roles changed.");

            return BadRequest("Something went wrong.");
        }

        [HttpPut("/addUserRole")]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> AddUserRole([FromBody] HandleUserRoleModel handleUserRoleModel)
        {
            if (await myUserManager.AddToUserRole(handleUserRoleModel))
                return Ok("Role Added.");
            return BadRequest("Something went wrong.");
        }

        [HttpDelete("/deleteUserRole")]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> DeleteUserRole([FromBody] HandleUserRoleModel handleUserRoleModel)
        {
            if (await myUserManager.DeleteRoleByInstitution(handleUserRoleModel))
                return Ok("Role deleted.");
            return BadRequest("Something went wrong.");
        }

        [HttpDelete("/deleteUserAccount")]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> DeleteUserAccount([FromBody] DeleteAccountModel deleteAccountModel)
        {
            if (await myUserManager.DeleteUserAccount(deleteAccountModel.Email))
                return Ok("Account deleted.");
            return BadRequest("The account with the specified email address doesn't exist.");
        }


    }
}
