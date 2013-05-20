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
            foreach (var feed in _context.Feeds)
            {
                _feedService.Refresh(feed.Id);
            }
        }
    }
}