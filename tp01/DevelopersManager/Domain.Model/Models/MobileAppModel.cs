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
		public string Name { get; set; }
		public bool PublishedStatus { get; set; }
		public DateTime PublishedDate { get; set; }
        public string DevId { get; set; } 
        public DeveloperModel Developer { get; set; }  
	
    }
}
