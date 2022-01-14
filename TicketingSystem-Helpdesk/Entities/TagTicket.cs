using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketingSystem_Helpdesk.Entities
{
    public class TagTicket
    {
        public string TagId { get; set; }
        public string TicketId { get; set; }
        public virtual Tag Tag { get; set; }
        public virtual Ticket Ticket { get; set; }
    }
}
