using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RssFeed = Radio7.Rss.Feed;

namespace Rss.Api.Data.Services
{
    public class RefreshDataService
    {
        private readonly DatabaseContext _context;

        public RefreshDataService(DatabaseContext context) => _context = context;

        public async Task RefreshAll() => 
            await _context.Feeds.ForEachAsync(async feed => await RefreshAsync(feed.Id));

        public async Task RefreshFolder(Guid id) =>
            await _context.Feeds.Where(x => x.FolderId == id).ForEachAsync(async feed => await RefreshAsync(feed.Id));

        public async Task RefreshFeed(Guid id) => 
            await RefreshAsync(id);

        private async Task RefreshAsync(Guid feedId)
        {
            // the RSS api is not async/awaitable, so we can use TaskCompletionSource to bridge that
            var task = new TaskCompletionSource<int>();
            var feed = _context.Feeds.Include(f => f.Items).Single(f => f.Id == feedId);

            if (feed.LastUpdateDateTime > GetExpiryDate(feed))
            {
                task.SetResult(0);
                return;
            }

            var rssFeed = new RssFeed(new Uri(feed.FeedUrl));

            // async callback
            rssFeed.FeedLoaded += (sender, args) =>
            {
                foreach (var rssItem in rssFeed.Items)
                {
                    // exist?
                    var item = feed.Items.FirstOrDefault(i => i.LinkUrl == rssItem.Id);
                    // update
                    if (item != null)
                    {
                        item.Name = rssItem.Title;
                        item.Raw = rssItem.Raw;
                        item.Content = HtmlCleanerHelper.Clean(rssItem.Raw);
                        item.Snippet = HtmlCleanerHelper.GetSnippet(rssItem.Raw, 200);
                        item.PublishedDateTime = GetPublishedDateTime(rssItem.PublishedDateTime);

                        _context.Entry(item).State = EntityState.Modified;
                        continue;
                    }

                    Debug.WriteLine("found new item");

                    // insert
                    var insertedItem = new Item
                    {
                        Id = Guid.NewGuid(),
                        LinkUrl = rssItem.Id,
                        Name = rssItem.Title,
                        Raw = rssItem.Raw,
                        Content = HtmlCleanerHelper.Clean(rssItem.Raw),
                        Snippet = HtmlCleanerHelper.GetSnippet(rssItem.Raw, 200),
                        PublishedDateTime = GetPublishedDateTime(rssItem.PublishedDateTime),
                        Feed = feed,
                        FeedId = feed.Id
                    };

                    feed.Items.Add(insertedItem);

                    _context.Entry(insertedItem).State = EntityState.Added;
                }

                feed.LastUpdateDateTime = DateTime.UtcNow;

                _context.Entry(feed).State = EntityState.Modified;

                task.SetResult(0);
            };

            // begin async load
            rssFeed.GetItemsFromWeb();

            // must await for SetResult to be called, in that FeedLoaded event callback
            // other wise we may not have the changes to save
            // when we await the TaskCompletionSource task that wraps the event callback, that
            // cause the code to return (with the async task) and the _context is disposed.
            // we need to await *without* prematurely returning.

            // i can't work out how to do that without being synchronous ¯\_(ツ)_/¯
            // but that kinda makes sense right, because I want these actions to 
            // run one after the other... sounds sync to me
            var _ = task.Task.Result;

            await _context.SaveChangesAsync();
        }

        private static DateTime GetPublishedDateTime(string publishedDateTime)
        {
            DateTime.TryParse(publishedDateTime, out var publishedDate);

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
