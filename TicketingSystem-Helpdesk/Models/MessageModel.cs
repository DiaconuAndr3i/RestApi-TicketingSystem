using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketingSystem_Helpdesk.Models
{
    public class MessageModel
    {
        public string TicketId { get; set; }
        public string ContentMessage { get; set; }
        public string EmailCreator { get; set; }
    }
}
