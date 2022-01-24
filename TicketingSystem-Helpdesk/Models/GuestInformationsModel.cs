using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketingSystem_Helpdesk.Models
{
    public class GuestInformationsModel
    {
        public UserInformationsModel UserInformationsModel { get; set; }
        public List<string> RequiredRoles { get; set; }
    }
}
