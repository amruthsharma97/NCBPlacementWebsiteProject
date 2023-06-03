using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NCBPlacementWebsite.Models
{
    public class StudentProfile
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Display(Name="University Register Number")]
        public string URNo { get; set; }

        [Display(Name="First Name")]
        [Required]
        public string FName { get; set; }

        [Display(Name = "Middle Name")]
        public string MName { get; set; }

        [Display(Name = "Last Name")]
        [Required]
        public string LName { get; set; }

        [Display(Name = "Mobile Number")]
        [Required]
        public string MNumber { get; set; }

        [Display(Name = "Email ID")]
        [Required]
        public string EmailId { get; set; }


        [Display(Name = "Date Of Birth")]
        [Required]
        public DateTime DOB { get; set; }


        [Display(Name = "Address Line-1")]
        public string AddrLine1 { get; set; }

        [Display(Name = "Address Line-2")]
        public string AddrLine2 { get; set; }

        [Display(Name = "Pincode")]
        public int PostalCode { get; set; }

        [Display(Name = "City")]
        public string City { get; set; }

        [Display(Name = "State")]
        public string State { get; set; }

        [Display(Name = "Country")]
        public string Country { get; set; }

        [Display(Name = "Percentage")]
        [Required]
        public float SSLCPercentage { get; set; }

        [Display(Name = "Board")]
        public string SSLCBoard { get; set; }

        [Display(Name = "Name Of School")]
        public string NameOfSchool { get; set; }

        [Display(Name = "State")]
        public string SSLCPassingState { get; set; }

        [Display(Name = "Country")]
        public string SSLCPassingCOuntry { get; set; }

        [Display(Name = "Year Of Passing")]
        [Required]
        public int SSLCYOP { get; set; }

        [Display(Name = "Percentage")]
        [Required]
        public float PUCPercentage { get; set; }

        [Display(Name = "Board")]
        public string PUCBoard { get; set; }

        [Display(Name = "Name Of College")]
        public string NameOfClg { get; set; }

        [Display(Name = "State")]
        public string PUCPassingState { get; set; }

        [Display(Name = "Country")]
        public string PUCPassingCOuntry { get; set; }

        [Display(Name = "Year Of Passing")]
        [Required]
        public int PUCYOP { get; set; }

        [Display(Name = "Percentage")]
        public float DegreePercentage { get; set; }

        [Display(Name = "Branch")]
        [Required]
        public string Branch { get; set; }

        [Display(Name = "Year Of Passing")]
        [Required]
        public int DegreeYOP { get; set; }

        public int Status { get; set; }
    }
}