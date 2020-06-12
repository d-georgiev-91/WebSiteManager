using System.Threading.Tasks;
using WebSiteManager.DataModels;

namespace WebSiteManager.Services
{
    /// <summary>
    /// Crud operations for WebSites
    /// </summary>
    public interface IWebSiteService
    {
        Task AddAsync(WebSite webSite);

        Task UpdateAsync(WebSite webSite);

        /// <summary>
        /// Soft deletes website
        /// </summary>
        /// <param name="webSiteId">The of the website</param>
        /// <returns>Service result. If no such website exists service result will have error of type <see cref="ErrorType"/></returns>
        Task<ServiceResult> DeleteAsync(int webSiteId);
    }
}
