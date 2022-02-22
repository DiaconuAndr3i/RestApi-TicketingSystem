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
    public class StatisticsController : ControllerBase
    {
        private readonly IStatisticsManager statisticsManager;

        public StatisticsController(IStatisticsManager statisticsManager)
        {
            this.statisticsManager = statisticsManager;
        }

        [HttpGet("/numberOfUsers")]
        public async Task<IActionResult> NumberOfUser()
        {
           var numberOfUsers = await statisticsManager.NumberOfUsers();

            return Ok(numberOfUsers);
        }

        [HttpGet("/numberOfTickets")]
        public async Task<IActionResult> NumberOfTickets()
        {
            var numberOfTickets = await statisticsManager.NumberOfTickets();

            return Ok(numberOfTickets);
        }

        [HttpGet("/numberOfInstituions")]
        public async Task<IActionResult> NumberOfInstituions()
        {
            var numberOfInstituions = await statisticsManager.NumberOfInstitutions();

            return Ok(numberOfInstituions);
        }

        [HttpGet("/percentageGuests")]
        public async Task<IActionResult> PercentageGuests()
        {
            var percentageGuests = await statisticsManager.GuestPercentage();

            return Ok(percentageGuests);
        }

        [HttpGet("/percentageDepartment/{institution}")]
        public async Task<IActionResult> PercentageDepartment([FromRoute] string institution)
        {
            var modelPercentage = await statisticsManager.MostLabeledDepartment(institution);

            return Ok(modelPercentage);
        }
    }
}
