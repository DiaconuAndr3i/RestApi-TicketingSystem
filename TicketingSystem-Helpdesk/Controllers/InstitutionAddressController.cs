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
    public class InstitutionAddressController : ControllerBase
    {
        private readonly IInstitutionManager institutionManager;

        public InstitutionAddressController(IInstitutionManager institutionManager)
        {
            this.institutionManager = institutionManager;
        }

        [HttpGet("/getAllInstitutions")]
        //[Authorize(Policy = "AllRoles")]
        public async Task<IActionResult> GetAllInstituions()
        {
            var instituions = await institutionManager.GetAllIdNameInstitutions();

            return Ok(instituions);
        }

        [HttpGet("/addInstitution/{institutionName}")]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> AddInstitution([FromRoute] string institutionName)
        {
            await institutionManager.AddInstitution(institutionName);

            return Ok("Institution added.");
        }

        [HttpPut("/updateInstitution/{idInstitution}/{nameInstitution}")]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> UpdateInstitution([FromRoute]string idInstitution, string nameInstitution)
        {
            if (await institutionManager.UpdateInstitution(idInstitution, nameInstitution))
                return Ok("Institution updated.");
            return BadRequest("Something went wrong.");
        }

        [HttpDelete("/deleteInstitution")]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> DeleteInstitution([FromBody] string institutionId)
        {
            if (await institutionManager.DeleteInstitution(institutionId))
                return Ok("Institution deleted.");
            return BadRequest("Something went wrong.");
        }

        [HttpPost("/addAddress")]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> AddAddress([FromBody] AddressModel addressModel)
        {
            await institutionManager.AddAddress(addressModel);

            return Ok("Address added.");
        }

        [HttpPost("/getAddressByInstitutionId")]
        [Authorize(Policy = "AllRolesWithoutGuest")]
        public async Task<IActionResult> GetAddressByInstitutionId([FromBody] string idInstitution)
        {
            var address = await institutionManager.GetAddressByInstitutionId(idInstitution);

            if (address == null)
                return BadRequest("The institution does not have an established address.");

            return Ok(address);
        }

        [HttpDelete("/deleteAddress")]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> DeleteAddress([FromBody] string idAddress)
        {
            if (await institutionManager.DeleteAddress(idAddress))
                return Ok("Institution deleted.");
            return BadRequest("Something went wrong.");
        }
    }
}
