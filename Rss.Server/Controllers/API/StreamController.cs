using Rss.Server.Models;
using System;
using System.Collections.Generic;
using System.Web.Http;
using Rss.Server.Services;

namespace Rss.Server.Controllers.API
{
    public class StreamController : ApiController
    {
        private readonly IItemService _itemService;

        public StreamController(IItemService itemService)
        {
            _itemService = itemService;
        }

        /// <summary>
        /// get first 100 unread items from all feeds
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Item> Get()
        {
            throw new NotImplementedException();
        }
    }
}
