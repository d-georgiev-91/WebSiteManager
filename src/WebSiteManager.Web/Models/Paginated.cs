using System.Collections.Generic;

namespace WebSiteManager.Web.Models
{
    public class Paginated<TData>
    {
        public IEnumerable<TData> Data { get; set; }

        public int TotalCount { get; set; }
    }
}
