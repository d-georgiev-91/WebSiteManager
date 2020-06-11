using System.Linq;
using System.Threading.Tasks;
using WebSiteManager.Data;
using WebSiteManager.DataModels;

namespace WebSiteManager.Services
{
    public class WebSiteService : IWebSiteService
    {
        private readonly IWebSiteManagerData _webSiteManagerData;

        public WebSiteService(IWebSiteManagerData webSiteManagerData)
        {
            _webSiteManagerData = webSiteManagerData;
        }

        public async Task AddAsync(WebSite webSite)
        {
            //TODO: Store image and crypt password
            _webSiteManagerData.WebSiteRepository.Add(webSite);
            await _webSiteManagerData.SaveChangesAsync();
        }

        public async Task DeleteAsync(int webSiteId)
        {
            var webSite = _webSiteManagerData.WebSiteRepository.Get(p=> p.Id == webSiteId).FirstOrDefault();
            webSite.IsDeleted = false;
            await _webSiteManagerData.SaveChangesAsync();
        }
    }
}