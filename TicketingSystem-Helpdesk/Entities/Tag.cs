using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketingSystem_Helpdesk.Entities
{
    public class Tag
    {
        public string Id { get; set; }
        public string TagType { get; set; }
        public virtual ICollection<TagTicket> TagTickets { get; set; }
    }
}
