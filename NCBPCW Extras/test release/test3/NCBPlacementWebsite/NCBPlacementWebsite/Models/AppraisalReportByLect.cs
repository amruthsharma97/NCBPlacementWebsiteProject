using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NCBPlacementWebsite.Models
{
    public class AppraisalReportByLect
    {

        [Required]
        [Display(Name = "Lecturer Name")]
        public string LuctName { get; set; }

        [Required]
        public int Year { get; set; }

    }
}