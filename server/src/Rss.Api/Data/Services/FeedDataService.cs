using System;
using System.Threading;

namespace Rss.Api.Data.Services
{
    public class FeedDataService
    {
        private readonly DatabaseContext _context;
        private static readonly ManualResetEvent RefreshResetEvent = new ManualResetEvent(false);

        public FeedDataService(DatabaseContext context) => _context = context;

        public Guid Add(Uri feedUrl, Guid? folderId)
        {
            var feed = new Feed
            {
                Id = Guid.NewGuid(),
                FeedUrl = feedUrl.ToString(),
                FolderId = folderId,
                UpdateFrequency = 12,
                UpdatePeriod = "Hourly"
            };

            var rssFeed = new Radio7.Rss.Feed(feedUrl);

            RefreshResetEvent.Reset();

            rssFeed.FeedLoaded += (sender, args) =>
            {
                feed.Name = rssFeed.Title ?? "can't find title";
                feed.HtmlUrl = rssFeed.HtmlUri.ToString();

                _context.Feeds.Add(feed);

                RefreshResetEvent.Set();
            };

            rssFeed.GetItemsFromWeb();

            // TODO: consider async/await, but it's abit tricky without modifying the interface signature
            RefreshResetEvent.WaitOne(TimeSpan.FromSeconds(20));

            _context.Feeds.Add(feed);
            _context.SaveChanges();

            return feed.Id;
        }
    }
}
