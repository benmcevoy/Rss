using Rss.Server.Models;
using System;
using System.Linq;
using RssFeed = Rss.Manager.Feed;

namespace Rss.Server.Services
{
    public class FeedService : IFeedService
    {
        private readonly FeedsDbEntities _context;

        public FeedService(FeedsDbEntities context)
        {
            _context = context;
        }

        public void Unsubscribe(Guid id)
        {
            var feed = Get(id);

            _context.Feeds.Remove(feed);

            _context.SaveChanges();
        }

        public void Rename(Guid id, string name)
        {
            var feed = Get(id);

            feed.Name = name;

            _context.SaveChanges();
        }

        public Feed Get(Guid id, ReadOptions readOptions = ReadOptions.Unread)
        {
            var feed = _context.Feeds
                            .Include("Folder")
                            .Include("Items")
                            .Single(f => f.Id == id);

            feed.Items = feed.Items.OrderByDescending(i => i.PublishedDateTime).ToList();

            return feed;
        }

        public void Mark(Guid id, MarkOptions markOptions)
        {
            throw new NotImplementedException();
        }

        public void Refresh()
        {
            foreach (var feed in _context.Feeds)
            {
                var rssFeed = new RssFeed(new Uri(feed.FeedUrl));

                rssFeed.GetItemsFromWeb();

                foreach (var rssItem in rssFeed.Items)
                {
                    // exist?
                    var item = feed.Items.FirstOrDefault(i => i.LinkUrl == rssItem.Id);
                    // update
                    if (item != null)
                    {
                        item.Name = rssItem.Title;
                        item.Raw = rssItem.Raw;
                        item.Content = rssItem.Content;
                        item.Snippet = rssItem.Snippet;
                        //item.FeedId = feed.Id;
                        item.PublishedDateTime = GetPublishedDateTime(rssItem.PublishedDateTime);
                        continue;
                    }

                    // insert
                    feed.Items.Add(new Item
                    {
                        Id = Guid.NewGuid(),
                        LinkUrl = rssItem.Id,
                        Name = rssItem.Title,
                        Raw = rssItem.Raw,
                        Content = rssItem.Content,
                        Snippet = rssItem.Snippet,
                        //FeedId = feed.Id,
                        PublishedDateTime = GetPublishedDateTime(rssItem.PublishedDateTime)
                    });
                }

                feed.LastUpdateDateTime = DateTime.UtcNow;

                _context.SaveChanges();
            }
        }

        private DateTime GetPublishedDateTime(string publishedDateTime)
        {
            var publishedDate = DateTime.UtcNow;

            DateTime.TryParse(publishedDateTime, out publishedDate);

            if (publishedDate.Year < 2000)
            {
                publishedDate = new DateTime(2000, 1, 1);
            }

            return publishedDate;
        }
    }
}