using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace NCBP.Entities
{
    public class PreviousExamEntity
    {
        public int Id { get; set; }
        [Display(Name = "Previous Institution")]
        public string PreviousInstitution { get; set; }
        [Display(Name = "Previous Course Name")]
        public string PreviousCourseName { get; set; }
        [Display(Name = "Month and Year of Passing")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM}")]
        public System.DateTime MonthAndYearOfPassing { get; set; }
        public string Result { get; set; }
        public Guid StudentId { get; set; }
        public int StateId { get; set; }
        public int CountryId { get; set; }
    }
}
