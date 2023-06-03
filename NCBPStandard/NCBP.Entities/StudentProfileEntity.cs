using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCBP.Entities
{
    public class StudentProfileEntity
    {
        public Guid Id { get; set; }

        public string Aadnum { get; set; }

        public string URNo { get; set; }

        public string FName { get; set; }

        public string MName { get; set; }

        public string LName { get; set; }

        public System.DateTime DOB { get; set; }

        public string MNumber { get; set; }

        public string EmailId { get; set; }

        public string Passcode { get; set; }

        public int EnroledYear { get; set; }

        public int ExpectedYOP { get; set; }

        public string AddressLine1 { get; set; }

        public string AddressLine2 { get; set; }

        public string PostalCode { get; set; }

        public System.DateTime CreatedDate { get; set; }

        public System.DateTime ApprovedDate { get; set; }

        public string ApprovedBy { get; set; }

        public int Status { get; set; }

        public int CourseId { get; set; }

        public int CityId { get; set; }

        public int StateId { get; set; }

        public int CountryId { get; set; }
    }
}
