using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NCBPlacementWebsite.Models
{
    public class CurriculumReportBySubj
    {
        [Required]
        public string Subject { get; set; }

        [Required]
        public int Year { get; set; }
    }
}