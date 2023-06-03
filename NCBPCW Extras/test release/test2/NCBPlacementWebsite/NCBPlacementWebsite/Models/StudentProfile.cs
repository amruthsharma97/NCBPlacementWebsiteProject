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



        [StringLength(12, MinimumLength = 12, ErrorMessage = "Invalid Aadhar Number.")]
        [RegularExpression(@"^(0*[1-9][0-9]*)$", ErrorMessage = "Invalid Aadhar Number.")]
        [Display(Name="Aadhar Number")]
        [Required]
        public string Aadnum { get; set; }


        [RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = "Register Number Should Not Contain Special Characters")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Invalid Register Number.")]
        [Display(Name="University Register Number")]
        public string URNo { get; set; }



        [RegularExpression(@"^[a-zA-Z\s\.]+$", ErrorMessage = "Name Should Not Contain Digits")]
        [Display(Name="First Name")]
        [StringLength(20)]
        [Required]
        public string FName { get; set; }


        [RegularExpression(@"^[a-zA-Z\s\.]+$", ErrorMessage = "Name Should Not Contain Digits")]
        [Display(Name = "Middle Name")]
        [StringLength(20)]
        public string MName { get; set; }


        [RegularExpression(@"^[a-zA-Z\s\.]+$", ErrorMessage = "Name Should Not Contain Digits")]
        [Display(Name = "Last Name")]
        [StringLength(20)]
        [Required]
        public string LName { get; set; }

        [Display(Name = "Mobile Number")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Invalid Mobile Number.")]
        [RegularExpression(@"^(0*[1-9][0-9]*)$", ErrorMessage = "Invalid Mobile Number.")]
        [Required]
        public string MNumber { get; set; }

        [Display(Name = "Email ID")]
        [Required]
        [EmailAddress]
        public string EmailId { get; set; }


        [Display(Name = "Date Of Birth")]
        [Required]
        public DateTime DOB { get; set; }


        [Display(Name = "Address Line-1")]
        public string AddrLine1 { get; set; }

        [Display(Name = "Address Line-2")]
        public string AddrLine2 { get; set; }

        [StringLength(6, MinimumLength = 6, ErrorMessage = "Invalid Pincode.")]
        [RegularExpression(@"^(0*[1-9][0-9]*)$", ErrorMessage = "Invalid Pincode.")]
        [Display(Name = "Pincode")]
        public string PostalCode { get; set; }


        [StringLength(50,ErrorMessage="String Length Exceeded!!!")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "City Name Should Not Contain Digits And Special Characters")]
        [Display(Name = "City")]
        public string City { get; set; }

        [StringLength(50, ErrorMessage = "String Length Exceeded!!!")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "State Name Should Not Contain Digits And Special Characters")]
        [Display(Name = "State")]
        public string State { get; set; }

        [StringLength(50, ErrorMessage = "String Length Exceeded!!!")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Country Name Should Not Contain Digits And Special Characters")]
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
        [StringLength(50, ErrorMessage = "String Length Exceeded!!!")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "State Name Should Not Contain Digits And Special Characters")]
        public string SSLCPassingState { get; set; }

        [Display(Name = "Country")]
        [StringLength(50, ErrorMessage = "String Length Exceeded!!!")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Country Name Should Not Contain Digits And Special Characters")]
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
        [StringLength(50, ErrorMessage = "String Length Exceeded!!!")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "State Name Should Not Contain Digits And Special Characters")]
        public string PUCPassingState { get; set; }

        [Display(Name = "Country")]
        [StringLength(50, ErrorMessage = "String Length Exceeded!!!")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Country Name Should Not Contain Digits And Special Characters")]
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