using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NCBPlacementWebsite.Models
{
    public class CurriculumReportByCrse
    {

        [Required]
        [Display(Name = "Course")]
        public string Crse { get; set; }

        [Required]
        public int Year { get; set; }
    }
}