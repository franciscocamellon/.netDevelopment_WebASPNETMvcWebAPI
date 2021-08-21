using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Model.Models;

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

        public static DeveloperViewModel From(DeveloperModel developerModel)
        {
            var developerViewModel = new DeveloperViewModel
            {
                Id = developerModel.Id,
                FirstName = developerModel.FirstName,
                LastName = developerModel.LastName,
                GraduationDate = developerModel.GraduationDate,
                EmployedStatus = developerModel.EmployedStatus,
                PublishedApps = developerModel.PublishedApps,

                MobileApps = developerModel?.MobileApps.Select(x => MobileAppViewModel.From(x, false)).ToList(),
            };
            return developerViewModel;
        }

        public DeveloperModel ToModel()
        {
            var developerModel = new DeveloperModel
            {
                Id = Id,
                FirstName = FirstName,
                LastName = LastName,
                GraduationDate = GraduationDate,
                EmployedStatus = EmployedStatus,
                PublishedApps = PublishedApps,

                MobileApps = MobileApps?.Select(x => x.ToModel(false)).ToList(),
            };

            return developerModel;
        }
    }
}
