using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketingSystem_Helpdesk.Entities
{
    public class Message
    {
        public string Id { get; set; }
        public string TicketId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ContentMessage { get; set; }
        public string AuthorMessage { get; set; }
        public virtual Ticket Ticket { get; set; }
    }
}
