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
    
    public partial class Entitlement
    {
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public int PermissionId { get; set; }
        public int StaffId { get; set; }
    
        public virtual Permission Permission { get; set; }
        public virtual Staff Staff { get; set; }
    }
}