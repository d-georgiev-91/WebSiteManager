using System.Collections.Generic;

namespace WebSiteManager.Services
{
    public class Sorting
    {
        public Sorting()
        {
            Columns = new Dictionary<string, SortingDirection>();
        }

        public IDictionary<string, SortingDirection> Columns { get; set; }
    }
}