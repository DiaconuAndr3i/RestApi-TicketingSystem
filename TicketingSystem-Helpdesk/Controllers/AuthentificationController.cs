using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
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
    public class AuthentificationController : ControllerBase
    {
        private readonly IAuthentificationManager authentificationManager;
        private readonly IServicesManager servicesManager;

        public AuthentificationController(IAuthentificationManager authentificationManager,
            IServicesManager servicesManager)
        {
            this.authentificationManager = authentificationManager;
            this.servicesManager = servicesManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel registerModel)
        {
            if (registerModel.RoleName.Equals("Administrator"))
                return BadRequest("Registration as Admin is denied.");

            var status = await authentificationManager.Register(registerModel); 

            if (servicesManager.ValidationRegistration(status))
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {
            try
            {
                var loginResponseModel = await authentificationManager.Login(loginModel);

                if (servicesManager.ValidationStringIsNull(loginResponseModel.AccessToken))
                {
                    return Ok(loginResponseModel);
                }
                else
                {
                    return BadRequest("Login Failed!");
                }
            }

            catch(Exception exp)
            {
                return BadRequest($"System error! '{exp}'");
            }
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenModel refreshTokenModel)
        {
            var response = await authentificationManager.RefreshToken(refreshTokenModel);

            if (response == null)
                return Unauthorized(new { message = "Invalid token" });

            return Ok(response);
        }

    }
}
