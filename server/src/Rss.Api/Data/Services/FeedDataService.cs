using System;
using System.Threading.Tasks;

namespace Rss.Api.Data.Services
{
    public class FeedDataService
    {
        private readonly DatabaseContext _context;

        public FeedDataService(DatabaseContext context) => _context = context;

        public async Task<Guid> Add(Uri feedUrl, Guid? folderId)
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

            await rssFeed.GetItemsFromWebAsync(includeFavIcon: true);

            feed.Name = rssFeed.Title ?? "can't find title";
            feed.HtmlUrl = rssFeed.HtmlUri.ToString();
            feed.FavIcon = rssFeed.FavIcon;

            _context.Feeds.Add(feed);
            _context.SaveChanges();

            return feed.Id;
        }
    }
}
