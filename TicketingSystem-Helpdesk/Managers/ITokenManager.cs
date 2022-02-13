using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketingSystem_Helpdesk.Entities;

namespace TicketingSystem_Helpdesk.Managers
{
    public interface ITokenManager
    {
        string GenerateAccessToken(List<string> roles);
        RefreshToken GenerateRefreshToken();
    }
}
