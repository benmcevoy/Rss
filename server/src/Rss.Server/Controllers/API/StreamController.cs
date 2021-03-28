using Rss.Server.Models;
using System.Collections.Generic;
using System.Web.Http.Cors;
using Rss.Server.Services;

namespace Rss.Server.Controllers.API
{
    [EnableCors(origins: "http://rss.local", headers: "*", methods: "*")]
    public class StreamController : DbContextApiController
    {
        private readonly IItemService _itemService;

        public StreamController(IItemService itemService, FeedsDbEntities context)
            : base(context)
        {
            _itemService = itemService;
        }

        /// <summary>
        /// get first 100 unread items from all feeds
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Item> Get()
        {
            return _itemService.Get(12);
        }
    }
}
