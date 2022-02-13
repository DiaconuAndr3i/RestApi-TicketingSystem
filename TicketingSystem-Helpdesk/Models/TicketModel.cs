using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketingSystem_Helpdesk.Models
{
    public class TicketModel
    {
        public string Title { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ArrivalEmail { get; set; }
        public string TicketCreatorEmail { get; set; }
        public bool NewActivity { get; set; }
        public string Priority { get; set; }
        public string Status { get; set; }
        public List<string> Tag { get; set; }
    }
}
