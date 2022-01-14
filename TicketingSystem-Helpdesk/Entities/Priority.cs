using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketingSystem_Helpdesk.Entities
{
    public class Priority
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public virtual Ticket Ticket { get; set; }
    }
}
