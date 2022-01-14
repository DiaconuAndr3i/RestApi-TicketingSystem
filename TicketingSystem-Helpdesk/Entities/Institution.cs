using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketingSystem_Helpdesk.Entities
{
    public class Institution
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public virtual AddressInstitution AddressInstitution { get; set; }
        public virtual ICollection<UserRole> UserRoleInstitutions { get; set; }
        public virtual ICollection<DepartmentInstitution> DepartmentInstitutions { get; set; }
    }
}
