using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Model.Models;

namespace Presentation.Models
{
    public class MobileAppIndexViewModel
    {
        public  string Search { get; set; }
        public bool OrderAscendant { get; set; }
        public IEnumerable<MobileAppModel> MobileApps { get; set; }
    }
}
