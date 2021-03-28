using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Cors;
using Rss.Server.Models;
using Rss.Server.Services;

namespace Rss.Server.Controllers.API.v2
{
    // TODO: filthy versioning
    [EnableCors(origins: "http://rss.local", headers: "*", methods: "*")]
    public class V2_StreamController : DbContextApiController
    {
        private readonly IItemService _itemService;

        public V2_StreamController(IItemService itemService, FeedsDbEntities context)
            : base(context)
        {
            _itemService = itemService;
        }

        /// <summary>
        /// get first 100 unread items from all feeds
        /// </summary>
        /// <returns></returns>
        public object Get()
        {
            var items =  _itemService.Get(12);

            return new
            {
                Name = "Stream",
                Feeds = new[]
                {
                    new
                    {
                        Name = "Stream",
                        Items = items
                    }
                }
            };
        }
    }
}
