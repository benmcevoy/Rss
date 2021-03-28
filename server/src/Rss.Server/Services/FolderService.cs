using System.Collections.Generic;
using Rss.Server.Models;
using System;
using System.Data.Entity;
using System.Linq;
using WebGrease.Css.Extensions;

namespace Rss.Server.Services
{
    public class FolderService : IFolderService
    {
        private readonly FeedsDbEntities _context;
        private readonly IFeedService _feedService;

        public FolderService(FeedsDbEntities context, IFeedService feedService)
        {
            _context = context;

            _feedService = feedService;
        }

        public Folder Get(Guid id)
        {
            var flatFolders = _context.Database.SqlQuery(typeof(FolderWithItemCount),
                                             string.Format(@"
select
Folder.Id, Folder.Name, Folder.LastUpdateDateTime,
Feed.Id AS FeedId, Feed.Name AS FeedName, Feed.FolderId, Feed.LastUpdateDateTime AS FeedLastUpdateDateTime,
Feed.FeedUrl, Feed.HtmlUrl, Feed.LastBuildDate, Feed.UpdatePeriod, Feed.UpdateFrequency, Feed.FavIcon,
coalesce(items.itemcount,0) as ItemCount

FROM Folder INNER JOIN Feed 
		ON Feed.FolderId = Folder.Id
	left join   (select Item.FeedId, count(1) AS ItemCount 
                from Item
                where item.ReadDateTime is null
                group by item.feedid) as items
	on Feed.Id = items.FeedId where Folder.Id = '{0}'", id.ToString("D")))
                          .Cast<FolderWithItemCount>().ToList();

            var distinctFolder = flatFolders.Select(f => new Folder
            {
                Id = f.Id,
                Name = f.Name,
                LastUpdateDateTime = f.LastUpdateDateTime
            }).Distinct(new FolderComparer()).Single();

            distinctFolder.Feeds = flatFolders
                                        .OrderBy(f => f.FeedName)
                                        .Select(f => new Feed
                                        {
                                            FavIcon = f.FavIcon,
                                            FolderId = f.FolderId,
                                            Folder = f.Folder,
                                            FeedUrl = f.FeedUrl,
                                            HtmlUrl = f.HtmlUrl,
                                            Id = f.FeedId,
                                            ItemCount = f.ItemCount,
                                            Items = f.Items,
                                            LastBuildDate = f.LastBuildDate,
                                            LastUpdateDateTime = f.FeedLastUpdateDateTime,
                                            Name = f.FeedName,
                                            UpdateFrequency = f.UpdateFrequency,
                                            UpdatePeriod = f.UpdatePeriod
                                        }).ToList();

            return distinctFolder;
        }

        public Folder Get2(Guid id)
        {
            return _context
                .Folders
                .Include(i => i.Feeds.Select(x => x.Items))
                .FirstOrDefault(f => f.Id == id);
        }

        public Folder Get(string folderName)
        {
            return _context
                .Folders
                .FirstOrDefault(f => f.Name == folderName);
        }

        public RootFolder GetRoot()
        {
            // project into anonymous type.  You are not allowed to project into a mapped entity
            var feeds = _context.Feeds
              .Where(f => f.FolderId == null)
              .Select(f => new
              {
                  Feed = f,
                  ItemCount = f.Items.Count(i => i.ReadDateTime == null)
              }).ToList();

            // sql query is not allowed heirachy. project into flat type.
            var flatFolders = _context.Database.SqlQuery(typeof(FolderWithItemCount),
@"select
Folder.Id, Folder.Name, Folder.LastUpdateDateTime,
Feed.Id AS FeedId, Feed.Name AS FeedName, Feed.FolderId, Feed.LastUpdateDateTime AS FeedLastUpdateDateTime,
Feed.FeedUrl, Feed.HtmlUrl, Feed.LastBuildDate, Feed.UpdatePeriod, Feed.UpdateFrequency, Feed.FavIcon,
coalesce(items.itemcount,0) as ItemCount

FROM Folder INNER JOIN Feed 
		ON Feed.FolderId = Folder.Id
	left join   (select Item.FeedId, count(1) AS ItemCount 
                from Item
                where item.ReadDateTime is null
                group by item.feedid) as items
	on Feed.Id = items.FeedId").Cast<FolderWithItemCount>().ToList();

            var distinctFolders = flatFolders.Select(f => new Folder
            {
                Id = f.Id,
                Name = f.Name,
                LastUpdateDateTime = f.LastUpdateDateTime
            }).Distinct(new FolderComparer()).ToList();

            foreach (var folder in distinctFolders)
            {
                folder.Feeds = flatFolders
                                    .Where(f => f.Id == folder.Id)
                                    .Select(f => new Feed
                                    {
                                        FavIcon = f.FavIcon,
                                        FolderId = f.FolderId,
                                        Folder = f.Folder,
                                        FeedUrl = f.FeedUrl,
                                        HtmlUrl = f.HtmlUrl,
                                        Id = f.FeedId,
                                        ItemCount = f.ItemCount,
                                        Items = f.Items,
                                        LastBuildDate = f.LastBuildDate,
                                        LastUpdateDateTime = f.FeedLastUpdateDateTime,
                                        Name = f.FeedName,
                                        UpdateFrequency = f.UpdateFrequency,
                                        UpdatePeriod = f.UpdatePeriod
                                    }).ToList();
            }

            return new RootFolder
            {
                Feeds = feeds.Select(f => new Feed
                {
                    ItemCount = f.ItemCount,
                    FavIcon = f.Feed.FavIcon,
                    FeedUrl = f.Feed.FeedUrl,
                    HtmlUrl = f.Feed.HtmlUrl,
                    Id = f.Feed.Id,
                    LastBuildDate = f.Feed.LastBuildDate,
                    LastUpdateDateTime = f.Feed.LastUpdateDateTime,
                    Name = f.Feed.Name,
                    UpdateFrequency = f.Feed.UpdateFrequency,
                    UpdatePeriod = f.Feed.UpdatePeriod,
                }),
                Folders = distinctFolders
            };
        }

        public void AddFeed(Guid id, Guid feedId)
        {
            throw new NotImplementedException();
        }

        public void RemoveFeed(Guid id, Guid feedId)
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid id)
        {
            var folder = Get(id);

            _context.Folders.Remove(folder);
        }

        public void Rename(Guid id, string name)
        {
            var folder = Get(id);

            folder.Name = name;
        }

        public Folder Create(string name)
        {
            var folder = _context.Folders.Create();

            folder.Name = name;

            _context.Folders.Add(folder);

            return folder;
        }

        public void Refresh(Guid id)
        {
            var folder = _context.Folders
                    .Include("Feeds")
                    .Single(f => f.Id == id);

            folder.Feeds
                .ForEach(feed => _feedService.Refresh(feed.Id));
        }


        public void Mark(Guid id, MarkOptions markOptions)
        {
            _context.MarkFolderAsReadCommand(id, markOptions);
        }

        public IEnumerable<Item> GetItems(Guid id, ReadOptions readOptions)
        {
            return _context.Items
                    .Include(i => i.Feed)
                    .Where(f =>
                        f.Feed.FolderId == id
                                    && f.ReadDateTime == null)
                    .OrderByDescending(i => i.PublishedDateTime);
        }
    }
}