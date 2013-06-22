using Rss.Server.Services;
using System.Diagnostics;
using System.Web.Http;

namespace Rss.Server.Controllers.API
{
    public class RefreshController : ApiController
    {
        private readonly IRefreshService _feedService;

        public RefreshController(IRefreshService refreshService)
        {
            _feedService = refreshService;
        }

        public string Get()
        {
            var sw = new Stopwatch();

            sw.Start();
            
            _feedService.RefreshAllFeeds();
            
            sw.Stop();

            return "Refresh complete " + sw.ElapsedMilliseconds;
        }
    }
}
