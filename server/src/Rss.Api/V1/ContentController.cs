using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Rss.Api.Data;
using Rss.Api.Data.Services;
using Rss.Api.V1.Model;

namespace Rss.Api.V1
{
    [ApiController, ApiVersion("1")]
    [Route("v{version:apiVersion}/content")]
    public class ContentController
    {
        private readonly DatabaseContext _databaseContext;
        private readonly RefreshDataService _refreshDataService;
        private readonly MarkAsReadDataService _markAsReadDataService;

        public ContentController(DatabaseContext databaseContext, RefreshDataService refreshDataService,
            MarkAsReadDataService markAsReadDataService)
        {
            _databaseContext = databaseContext;
            _refreshDataService = refreshDataService;
            _markAsReadDataService = markAsReadDataService;
        }

        [HttpGet, Route("Stream")]
        public RssItemList Stream()
        {
            var stream = _databaseContext
                .Items
                .Include(x => x.Feed)
                .ThenInclude(x => x.Folder)
                .Where(item => item.ReadDateTime == null)
                .OrderByDescending(item => item.PublishedDateTime);

            return new RssItemList
            {
                Name = "Stream",
                Type = "Stream",
                RssItems = stream.Select(Mapper.Map).ToList()
            };
        }

        [HttpGet, Route("Folder/{id}")]
        public RssItemList Folder(Guid id)
        {
            var folder = _databaseContext
                .Folders
                .Include(x => x.Feeds)
                .ThenInclude(x => x.Items.Where(item => item.ReadDateTime == null))
                .Single(f => f.Id == id);

            return new RssItemList
            {
                Id = folder.Id,
                Name = folder.Name,
                Type = "Folder",
                RssItems = folder.Feeds
                    .SelectMany(f => f.Items)
                    .OrderByDescending(item => item.PublishedDateTime)
                    .Select(Mapper.Map)
                    .ToList()
            };
        }

        [HttpGet, Route("Feed/{id}")]
        public RssItemList Feed(Guid id)
        {
            var feed = _databaseContext
                .Feeds
                .Include(x => x.Items.Where(item => item.ReadDateTime == null))
                .Single(f => f.Id == id);

            return new RssItemList
            {
                Id = feed.Id,
                Name = feed.Name,
                Type = "Feed",
                RssItems = feed.Items
                    .Select(Mapper.Map)
                    .OrderByDescending(item => item.PublishedDateTime)
                    .ToList()
            };
        }

        [HttpPost, Route("Read")]
        public RssItem Read(Guid id)
        {
            var item = _databaseContext.Items
                .Include(x => x.Feed)
                .ThenInclude(x => x.Folder)
                .Single(x => x.Id == id);

            // mark as read
            item.ReadDateTime = DateTime.UtcNow;

            _databaseContext.SaveChanges();

            return Mapper.Map(item);
        }

        [HttpPost, Route("Refresh")]
        public Task Refresh(string type, Guid? id) =>
            type.ToLowerInvariant() switch
            {
                "stream" => _refreshDataService.RefreshAll(),
                "folder" => _refreshDataService.RefreshFolder(id.GetValueOrDefault()),
                "feed" => _refreshDataService.RefreshFeed(id.GetValueOrDefault()),
                _ => throw new NotSupportedException("refresh type or id is not supported")
            };

        [HttpPost, Route("MarkAsRead")]
        public Task MarkAsRead(string type, Guid? id) =>
            type.ToLowerInvariant() switch
            {
                "stream" => _markAsReadDataService.MarkAsReadAll(),
                "folder" => _markAsReadDataService.MarkAsReadFolder(id.GetValueOrDefault()),
                "feed" => _markAsReadDataService.MarkAsReadFeed(id.GetValueOrDefault()),
                _ => throw new NotSupportedException("MarkAsRead type or id is not supported")
            };
    }
}
