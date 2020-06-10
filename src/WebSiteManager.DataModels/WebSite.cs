namespace WebSiteManager.DataModels
{
    public class WebSite
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Url { get; set; }

        public bool IsDeleted { get; set; }

        public CategoryId CategoryId { get; set; }

        public Category Category { get; set; }

        public int LoginId { get; set; }

        public Login Login { get; set; }
    }
}
