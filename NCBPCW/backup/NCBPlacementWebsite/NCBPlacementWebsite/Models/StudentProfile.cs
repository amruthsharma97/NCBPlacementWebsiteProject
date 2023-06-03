using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NCBPlacementWebsite.Models
{
    public class StudentProfile
    {
        
        public int Id { get; set; }

        
        public string URNo { get; set; }

        [Required]
        public string FName { get; set; }

        public string MName { get; set; }

        [Required]
        public string LName { get; set; }

        [Required]
        public string MNumber { get; set; }


        [Required]
        public string EmailId { get; set; }


        [Required]
        public DateTime DOB { get; set; }

        
        public string AddrLine1 { get; set; }

        public string AddrLine2 { get; set; }

        public int PostalCode { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        [Required]
        public float SSLCPercentage { get; set; }

        public string SSLCBoard { get; set; }

        public string NameOfSchool { get; set; }

        public string SSLCPassingState { get; set; }

        public string SSLCPassingCOuntry { get; set; }

        [Required]
        public int SSLCYOP { get; set; }

        [Required]
        public float PUCPercentage { get; set; }

        public string PUCBoard { get; set; }

        public string NameOfClg { get; set; }

        public string PUCPassingState { get; set; }

        public string PUCPassingCOuntry { get; set; }

        [Required]
        public int PUCYOP { get; set; }

        public float DegreePercentage { get; set; }

        [Required]
        public string Branch { get; set; }

        [Required]
        public int DegreeYOP { get; set; }

        public int Status { get; set; }
    }
}