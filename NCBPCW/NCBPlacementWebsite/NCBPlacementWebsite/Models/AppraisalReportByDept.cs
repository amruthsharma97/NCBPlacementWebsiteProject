using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NCBPlacementWebsite.Models
{
    public class AppraisalReportByDept
    {
        [Required]
        [Display(Name="Department Name")]
        public string DeptName { get; set; }

        [Required]
        public int Year { get; set; }
    }
}