using Rss.Server.Models;
using Rss.Server.Services;
using System.Diagnostics;

namespace Rss.Server.Controllers.API
{
    public class RefreshController : DbContextApiController
    {
        private readonly IRefreshService _feedService;

        public RefreshController(IRefreshService refreshService, FeedsDbEntities context)
            : base(context)
        {
            _feedService = refreshService;
        }

        public string Get()
        {
            var sw = new Stopwatch();

            sw.Start();
            
            _feedService.RefreshAllFeeds();

            Context.SaveChanges();

            sw.Stop();

            return "Refresh complete " + sw.ElapsedMilliseconds;
        }
    }
}
