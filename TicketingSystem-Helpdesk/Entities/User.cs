using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketingSystem_Helpdesk.Entities
{
    public class User : IdentityUser
    {
        public string First_name { get; set; }
        public string Last_name { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }
        public ICollection<Ticket> Tickets { get; set; }
        public virtual Guest Guest { get; set; }
    }
}
