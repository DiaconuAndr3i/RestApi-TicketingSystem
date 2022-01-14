using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketingSystem_Helpdesk.Entities
{
    public class Ticket
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Arrival { get; set; }
        public string UserId { get; set; }
        public string PriorityId { get; set; }
        public string StatusId { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<TagTicket> TagTickets { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
        public virtual Priority Priority { get; set; }
        public virtual Status Status { get; set; }
    }
}
