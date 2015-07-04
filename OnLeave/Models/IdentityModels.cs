using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace OnLeave.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class User : IdentityUser
    {
        [Required(ErrorMessage="First name required")]
        [StringLength(50, ErrorMessage = "First name exceed 50 charactes")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name required")]
        [StringLength(50, ErrorMessage = "Last name exceed 50 charactes")]
        public string LastName { get; set; }

        [StringLength(50, ErrorMessage="Telephone exceed 50 charactes")]
        public string Telephone { get; set; }

        [EmailAddress(ErrorMessage="email address is not valid")]
        public string Email { get; set; }
    }

    public class UserDbContext : IdentityDbContext<User>
    {
        public UserDbContext()
            : base("UserContext")
        {            
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Change the name of the table to be Users instead of AspNetUsers
            modelBuilder.Entity<IdentityUser>()
                .ToTable("Users");
            modelBuilder.Entity<User>()
                .ToTable("Users");
        }
    }
}