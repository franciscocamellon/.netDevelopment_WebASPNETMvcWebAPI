using System;
using System.Collections.Generic;

namespace Domain.Model.Models
{
    public class DeveloperModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime GraduationDate { get; set; }
        public bool EmployedStatus { get; set; }
        public int PublishedApps { get; set; }

        public List<MobileAppModel> MobileApps {get; set;}
    }
}
