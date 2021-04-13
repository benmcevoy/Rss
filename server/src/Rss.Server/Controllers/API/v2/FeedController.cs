using System;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using Rss.Server.Models;
using Rss.Server.PostModel;
using Rss.Server.Services;

namespace Rss.Server.Controllers.API.v2
{
    [EnableCors(origins: "http://rss.local", headers: "*", methods: "*")]
    public class V2_FeedController : DbContextApiController
    {
        private readonly IFeedService _feedService;

        public V2_FeedController(IFeedService feedService, FeedsDbEntities context)
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
        public object Get(Guid id)
        {
            var feeds = _feedService.Get(id, ReadOptions.Unread);
            return new
            {
                Name = feeds.Name,
                Feeds = new[]
                {
                    feeds
                }
            };
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
