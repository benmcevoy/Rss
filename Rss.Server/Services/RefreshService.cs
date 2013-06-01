using System;
using System.Linq;
using Rss.Server.Models;

namespace Rss.Server.Services
{
    public class RefreshService : IRefreshService
    {
        private readonly IFeedService _feedService;
        private readonly FeedsDbEntities _context;

        public RefreshService(IFeedService feedService, FeedsDbEntities context)
        {
            _feedService = feedService;
            _context = context;
        }

        void IRefreshService.RefreshAllFeeds()
        {
            foreach (var feed in _context.Feeds.Where(
                f =>
                f.LastUpdateDateTime < GetExpiryDate(f)
                ))
            {
                _feedService.Refresh(feed.Id);
            }
        }

        private static DateTime GetExpiryDate(Feed feed)
        {
            if (feed.UpdatePeriod.ToLowerInvariant() == "hourly")
            {
                return DateTime.Now.AddHours(-feed.UpdateFrequency);
            }

            if (feed.UpdatePeriod.ToLowerInvariant() == "daily")
            {
                return DateTime.Now.AddDays(-feed.UpdateFrequency);
            }

            if (feed.UpdatePeriod.ToLowerInvariant() == "weekly")
            {
                return DateTime.Now.AddDays(-7 * feed.UpdateFrequency);
            }

            // default
            return DateTime.Now.AddDays(-1);
        }
    }
}