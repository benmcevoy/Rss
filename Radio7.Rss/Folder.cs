using System.Collections.Generic;
using System.Linq;

namespace Radio7.Rss
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
