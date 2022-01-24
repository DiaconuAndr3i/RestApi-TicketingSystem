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
    [Authorize(Policy = "AllRolesWithoutGuest")]
    public class TicketController : ControllerBase
    {
        private readonly ITicketManager ticketManager;

        public TicketController(ITicketManager ticketManager)
        {
            this.ticketManager = ticketManager;
        }

        [HttpPost("createTicket")]
        public async Task<IActionResult> CreateTicket([FromBody] CreateTicketModel createTicketModel)
        {
            if (await ticketManager.CreateTicket(createTicketModel))
                return Ok();

            return BadRequest("Something went wrong.");

        }


        /*
         * direction true means that we will receive information about tickets that have been sent by someone
         * direction false means that we will receive information about tickets that have been received from someone
         */
        [HttpGet("/getTicketsByCreatorOrArrival/{email}/{direction}")]
        public async Task<IActionResult> GetTicketsByCreatorOrArrival([FromRoute] string email, bool direction)
        {
            var tickets = await ticketManager.GetAllTicketsByCreatorArrivalEmail(email, direction);

            return Ok(tickets);
        }


        [HttpGet("/getTicketsByStatus/{email}/{status}/{direction}")]
        public async Task<IActionResult> GetTicketsByStatus([FromRoute] string status, string email, bool direction)
        {
            var tickets = await ticketManager.GetAllTicketsByStatus(status, email, direction);

            return Ok(tickets);
        }

        [HttpGet("/getTicketsByPriority/{email}/{priority}/{direction}")]
        public async Task<IActionResult> GetTicketsByPriority([FromRoute] string priority, string email, bool direction)
        {
            var tickets = await ticketManager.GetAllTicketsByPriority(priority, email, direction);

            return Ok(tickets);
        }

        [HttpDelete("/deleteTicketById")]
        public async Task<IActionResult> DeleteTicketById([FromBody] string idTicket)
        {
            if (await ticketManager.DeleteTicket(idTicket))
                return Ok("Ticket deleted.");

            return BadRequest("The ticket with the specified id doesn't exist.");
        }

        [HttpPut("/changeStatusTicket/{idTicket}/{status}")]
        public async Task<IActionResult> ChangeStatusTicket([FromRoute] string idTicket, string status)
        {
            if (await ticketManager.ChangeStatusTicket(idTicket, status))
                return Ok("Status changed.");
            return BadRequest("Something went wrong.");
        }
    }
}
