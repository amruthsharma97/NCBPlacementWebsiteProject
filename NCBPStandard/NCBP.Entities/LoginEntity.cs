using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace NCBP.Entities
{
    public class LoginEntity
    {
        public int Id { get; set; }
        public int StaffId { get; set; }
        public string Name { get; set; }
        [Required]
        [Display(Name = "User Name")]
        public string UserName { get; set; }
        [Required]
        [Display(Name = "Password")]
        public string Passcode { get; set; }
        [Required]
        public bool IsFirstLogin { get; set; }
        public bool IsActive { get; set; }
        public bool IsAdmin { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime? UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }

        ///Not in dataBase, using just for model-entity purpose
        [Display(Name = "New Password")]
        public string NewPasscode { get; set; }
        [Required]
        [Display(Name = "Confirm New Password")]
        public string ConfirmPasscode { get; set; }
    }
}
