using System.Collections.Generic;

namespace WebSiteManager.Services
{
    public class Paginated<TData>
    {
        public IEnumerable<TData> Data { get; set; }

        public int TotalCount { get; set; }
    }
}
