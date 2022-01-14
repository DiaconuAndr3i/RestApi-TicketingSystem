using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketingSystem_Helpdesk.Entities
{
    public class DepartmentInstitution
    {
        public string DepartmentId { get; set; }
        public string InstitutionId { get; set; }
        public virtual Department Department { get; set; }
        public virtual Institution Institution { get; set; }
    }
}
