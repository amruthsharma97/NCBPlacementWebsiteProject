using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NCBPlacementWebsite.Models
{
    public class Department
    {

        [Key]
        public int Id { get; set; }



        [Display(Name = "Department Name")]
        [RegularExpression(@"^[a-zA-Z\s\.]+$", ErrorMessage = "Department Name Should Not Contain Digits")]
        [Required]
        public string DeptName { get; set; }
    }
}