using System;
using System.ComponentModel.DataAnnotations;
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
    }
}
