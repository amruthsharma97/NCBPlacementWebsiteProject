using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NCBPlacementWebsite.Models
{
    
    public class ContactList
    {

        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [RegularExpression(@"^[a-zA-Z\s\.]+$", ErrorMessage = "Name Should Not Contain Digits")]
        [Display(Name = "Name")]
        [StringLength(50)]
        [Required]
        public string Name { get; set; }

        [RegularExpression(@"^[a-zA-Z\s\.]+$", ErrorMessage = "Department Name Should Not Contain Digits")]
        [Display(Name = "Department")]
        [StringLength(100)]
        [Required]
        public string Dept { get; set; }


        [Display(Name = "Mobile Number")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Invalid Mobile Number.")]
        [RegularExpression(@"^(0*[1-9][0-9]*)$", ErrorMessage = "Invalid Mobile Number.")]
        [Required]
        public string PhoneNo { get; set; }

        [Display(Name = "Email ID")]
        [Required]
        [EmailAddress]
        public string MailId { get; set; }


    }
}