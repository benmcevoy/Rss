using Rss.Server.Models;
using System.Collections.Generic;

namespace Rss.Server.Models
{
    public class RootFolder
    {
        public RootFolder()
        {
            Name = "home";
            Folders = new List<Folder>();
            Feeds = new List<Feed>();
        }

        public string Name { get; set; }

        public IEnumerable<Folder> Folders { get; set; }

        public IEnumerable<Feed> Feeds { get; set; }
    }
}