using System.Linq;
using System.Threading.Tasks;
using WebSiteManager.Data;
using WebSiteManager.DataModels;
using WebSiteManager.Services.Security;

namespace WebSiteManager.Services
{
    /// <inheritdoc cref="IWebSiteService"/>
    public class WebSiteService : IWebSiteService
    {
        private const string NoSuchWebsiteWithIdErrorMessage = "No such website with id {0}";

        private readonly IWebSiteManagerData _webSiteManagerData;
        private readonly IPasswordEncryptor _passwordEncryptor;

        public WebSiteService(IWebSiteManagerData webSiteManagerData,
            IPasswordEncryptor passwordEncryptor)
        {
            _webSiteManagerData = webSiteManagerData;
            _passwordEncryptor = passwordEncryptor;
        }

        public async Task AddAsync(WebSite webSite)
        {
            webSite.Login.Password = _passwordEncryptor.Encrypt(webSite.Login.Password);

            _webSiteManagerData.WebSiteRepository.Add(webSite);
            await _webSiteManagerData.SaveChangesAsync();
        }

        public async Task UpdateAsync(WebSite webSite)
        {
            if (webSite.Login?.Password != null)
            {
                webSite.Login.Password = _passwordEncryptor.Encrypt(webSite.Login.Password);
            }

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
                serviceResult.AddError(ErrorType.NotFound, string.Format(NoSuchWebsiteWithIdErrorMessage, webSiteId));
                return serviceResult;
            }

            webSite.IsDeleted = true;
            await _webSiteManagerData.SaveChangesAsync();

            return serviceResult;
        }
    }
}