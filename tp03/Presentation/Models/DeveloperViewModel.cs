using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Presentation.Models
{
    public class DeveloperViewModel
    {
        public Guid Id { get; set; }
        [Required]
        [StringLength(150, MinimumLength = 5)]
        [Display(Name = "Nome")]
        public string FirstName { get; set; }
        [Required]
        [StringLength(150)]
        [Display(Name = "Sobrenome")]
        public string LastName { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Data de formação")]
        public DateTime GraduationDate { get; set; }
        [Display(Name = "Empregado")]
        public bool EmployedStatus { get; set; }
        [Range(minimum:0, maximum:999)]
        [Display(Name = "Apps publicados")]
        public int? PublishedApps { get; set; }

        public List<MobileAppViewModel> MobileApps { get; set; }
    }
}
