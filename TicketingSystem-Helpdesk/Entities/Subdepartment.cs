using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketingSystem_Helpdesk.Entities
{
    public class Subdepartment
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string DepartmentId { get; set; }
        public virtual Department Department { get; set; }
    }
}
