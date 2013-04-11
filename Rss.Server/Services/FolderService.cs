using Rss.Server.Models;
using System;
using System.Linq;

namespace Rss.Server.Services
{
    public class FolderService : IFolderService
    {
        private readonly FeedsDbEntities _context;

        public FolderService(FeedsDbEntities context)
        {
            _context = context;
        }

        public Folder Get(Guid id)
        {
            return _context.Folders.Include("Feeds").Single(f => f.Id == id);
        }

        public RootFolder GetRoot()
        {
            var folders = _context.Folders.Include("Feeds").ToList();
            var looseFeeds = _context.Feeds.Where(f => f.FolderId == null).ToList();

            // reproject to avoid enumerating Items
            // TODO: should use view models?
            var root = new RootFolder
                {
                    Folders = folders.Select(f => new Folder
                        {
                        Name = f.Name,
                        Id = f.Id,
                        Feeds = f.Feeds.Select(feed => new Feed
                            {
                                Id = feed.Id,
                                FavIcon = feed.FavIcon,
                                FeedUrl = feed.FeedUrl,
                                FolderId = feed.FolderId,
                                HtmlUrl = feed.HtmlUrl,
                                LastBuildDate = feed.LastBuildDate,
                                LastUpdateDateTime = feed.LastUpdateDateTime,
                                Name = feed.Name,
                                UpdateFrequency = feed.UpdateFrequency,
                                UpdatePeriod = feed.UpdatePeriod
                            }).ToList()
                        }),
                    Feeds = looseFeeds
                };

            return root;
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

            _context.SaveChanges();
        }

        public void Rename(Guid id, string name)
        {
            var folder = Get(id);

            folder.Name = name;

            _context.SaveChanges();
        }

        public void Create(string name)
        {
            var folder = _context.Folders.Create();

            folder.Name = name;

            _context.Folders.Add(folder);

            _context.SaveChanges();
        }
    }
}