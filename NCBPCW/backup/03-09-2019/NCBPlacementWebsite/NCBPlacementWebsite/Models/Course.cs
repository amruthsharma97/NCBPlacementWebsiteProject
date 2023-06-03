using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NCBPlacementWebsite.Models
{
    public class Course
    {


        [Key]
        public int Id { get; set; }

        [Display(Name = "Course")]
        [RegularExpression(@"^[a-zA-Z\s\.\-]+$", ErrorMessage = "Course Name Should Not Contain Digits")]
        [Required]
        public string Crse { get; set; }

    }
}