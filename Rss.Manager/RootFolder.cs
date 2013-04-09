using System.Collections.Generic;

namespace Rss.Manager
{
    public class RootFolder
    {
        private readonly List<Feed> _feeds;
        private readonly List<Folder> _folders;

        public RootFolder()
        {
            Name = "home";
            _feeds = new List<Feed>();
            _folders = new List<Folder>();
        }
        
        public string Name { get; private set; }

        public IEnumerable<Folder> Folders
        {
            get { return _folders; }
        }

        public IEnumerable<Feed> Feeds
        {
            get { return _feeds; }
        }

        public void AddFeed(Feed feed)
        {
            _feeds.Add(feed);
        }

        public void AddFolder(Folder folder)
        {
            _folders.Add(folder);
        }
    }
}
