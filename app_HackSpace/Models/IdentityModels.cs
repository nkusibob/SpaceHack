using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace app_HackSpace.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Notez qu'authenticationType doit correspondre à l'élément défini dans CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Ajouter les revendications personnalisées de l’utilisateur ici
            return userIdentity;
        }
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

        //-----ADDED>>>>>-------------------------------------------------------------------------------------------
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // redéfinir le schéma
            modelBuilder.HasDefaultSchema("user");
            // et le nom des tables si besoin
            //modelBuilder.Entity<ApplicationUser>().ToTable("UserTableName");
            //modelBuilder.Entity<IdentityRole>().ToTable("RoleTableName");
            //modelBuilder.Entity<IdentityUserRole>().ToTable("UserRoleTableName");
            //modelBuilder.Entity<IdentityUserClaim>().ToTable("UserClaimTableName");
            //modelBuilder.Entity<IdentityUserLogin>().ToTable("UserLoginTableName");
        }
        //-----<<<<<ADDED-------------------------------------------------------------------------------------------

    }
}