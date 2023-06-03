using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NCBPlacementWebsite.Models
{
    public class Subjects
    {

        [Key]
        public int Id { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z0-9\-\s\.]+$", ErrorMessage = "Course Name Should Not Contain Digits")]
        public string Subject { get; set; }

        [Display(Name = "Course")]
        [RegularExpression(@"^[a-zA-Z\s\.]+$", ErrorMessage = "Course Name Should Not Contain Digits")]
        [Required]
        public string Branch { get; set; }
    }
}