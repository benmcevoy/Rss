using System.Collections.Generic;
using System.Linq;

namespace Rss.Manager
{
    public class Folder
    {
        public Folder()
        {
            Feeds = Enumerable.Empty<Feed>();
        }

        public string Name { get; set; }

        public IEnumerable<Feed> Feeds { get; set; }
    }
}
