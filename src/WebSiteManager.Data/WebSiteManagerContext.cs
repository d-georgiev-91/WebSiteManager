using Microsoft.EntityFrameworkCore;
using WebSiteManager.DataModels;

namespace WebSiteManager.Data
{
    public class WebSiteManagerContext : DbContext
    {
        public DbSet<Login> Logins { get; set; }

        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
