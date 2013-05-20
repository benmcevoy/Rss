using Rss.Server.Services;
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
            _feedService.RefreshAllFeeds();
            return "Refresh complete";
        }
    }
}
