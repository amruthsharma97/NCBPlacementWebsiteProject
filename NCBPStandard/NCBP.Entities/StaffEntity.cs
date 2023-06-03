using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace NCBP.Entities
{
    public class StaffEntity
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        public string RollNo { get; set; }
        [Required]
        public string Qualification { get; set; }
        public string Designation { get; set; }
        [Display(Name = "Expereince")]
        public string Experiance { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
        [Required]
        [Display(Name = "Date of Birth")]
        public System.DateTime DateOfBirth { get; set; }
        [Required]
        [Display(Name = "Date of Joining")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
        public System.DateTime DateOfJoin { get; set; }
        [Required]
        public string Department { get; set; }
        [Display(Name = "E-Mail")]
        public string Email { get; set; }
        [Required]
        [RegularExpression("^([6-9]{1})([0-9]{9})$", ErrorMessage = "Please enter a valid phone number")]
        public string Phone { get; set; }
        public string Address { get; set; }
        public bool IsActive { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
    }
}
