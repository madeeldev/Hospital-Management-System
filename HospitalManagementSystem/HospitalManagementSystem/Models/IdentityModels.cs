using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace HospitalManagementSystem.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string UserRole { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Patients> Patients { get; set; }
        public DbSet<AssignedDoctorsAndPatients> AssignedDoctorsAndPatients { get; set; }
        public DbSet<Doctors> Doctors { get; set; }
        public DbSet<Departments> Departments { get; set; }
        public DbSet<MedicineRecord> MedicineRecords { get; set; }
        public DbSet<Attendence> Attendences { get; set; }
        public DbSet<Ambulance> Ambulances { get; set; }
        public DbSet<Prescriptions> Prescriptions { get; set; }
        public DbSet<Appointments> Appointments { get; set; }



        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}