using System.Data;
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
            var feed = _context.Feeds
                .Include("Items")
                .Single(f => f.Id == id);

            _context.Feeds.Remove(feed);

            _context.SaveChanges();
        }

        public void Rename(Guid id, string name)
        {
            var feed = _context.Feeds.Find(id);

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
            var feed = _context.Feeds
                .Include("Items")
                .Single(f => f.Id == id);

            // TOOD: respect markOptions
            foreach (var item in feed.Items)
            {
                item.ReadDateTime = DateTime.Now;
            }

            _context.SaveChanges();
        }

        public void Refresh(Guid id)
        {
            var feed = _context.Feeds.Include(f => f.Items).Single(f => f.Id == id);

            if (feed.LastUpdateDateTime > GetExpiryDate(feed))
                return;

            var rssFeed = new RssFeed(new Uri(feed.FeedUrl));

            rssFeed.GetItemsFromWeb();

            //var itemQuery = _context.Entry(feed).Collection(f => f.Items).Query();
            //feed.Items = itemQuery.OrderByDescending(i => i.PublishedDateTime).Take(100).ToList();

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

        private static DateTime GetPublishedDateTime(string publishedDateTime)
        {
            var publishedDate = DateTime.UtcNow;

            DateTime.TryParse(publishedDateTime, out publishedDate);

            if (publishedDate.Year < 2000)
            {
                publishedDate = new DateTime(2000, 1, 1);
            }

            return publishedDate;
        }

        private static DateTime GetExpiryDate(Feed feed)
        {
            if (feed.UpdatePeriod.ToLowerInvariant() == "hourly")
            {
                return DateTime.UtcNow.AddHours(-feed.UpdateFrequency);
            }

            if (feed.UpdatePeriod.ToLowerInvariant() == "daily")
            {
                return DateTime.UtcNow.AddDays(-feed.UpdateFrequency);
            }

            return feed.UpdatePeriod.ToLowerInvariant() == "weekly" ?
                DateTime.UtcNow.AddDays(-7 * feed.UpdateFrequency) :
                DateTime.UtcNow.AddDays(-1); //default
        }
    }
}