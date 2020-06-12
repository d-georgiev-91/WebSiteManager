using System.Linq;
using System.Threading.Tasks;
using WebSiteManager.Data;
using WebSiteManager.DataModels;

namespace WebSiteManager.Services
{
    /// <inheritdoc cref="IWebSiteService"/>
    public class WebSiteService : IWebSiteService
    {
        private const string NoSuchWebsiteWithIdErrorMessage = "No such website with id {0}";
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

        public async Task UpdateAsync(WebSite webSite)
        {
            //TODO: Store image and crypt password
            _webSiteManagerData.WebSiteRepository.Update(webSite);
            await _webSiteManagerData.SaveChangesAsync();
        }

        private WebSite GetWebSiteById(int webSiteId) => _webSiteManagerData.WebSiteRepository.Get(w => w.Id == webSiteId).FirstOrDefault();


        /// <inheritdoc cref="IWebSiteService.DeleteAsync" />
        public async Task<ServiceResult> DeleteAsync(int webSiteId)
        {
            var webSite = GetWebSiteById(webSiteId);
            var serviceResult = new ServiceResult();

            if (webSite == null)
            {
                serviceResult.AddError(ErrorType.NotFound, NoSuchWebsiteWithIdErrorMessage);
                return serviceResult;
            }

            webSite.IsDeleted = true;
            await _webSiteManagerData.SaveChangesAsync();

            return serviceResult;
        }
    }
}