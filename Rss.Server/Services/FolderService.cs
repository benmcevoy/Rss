using Rss.Server.Models;
using System;
using System.Linq;

namespace Rss.Server.Services
{
    public class FolderService : IFolderService
    {
        private readonly FeedsDBEntities _context;

        public FolderService(FeedsDBEntities context)
        {
            _context = context;
        }

        public Folder Get(Guid id)
        {
            return _context.Folders.Include("Feeds").Single(f => f.Id == id);
        }

        public RootFolder GetRoot()
        {
            var root = new RootFolder
                {
                    Folders = _context.Folders.Include("Feeds").ToList(),
                    Feeds = _context.Feeds.Where(f => f.FolderId == null).ToList()
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