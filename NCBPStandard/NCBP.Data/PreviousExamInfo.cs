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
    
    public partial class PreviousExamInfo
    {
        public int Id { get; set; }
        public System.Guid StudentId { get; set; }
        public string PreviousCourseName { get; set; }
        public string PreviousInstitution { get; set; }
        public System.DateTime MonthAndYearOfPassing { get; set; }
        public string Result { get; set; }
        public int StateId { get; set; }
        public int CountryId { get; set; }
    
        public virtual Country Country { get; set; }
        public virtual State State { get; set; }
        public virtual StudentProfile StudentProfile { get; set; }
    }
}
