using System.Collections.Generic;
using System.Linq;
using Rss.Server.Models;
using System;

namespace Rss.Server.Services
{
    public class ItemService : IItemService
    {
        private readonly FeedsDbEntities _context;

        public ItemService(FeedsDbEntities context)
        {
            _context = context;
        }

        public void Read(Guid id)
        {
            var item = _context.Items.Find(id);

            item.ReadDateTime = DateTime.Now;

            _context.SaveChanges();
        }

        public Item Get(Guid id)
        {
            return _context.Items.Include("Feed").Single(i => i.Id == id);
        }

        public IEnumerable<Item> Get(int count)
        {
            return _context.Items.Include("Feed")
                .OrderByDescending(i => i.PublishedDateTime)
                .Where(i => i.ReadDateTime == null)
                .Take(count);
        }
    }
}