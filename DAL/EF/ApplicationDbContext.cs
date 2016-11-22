using BEP.BL.Models.Gebruikers;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace BEP.DAL
{
  public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
  {
    public ApplicationDbContext()
        : base("BEPDB-EF", throwIfV1Schema: false)
    {
      Database.SetInitializer(
  new MigrateDatabaseToLatestVersion<ApplicationDbContext, Migrations.ApplicationDbContext.Configuration>("BEPDB-EF")
  );
      this.Configuration.ProxyCreationEnabled = false;
    }

    public static ApplicationDbContext Create()
    {
      return new ApplicationDbContext();
    }
  }
}
