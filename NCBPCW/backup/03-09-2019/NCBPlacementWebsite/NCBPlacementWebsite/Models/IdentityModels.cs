using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;

namespace NCBPlacementWebsite.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        [Display(Name = "Full Name"), Required]
        [StringLength(50)]
        public string FullName { get; set; }


        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationRole : IdentityRole
    {
        public ApplicationRole() : base() {}

        public ApplicationRole(string roleName) : base(roleName){}

    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<StudentProfile> StudentProfiles { get; set; }


        public DbSet<ContactList> ContactLists { get; set; }



        public DbSet<Course> Courses { get; set; }

        public DbSet<Subjects> Subject { get; set; }

        public DbSet<Lecturers> Lecturer { get; set; }

        public DbSet<Department> Departments{ get; set; }


        public DbSet<CurriculumFeedbacks> CurriculumFeedback { get; set; }


        public DbSet<AppraisalFeedbacks> AppraisalFeedback { get; set; }
    }
}