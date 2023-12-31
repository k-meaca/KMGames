using System.Data.Entity;
using System.Data.Entity.Hierarchy;
using System.Web.Hosting;
using KMGames.Data.EntityTypeConfiguration;
using KMGames.Entities.Entities;
using Microsoft.AspNet.Identity.EntityFramework;

namespace KMGames.Web.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("KmGamesDBContext", throwIfV1Schema: false)
        {
        }

        static ApplicationDbContext()
        {
            // Set the database initializer which is run once during application start
            // This seeds the database with admin user credentials and admin role
            Database.SetInitializer<ApplicationDbContext>(new ApplicationDbInitializer());
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

    }
}