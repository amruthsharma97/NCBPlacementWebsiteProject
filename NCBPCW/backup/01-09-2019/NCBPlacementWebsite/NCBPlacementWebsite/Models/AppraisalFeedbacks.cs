using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NCBPlacementWebsite.Models
{
    public class AppraisalFeedbacks
    {

        [Key]
        public int Id { get; set; }


        [Display(Name = "Branch")]
        [Required]
        public string Branch { get; set; }

        [Display(Name = "Lecturer Name")]
        [Required]
        public string LuctName { get; set; }


        [Display(Name = "Batch")]
        [Required]
        public int SYear { get; set; }


        [Display(Name = "Year")]
        [Required]
        public int EYear { get; set; }

        [Display(Name = "1.	Regularity in conducting the classes ")]
        [Required(ErrorMessage="Select One!!!")]
        public int Q1 { get; set; }

        [Display(Name = "2.	Punctuality ")]
        [Required(ErrorMessage = "Select One!!!")]
        public int Q2 { get; set; }

        [Display(Name = "3.	Preparation for the classes ")]
        [Required(ErrorMessage = "Select One!!!")]
        public int Q3 { get; set; }

        [Display(Name = "4.	Completion of syllabus on time ")]
        [Required(ErrorMessage = "Select One!!!")]
        public int Q4 { get; set; }

        [Display(Name = "5.	Competency to handle to the subject ")]
        [Required(ErrorMessage = "Select One!!!")]
        public int Q5 { get; set; }

        [Display(Name = "6.	Presentation skills like Voice, clarity and language ")]
        [Required(ErrorMessage = "Select One!!!")]
        public int Q6 { get; set; }

        [Display(Name = "7.	Methodology used to import the knowledge ")]
        [Required(ErrorMessage = "Select One!!!")]
        public int Q7 { get; set; }

        [Display(Name = "8.	Interaction with the students ")]
        [Required(ErrorMessage = "Select One!!!")]
        public int Q8 { get; set; }

        [Display(Name = "9.	Accessibility ")]
        [Required(ErrorMessage = "Select One!!!")]
        public int Q9 { get; set; }

        [Display(Name = "10. Role as a Mentor ")]
        [Required(ErrorMessage = "Select One!!!")]
        public int Q10 { get; set; }
    }
}