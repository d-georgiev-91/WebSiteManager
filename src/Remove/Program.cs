using System.Threading.Tasks;
using WebSiteManager.Data;
using WebSiteManager.DataModels;
using WebSiteManager.Services;

namespace Remove
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var dbContext = new WebSiteManagerContext();
            var websiteManagerData = new WebSiteManagerData(dbContext);
            var webSiteService = new WebSiteService(websiteManagerData, null);

            await Delete(webSiteService);
        }

        private static async Task Add(WebSiteService webSiteService)
        {
            await webSiteService.AddAsync(new WebSite
            {
                HomePageSnapshotPath = "somefile.jpg",
                Name = "Google",
                CategoryId = CategoryId.ArtsAndEntertainment,
                Url = "google.com",
                Login = new Login
                {
                    Username = "asdfb@abv.com",
                    Password = "afsagasgsa"
                }
            });
        }

        private static async Task Delete(WebSiteService webSiteService)
        {
            await webSiteService.DeleteAsync(1);
        }
    }
}
