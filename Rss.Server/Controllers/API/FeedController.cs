using Rss.Server.Filters;
using Rss.Server.Models;
using Rss.Server.Services;
using System;
using System.Web.Http;

namespace Rss.Server.Controllers.API
{
    public class FeedController : ApiController
    {
        private readonly IFeedService _feedService;

        public FeedController(IFeedService feedService)
        {
            _feedService = feedService;
        }

        /// <summary>
        /// get feed by {id} and unread items
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ApiCache(60)]
        public Feed Get(Guid id)
        {
            return _feedService.Get(id, ReadOptions.Unread);
        }

        /// <summary>
        /// get feed by {id} and first 100 items, read or unread
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Feed All(Guid id)
        {
            return _feedService.Get(id, ReadOptions.All);
        }

        /// <summary>
        /// unsubscribe from this feed
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public void Unsubscribe(Guid id)
        {
            _feedService.Unsubscribe(id);
        }

        /// <summary>
        /// rename the feed
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        public void Rename(Guid id, string name)
        {
            _feedService.Rename(id, name);
        }

        /// <summary>
        /// mark all items in this feed as read
        /// </summary>
        /// <param name="id"></param>
        public void Mark(Guid id)
        {
            _feedService.Mark(id, MarkOptions.All);
        }

        /// <summary>
        /// Move this feed into the folder
        /// </summary>
        /// <param name="id"></param>
        /// <param name="folderId"></param>
        public void MoveToFolder(Guid id, Guid folderId)
        {
            throw new NotImplementedException();
        }


    }
}
