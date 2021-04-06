using System.Collections.Generic;

namespace Rss.Api.V1.Model
{
    public class Subscription
    {
        public string Name { get; set; } = "Subscription";
        public IList<Folder> Folders { get; set; } = new List<Folder>();
        public IList<Feed> Feeds { get; set; } = new List<Feed>();
    }
}
