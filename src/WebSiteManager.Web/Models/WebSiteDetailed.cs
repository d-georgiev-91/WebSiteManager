using WebSiteManager.DataModels;

namespace WebSiteManager.Web.Models
{
    public class WebSiteDetailed
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Url { get; set; }

        public string HomePageSnapshotPath { get; set; }

        public string Category { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
    }
}
