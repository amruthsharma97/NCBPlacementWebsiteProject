//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NCBP.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class StudentProfile
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public StudentProfile()
        {
            this.PreviousExamInfoes = new HashSet<PreviousExamInfo>();
            this.Semesters = new HashSet<Semester>();
        }
    
        public System.Guid Id { get; set; }
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
        public string AddrLine1 { get; set; }
        public string AddrLine2 { get; set; }
        public string PostalCode { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<System.DateTime> ApprovedDate { get; set; }
        public string ApprovedBy { get; set; }
        public int Status { get; set; }
        public int CourseId { get; set; }
        public int CityId { get; set; }
        public int StateId { get; set; }
        public int CountryId { get; set; }
    
        public virtual City City { get; set; }
        public virtual Country Country { get; set; }
        public virtual Course Course { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PreviousExamInfo> PreviousExamInfoes { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Semester> Semesters { get; set; }
        public virtual State State { get; set; }
    }
}
