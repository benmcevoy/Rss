using Rss.Server.Services;
using System;
using System.Web.Http;

namespace Rss.Server.Controllers.API
{
    public class ItemController : ApiController
    {
        private readonly IItemService _itemService;

        public ItemController(IItemService itemService)
        {
            _itemService = itemService;
        }

        /// <summary>
        /// set item as read
        /// </summary>
        /// <param name="id"></param>
        [HttpPost]
        public void Read([FromBody]Guid id)
        {
            _itemService.Read(id);
        }

        [HttpPost]
        public void Unread([FromBody]Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
