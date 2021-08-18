using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model.Model
{
    public class DeveloperModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime GraduationDate { get; set; }
        public bool EmployedStatus { get; set; }
        public int PublishedApps { get; set; }
    }
}
