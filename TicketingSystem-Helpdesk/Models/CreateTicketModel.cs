using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketingSystem_Helpdesk.Models
{
    public class CreateTicketModel
    {
        public TicketModel TicketModel { get; set; }
        public string ContentMessage { get; set; }
    }
}
