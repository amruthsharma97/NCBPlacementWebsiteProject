using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NCBPlacementWebsite.Models
{
    public class CurriculumFeedbacks
    {

        [Key]
        public int Id { get; set; }

        [Display(Name = "Student Name")]
        [Required]
        public string SName { get; set; }


        [RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = "Register Number Should Not Contain Special Characters")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Invalid Register Number.")]
        [Display(Name="University Register Number")]
        [Required]
        public string URNo { get; set; }


        [Display(Name = "Branch")]
        [Required]
        public string Branch { get; set; }

        [Required]
        public string Subject { get; set; }

        [Display(Name = "Batch")]
        [Required]
        public int SYear { get; set; }

        [Display(Name = "Year")]
        [Required]
        public int EYear { get; set; }

        [Display(Name = "1. Is the design of the syllabus and the sequencing of units in the syllabus coherent?")]
        [Required(ErrorMessage = "Select One!!!")]
        public string Q1 { get; set; }

        [Display(Name = "2. Are the objectives of the syllabus met?")]
        [Required(ErrorMessage = "Select One!!!")]
        public string Q2 { get; set; }

        [Display(Name = "3. Do you think the syllabus is challenging? ")]
        [Required(ErrorMessage = "Select One!!!")]
        public string Q3 { get; set; }

        [Display(Name = "4. Is the number of hours allotted to the syllabus adequate?")]
        [Required(ErrorMessage = "Select One!!!")]
        public string Q4 { get; set; }

        [Display(Name = "5. Where the given support material inspiring? ")]
        [Required(ErrorMessage = "Select One!!!")]
        public string Q5 { get; set; }

        [Display(Name = "6. Do the theoretical concepts help to relate with real life situations?")]
        [Required(ErrorMessage = "Select One!!!")]
        public string Q6 { get; set; }

        [Display(Name = "7. Were the assignments designed to help you understand the syllabus better?")]
        [Required(ErrorMessage = "Select One!!!")]
        public string Q7 { get; set; }

        [Display(Name = "8. Were the assignments challenging your understanding? ")]
        [Required(ErrorMessage = "Select One!!!")]
        public string Q8 { get; set; }

        [Display(Name = "9. Do the Tests challenge your comprehension of the topic discussed?")]
        [Required(ErrorMessage = "Select One!!!")]
        public string Q9 { get; set; }

        [Display(Name = "10. Is the laboratory well equipped for the practical work?")]
        [Required(ErrorMessage = "Select One!!!")]
        public string Q10 { get; set; }

        [Display(Name = "11. Does the Library have reading materials related to the syllabus?")]
        [Required(ErrorMessage = "Select One!!!")]
        public string Q11 { get; set; }

        [Display(Name = "12. Were the internal tests held in a fair manner?")]
        [Required(ErrorMessage = "Select One!!!")]
        public string Q12 { get; set; }

        [Display(Name = "13. Did you participate actively in the class? ")]
        [Required(ErrorMessage = "Select One!!!")]
        public string Q13 { get; set; }

        [Display(Name = "14. Are the classrooms learner-centric?")]
        [Required(ErrorMessage = "Select One!!!")]
        public string Q14 { get; set; }

        [Display(Name = "15. Are you satisfied with your progress in the discipline of your interest?s")]
        [Required(ErrorMessage = "Select One!!!")]
        public string Q15 { get; set; }

        [Display(Name = "16. Is the syllabus designed to help you to take up higher education?")]
        [Required(ErrorMessage = "Select One!!!")]
        public string Q16 { get; set; }

        [Display(Name = "17. Does the syllabus motivate you take up research?")]
        [Required(ErrorMessage = "Select One!!!")]
        public string Q17 { get; set; }

        [Display(Name = "18. Does the syllabus equip you with employability skills?")]
        [Required(ErrorMessage = "Select One!!!")]
        public string Q18 { get; set; }

        [Display(Name = "19. Is the syllabus relevant in the global context?")]
        [Required(ErrorMessage = "Select One!!!")]
        public string Q19 { get; set; }

    }
}