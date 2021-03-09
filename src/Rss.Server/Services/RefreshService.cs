using Rss.Server.Models;
using WebGrease.Css.Extensions;

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

        public void RefreshAllFeeds()
        {
            _context.Feeds.ForEach(feed => _feedService.Refresh(feed.Id));
        }
    }
}