using System.Threading.Tasks;
using WebSiteManager.DataModels;

namespace WebSiteManager.Services
{
    public interface IWebSiteService
    {
        Task AddAsync(WebSite webSite);

        Task DeleteAsync(int webSiteId);
    }
}
