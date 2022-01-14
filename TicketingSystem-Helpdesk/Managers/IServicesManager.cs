using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketingSystem_Helpdesk.Entities;
using TicketingSystem_Helpdesk.Models;

namespace TicketingSystem_Helpdesk.Managers
{
    public interface IServicesManager
    {
        //Validation for data consistency in the database
        bool ValidationExistElement(List<string> element, string elements);
        bool ValidationCountZero(List<string> list);
        bool ValidationSignInResult(SignInResult signInResults);
        bool ValidationExistUser(User user);
        bool ValidationIdentityResul(IdentityResult identityResult);
        bool ValidationRegistration(string status);
        bool ValidationStringIsNull(string str);
        string CreateId();
        DateTime GenerateCurrentDate();
        string RemoveWhiteSpacesFromString(string str);
    }
}
