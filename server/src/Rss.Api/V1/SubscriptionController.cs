using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rss.Api.Data;
using Rss.Api.Data.Services;
using Rss.Api.V1.Model;
using Feed = Rss.Api.V1.Model.Feed;

namespace Rss.Api.V1
{
    [ApiController, ApiVersion("1")]
    [Route("v{version:apiVersion}/subscription")]
    public class SubscriptionController
    {
        private readonly DatabaseContext _context;
        private readonly FeedDataService _feedDataService;

        public SubscriptionController(DatabaseContext context, FeedDataService feedDataService)
        {
            _context = context;
            _feedDataService = feedDataService;
        }

        [HttpGet]
        public Subscription Get()
        {
            var folders = _context.Folders
                .Include(x => x.Feeds)
                .ThenInclude(x => x.Items.Where(item => item.ReadDateTime == null));

            return new Subscription
            {
                Folders = folders.Select(Mapper.Map).ToList()
            };
        }

        [HttpPost, Route("Subscribe")]
        public Task Subscribe(SubscribePostModel postModel)
        {
            _feedDataService.Add(new Uri(postModel.FeedUrl), postModel.FolderId);

            return Task.CompletedTask;
        }

        [HttpPost, Route("Unsubscribe")]
        public Task Unsubscribe(Guid id)
        {
            var feed = _context.Feeds.Single(f => f.Id == id);

            _context.Feeds.Remove(feed);

            return _context.SaveChangesAsync();
        }
    }
}
