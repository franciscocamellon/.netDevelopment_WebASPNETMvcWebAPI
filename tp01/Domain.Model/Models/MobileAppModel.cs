using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model.Models
{
    public class MobileAppModel
    {
        public Guid Id { get; set; }
        public string AppName { get; set; }
        public bool PublishedStatus { get; set; }
        public DateTime PublishedDate { get; set; }
        public DateTime? ModificationDate { get; set; }

        public Guid DeveloperId { get; set; }
        public DeveloperModel Developer { get; set; }

    }
}
