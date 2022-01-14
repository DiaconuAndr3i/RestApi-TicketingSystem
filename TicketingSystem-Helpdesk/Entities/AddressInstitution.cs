using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketingSystem_Helpdesk.Entities
{
    public class AddressInstitution
    {
        public string Id { get; set; }
        public string InstitutionId { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Street { get; set; }
        public virtual Institution Institution { get; set; }
    }
}
