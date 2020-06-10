using Microsoft.EntityFrameworkCore;
using WebSiteManager.DataModels;

namespace WebSiteManager.Data
{
    public class WebSiteManagerContext : DbContext
    {
        public DbSet<Login> Logins { get; set; }
    }
}
