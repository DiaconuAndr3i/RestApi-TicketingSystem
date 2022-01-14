using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketingSystem_Helpdesk.Models
{
    public class RegisterModel
    {
        public string First_name { get; set; }
        public string Last_name { get; set; }
        public string Phone_number { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string InstitutionName { get; set; }
        public string RoleName { get; set; }
    }
}
