using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Rss.Api.Data;
using Rss.Api.V1.Model;

namespace Rss.Api.V1
{
    [ApiController, ApiVersion("1")]
    [Route("v{version:apiVersion}/content")]
    public class ContentController
    {
        private readonly DatabaseContext _databaseContext;

        public ContentController(DatabaseContext databaseContext) => _databaseContext = databaseContext;

        [HttpGet, Route("Stream")]
        public RssItemList Stream()
        {
            // TODO: needs some more predicate to exclude read items
            // OrderBy published desc

            var stream = _databaseContext
                .Items
                .Include(x => x.Feed)
                .ThenInclude(x => x.Folder);

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

            // TODO: needs some more predicate to exclude read items
            // OrderBy published desc

            return new RssItemList
            {
                Id = folder.Id,
                Name = folder.Name,
                Type = "Folder",
                RssItems = folder.Feeds.SelectMany(f => f.Items).Select(Mapper.Map).ToList()
            };
        }

        [HttpGet, Route("Feed/{id}")]
        public RssItemList Feed(Guid id)
        {
            var feed = _databaseContext
                .Feeds
                .Include(x => x.Items)
                .Single(f => f.Id == id);

            // TODO: needs some more predicate to exclude read items

            return new RssItemList
            {
                Id = feed.Id,
                Name = feed.Name,
                Type = "Feed",
                RssItems = feed.Items.Select(Mapper.Map).ToList()
            };
        }

        [HttpGet, Route("Read/{id}")]
        public RssItem Read(Guid id)
        {
            var item = _databaseContext.Items
                .Include(x => x.Feed)
                .ThenInclude(x => x.Folder)
                .Single(x => x.Id == id);

            // TODO: mark as read

            return Mapper.Map(item);
        }

        [HttpPost, Route("Refresh")]
        public bool Refresh(string type, Guid? id) => throw new NotImplementedException();

        [HttpPost, Route("MarkAsRead")]
        public bool MarkAsRead(string type, Guid? id) => throw new NotImplementedException();

    }
}
