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
            throw new NotImplementedException();
        }

        public Item Get(Guid id)
        {
            return _context.Items.Single(i => i.Id == id);
        }

        public IEnumerable<Item> Get()
        {
            throw new NotImplementedException();
        }
    }
}