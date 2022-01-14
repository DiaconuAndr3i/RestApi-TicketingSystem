using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketingSystem_Helpdesk.Entities
{
    public class Department
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<DepartmentInstitution> DepartmentInstitutions { get; set; }
        public virtual ICollection<Subdepartment> Subdepartments { get; set; }
    }
}
