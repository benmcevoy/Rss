using System;
using System.Linq;
using System.Threading.Tasks;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rss.Api.Data;
using Rss.Api.Data.Services;
using Rss.Api.V2.Model;

namespace Rss.Api.V2
{
    [ApiController, ApiVersion("2")]
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
        public async Task Subscribe(SubscribePostModel postModel)
        {
            await _feedDataService.Add(new Uri(postModel.FeedUrl), postModel.FolderId);
        }

        [HttpPost, Route("Unsubscribe")]
        public async Task Unsubscribe(Guid id)
        {
            var feed = _context.Feeds.Single(f => f.Id == id);

            _context.Feeds.Remove(feed);

            await _context.SaveChangesAsync();
        }
    }
}
