using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using TicketingSystem_Helpdesk.Models;

namespace TicketingSystem_Helpdesk.Managers
{
    public class MailJet : IEmailManager
    {
        public async Task Send(string emailAddress, string body, string subject, EmailOptionsModel options)
        {
            var client = new SmtpClient();
            client.Host = options.Host;
            client.Credentials = new NetworkCredential(options.APIKey, options.APIKeySecret);
            client.Port = options.Port;

            var contentBody = new MailMessage(options.SenderEmail, emailAddress);
            contentBody.Body = body;
            contentBody.Subject = subject;
            contentBody.IsBodyHtml = true;
            await client.SendMailAsync(contentBody);
        }
    }
}
