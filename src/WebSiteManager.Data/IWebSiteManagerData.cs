using System.Threading.Tasks;
using WebSiteManager.Data.Repositories;
using WebSiteManager.DataModels;

namespace WebSiteManager.Data
{
    public interface IWebSiteManagerData
    {
        IRepository<WebSite> WebSiteRepository { get; }

        IRepository<Login> LoginRepository { get; }

        Task<int> SaveChangesAsync();
    }
}
