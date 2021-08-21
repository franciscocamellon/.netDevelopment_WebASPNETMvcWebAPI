using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Model.Models;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Models
{
    public class MobileAppViewModel
    {
        public Guid Id { get; set; }
        [Required]
        [StringLength(150, MinimumLength = 5)]
        [Remote(action: "IsUnusedName", controller: "MobileApp", AdditionalFields = "Id")]
        public string AppName { get; set; }
        [Required]
        public bool PublishedStatus { get; set; }
        [DataType(DataType.Date)]
        public DateTime PublishedDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime? ModificationDate { get; set; }

        [Required]
        public Guid DeveloperId { get; set; }
        public DeveloperViewModel Developer { get; set; }

        public static MobileAppViewModel From(MobileAppModel mobileAppModel, bool firstMap = true)
        {
            var developer = firstMap
                ? DeveloperViewModel.From(mobileAppModel.Developer)
                : null;

            var mobileAppViewModel = new MobileAppViewModel
            {
                Id = mobileAppModel.Id,
                AppName = mobileAppModel.AppName,
                PublishedStatus = mobileAppModel.PublishedStatus,
                PublishedDate = mobileAppModel.PublishedDate,
                ModificationDate = mobileAppModel.ModificationDate,
                DeveloperId = mobileAppModel.DeveloperId,

                Developer = developer
            };
            return mobileAppViewModel;
        }

        public MobileAppModel ToModel(bool firstMap = true)
        {
            var developer = firstMap
                ? Developer?.ToModel()
                : null;

            var mobileAppModel = new MobileAppModel
            {
                Id = Id,
                AppName = AppName,
                PublishedStatus = PublishedStatus,
                PublishedDate = PublishedDate,
                ModificationDate = ModificationDate,
                DeveloperId = DeveloperId,

                Developer = developer
            };
            return mobileAppModel;
        }
    }
}
