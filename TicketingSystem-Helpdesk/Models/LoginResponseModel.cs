using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketingSystem_Helpdesk.Models
{
    public class LoginResponseModel
    {
        public string FirstName { get; set; }
        public string Lastname { get; set; }
        public string AccessToken { get; set; }
        public string Institution { get; set; }
        public List<string> Roles { get; set; }
    }
}
