using System.Collections.Generic;

namespace Presentation.Models
{
    public class DeveloperIndexViewModel
    {
        public  string Search { get; set; }
        public bool OrderAscendant { get; set; }
        public IEnumerable<DeveloperViewModel> Developers { get; set; }
    }
}
