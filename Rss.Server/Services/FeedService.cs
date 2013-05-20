using Rss.Server.Models;
using System;
using System.Linq;
using System.Data.Entity;
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
            var feed = _context.Feeds.Find(id);

            if (feed == null)
            {
                return null;
            }

            _context.Entry(feed).Reference(f => f.Folder).Load();

            _context.Entry(feed)
                .Collection(f => f.Items)
                .Query()
                // TODO: should respect readOptions
                .Where(i => i.ReadDateTime == null)
                .Load();

            feed.Items = feed.Items.OrderByDescending(i => i.PublishedDateTime).ToList();

            return feed;
        }

        public void Mark(Guid id, MarkOptions markOptions)
        {
            var feed = Get(id);

            foreach (var item in feed.Items)
            {
                item.ReadDateTime = DateTime.Now;
            }

            _context.SaveChanges();
        }

        public void Refresh(Guid id)
        {
            var feed = _context.Feeds
                            .Include("Items")
                            .Single(f => f.Id == id);
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
                    PublishedDateTime = GetPublishedDateTime(rssItem.PublishedDateTime)
                });
            }

            feed.LastUpdateDateTime = DateTime.UtcNow;

            _context.SaveChanges();
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