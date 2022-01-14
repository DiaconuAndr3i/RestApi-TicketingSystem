using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketingSystem_Helpdesk.Models;

namespace TicketingSystem_Helpdesk.Managers
{
    public interface IAuthentificationManager
    {
        Task<string> Register(RegisterModel registerModel);
        Task<LoginResponseModel> Login(LoginModel loginModel);
    }
}
