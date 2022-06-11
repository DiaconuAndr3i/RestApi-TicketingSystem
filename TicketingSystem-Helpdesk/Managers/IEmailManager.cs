using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketingSystem_Helpdesk.Models;

namespace TicketingSystem_Helpdesk.Managers
{
    public interface IEmailManager
    {
        Task Send(string emailAddress, string body, string subject, EmailOptionsModel options);
    }
}
