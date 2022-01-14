using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketingSystem_Helpdesk.Entities
{
    public class GuestRoles
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string GuestId { get; set; }
        public virtual Guest Guest { get; set; }
    }
}
