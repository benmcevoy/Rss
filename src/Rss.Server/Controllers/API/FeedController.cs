using Rss.Server.Filters;
using Rss.Server.Models;
using Rss.Server.PostModel;
using Rss.Server.Services;
using System;
using System.Threading.Tasks;
using System.Web.Http;

namespace Rss.Server.Controllers.API
{
    public class FeedController : DbContextApiController
    {
        private readonly IFeedService _feedService;

        public FeedController(IFeedService feedService, FeedsDbEntities context)
            : base(context)
        {
            _feedService = feedService;
        }

        /// <summary>
        /// get feed by {id} and unread items
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // [ApiCache(60)]
        [HttpGet]
        public Feed Get(Guid id)
        {
            return _feedService.Get(id, ReadOptions.Unread);
        }

        /// <summary>
        /// get feed by {id} and first 100 items, read or unread
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public Feed All(Guid id)
        {
            return _feedService.Get(id, ReadOptions.All);
        }

        /// <summary>
        /// unsubscribe from this feed
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public void Unsubscribe([FromBody]Guid id)
        {
            _feedService.Unsubscribe(id);
            Context.SaveChanges();
        }

        /// <summary>
        /// rename the feed
        /// </summary>
        [HttpPost]
        public void Rename(RenameDto renameDto)
        {
            _feedService.Rename(renameDto.Id, renameDto.Name);
            Context.SaveChanges();
        }

        /// <summary>
        /// mark all items in this feed as read
        /// </summary>
        /// <param name="id"></param>
        [HttpPost]
        public void Mark([FromBody]Guid id)
        {
            _feedService.Mark(id, MarkOptions.All);
            Context.SaveChanges();
        }

        /// <summary>
        /// Move this feed into the folder
        /// </summary>
        [HttpPost]
        public void MoveToFolder(MoveFeedToFolderDto moveFeedToFolderDto)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public async Task Refresh([FromBody] Guid id)
        {
            await _feedService.Refresh(id, true);
            Context.SaveChanges();
        }
    }
}
