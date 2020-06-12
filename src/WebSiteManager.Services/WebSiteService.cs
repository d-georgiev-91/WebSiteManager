using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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

        public ServiceResult<Paginated<WebSite>> GetAll(Page page, Sorting sorting)
        {
            var serviceResult = new ServiceResult<Paginated<WebSite>>();

            var webSites = _webSiteManagerData.WebSiteRepository.Get();

            var webSitesCount = webSites.Count();

            if (sorting != null && sorting.Columns.Any())
            {
                webSites = webSites.OrderBy(string.Join(",", sorting.Columns.Select(c => c.Key + " " + c.Value)));
            }

            if (page != null)
            {
                webSites = webSites
                    .Take(page.Size)
                    .Skip(page.Index * page.Size);
            }

            serviceResult.Data = new Paginated<WebSite>
            {
                Data = webSites.Include(w => w.Login),
                TotalCount = webSitesCount
            };

            return serviceResult;
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