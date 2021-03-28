using System.Collections.Generic;

namespace Radio7.Rss
{
    public class RssManager : IRssManager
    {
        private readonly List<Feed> _feeds;
        
        public RssManager()
        {
            _feeds = new List<Feed>();
        }

        public void Subscribe(Feed feed)
        {
            _feeds.Add(feed);
        }

        public void Unsubscribe(Feed feed)
        {
            _feeds.Remove(feed);
        }

        public IEnumerable<Feed> Feeds
        {
            get { return _feeds; }
        }
    }
}