using Rss.Server.Services;
using System.Web.Http;
using System.Threading;

namespace Rss.Server.Controllers.API
{
    public class RefreshController : ApiController
    {
        private readonly IFeedService _feedService;

        public RefreshController(IFeedService feedService)
        {
            _feedService = feedService;
        }

        public string Get()
        {
            _feedService.Refresh();
            return "Refresh complete";
        }
    }
}
