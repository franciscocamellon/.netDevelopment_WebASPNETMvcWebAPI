using System.Collections.Generic;

namespace Presentation.Models
{
    public class MobileAppIndexViewModel
    {
        public  string Search { get; set; }
        public bool OrderAscendant { get; set; }
        public IEnumerable<MobileAppViewModel> MobileApps { get; set; }
    }
}
