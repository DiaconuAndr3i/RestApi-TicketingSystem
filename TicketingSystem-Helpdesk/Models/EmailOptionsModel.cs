using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketingSystem_Helpdesk.Models
{
    public class EmailOptionsModel
    {
        public string Host { get; set; }
        public string APIKey { get; set; }
        public string APIKeySecret { get; set; }
        public int Port { get; set; }
        public string SenderEmail { get; set; }
    }
}
