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
    public class MessageController : ControllerBase
    {
        private readonly IMessageManager messageManager;

        public MessageController(IMessageManager messageManager)
        {
            this.messageManager = messageManager;
        }

        [HttpPost("/createMessage")]
        public async Task<IActionResult> CreateMessage([FromBody] MessageModel messageModel)
        {
            await messageManager.AddMessage(messageModel);

            return Ok();
        }

        [HttpGet("/getMessagesFromTicket/{idTicket}")]
        public async Task<IActionResult> GetMessagesFromTicket([FromRoute] string idTicket)
        {
            var messages = await messageManager.GetMessagesFromTicket(idTicket);

            return Ok(messages);
        }

        [HttpDelete("/deleteMessage")]
        public async Task<IActionResult> DeleteMessage([FromBody] string idMessage)
        {
            if (await messageManager.DeleteMessage(idMessage))
                return Ok("Message deleted.");
            return BadRequest("Something went wrong.");
        }

        [HttpPut("/updateMessage/{idMessage}")]
        public async Task<IActionResult> UpdateMessage([FromBody] MessageModel messageModel, [FromRoute] string idMessage)
        {
            if (await messageManager.UpdateMessage(idMessage, messageModel))
                return Ok("Successfully updated");
            return BadRequest("Something went wrong.");
        }
    }
}
