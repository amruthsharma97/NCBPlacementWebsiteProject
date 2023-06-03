using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCBP.Entities
{
    public class EntitlementEntity
    {
        public int Id { get; set; }
        public int StaffId { get; set; }
        public int PermissionId { get; set; }
        public bool IsActive { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime? UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
    }
}
