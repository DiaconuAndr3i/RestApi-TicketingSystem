using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketingSystem_Helpdesk.Entities;
using TicketingSystem_Helpdesk.Models;
using TicketingSystem_Helpdesk.Repositories;

namespace TicketingSystem_Helpdesk.Managers
{
    public class ServicesManager : IServicesManager
    {
        private readonly IConfiguration configuration;

        public ServicesManager(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string CreateId()
        {
            var id = DateTime.Now.ToString("yyyyMMddHHmmssffff");

            return id;
        }

        public DateTime GenerateCurrentDate()
        {
            return DateTime.Now;
        }

        public bool ValidationCountZero(List<string> list)
        {
            if (list.Count == 0)
                return false;

            return true;
        }

        public bool ValidationExistElement(List<string> elements, string element)
        {
            if (!elements.Contains(element))
                return false;

            return true;
        }

        public bool ValidationExistUser(User user)
        {
            if (user != null)
                return true;

            return false;
        }

        public bool ValidationIdentityResul(IdentityResult identityResult)
        {
            if (identityResult.Succeeded)
                return true;

            return false;
        }

        public bool ValidationRegistration(string status)
        {
            var statusOk = configuration.GetSection("RegisterStatus").GetSection("StatusOk").Get<string>();

            if (status == statusOk)
                return true;

            return false;
        }

        public bool ValidationSignInResult(SignInResult signInResults)
        {
            if (signInResults.Succeeded)
                return true;

            return false;
        }

        public bool ValidationStringIsNull(string str)
        {
            if (str != null)
                return true;

            return false;
        }

        public string RemoveWhiteSpacesFromString(string str)
        {
            var retstr = String.Concat(str.Where(c => !Char.IsWhiteSpace(c)));

            return retstr;
        }
    }
}
